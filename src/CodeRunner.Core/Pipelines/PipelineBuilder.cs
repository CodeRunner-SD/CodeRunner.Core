using CodeRunner.Diagnostics;
using CodeRunner.Loggings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeRunner.Pipelines
{
    public class PipelineBuilder<TOrigin, TResult> where TResult : class
    {
        private List<(string, object)> Configures { get; } = new List<(string, object)>();

        private List<(string, PipelineOperation<TOrigin, TResult>)> Ops { get; } = new List<(string, PipelineOperation<TOrigin, TResult>)>();

        public PipelineBuilder<TOrigin, TResult> Configure(string name, Func<IServiceScope, Task> func)
        {
            Assert.ArgumentNotNull(name, nameof(name));
            Assert.ArgumentNotNull(func, nameof(func));

            Configures.Add((name, func));
            return this;
        }

        public PipelineBuilder<TOrigin, TResult> Configure(string name, Action<IServiceScope> func)
        {
            Assert.ArgumentNotNull(name, nameof(name));
            Assert.ArgumentNotNull(func, nameof(func));

            Configures.Add((name, func));
            return this;
        }

        public PipelineBuilder<TOrigin, TResult> Use(string name, PipelineOperation<TOrigin, TResult> op)
        {
            Assert.ArgumentNotNull(name, nameof(name));
            Assert.ArgumentNotNull(op, nameof(op));

            Ops.Add((name, op));
            return this;
        }

        public async Task<Pipeline<TOrigin, TResult>> Build(TOrigin origin, ILogger logger)
        {
            Assert.ArgumentNotNull(origin, nameof(origin));
            Assert.ArgumentNotNull(logger, nameof(logger));

            ServiceProvider services = new ServiceProvider();
            foreach ((string name, object func) in Configures)
            {
                switch (func)
                {
                    case Func<IServiceScope, Task> f:
                        await f(await services.CreateScope(name).ConfigureAwait(false)).ConfigureAwait(false);
                        break;
                    case Action<IServiceScope> a:
                        a(await services.CreateScope(name).ConfigureAwait(false));
                        break;
                }
            }
            return new Pipeline<TOrigin, TResult>(origin, logger, services, Ops.ToArray());
        }
    }
}
