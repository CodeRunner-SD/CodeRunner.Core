using CodeRunner.Diagnostics;
using CodeRunner.Loggings;
using System.Diagnostics.CodeAnalysis;

namespace CodeRunner.Pipelines
{
    public class PipelineContext
    {
        public PipelineContext(IServiceScope services, LoggerScope logs)
        {
            Assert.ArgumentNotNull(services, nameof(services));
            Assert.ArgumentNotNull(logs, nameof(logs));

            Services = services;
            Logs = logs;
        }

        public IServiceScope Services { get; }

        public LoggerScope Logs { get; }

        public bool IsStopped { get; set; }
    }

    public class PipelineContext<TOrigin, TResult> : PipelineContext where TResult : class
    {
        public PipelineContext(IServiceScope services, TOrigin origin, TResult? result, LoggerScope logs) : base(services, logs)
        {
            Assert.ArgumentNotNull(services, nameof(services));
            Assert.ArgumentNotNull(origin, nameof(origin));
            Assert.ArgumentNotNull(logs, nameof(logs));

            Origin = origin;
            Result = result;
        }

        public TOrigin Origin { get; }

        public TResult? Result { get; }

        [DoesNotReturn]
        public TResult IgnoreResult()
        {
            Logs.Log("Pipeline ignore result.", LogLevel.Debug);
            throw new PipelineResultIgnoreException();
        }
    }
}
