using CodeRunner.Diagnostics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeRunner.Pipelines
{
    public class ServiceProvider
    {
        private readonly Dictionary<Type, Dictionary<string, ServiceItem>> pools = new Dictionary<Type, Dictionary<string, ServiceItem>>();

        public Task<IServiceScope> CreateScope(string name)
        {
            Assert.ArgumentNotNull(name, nameof(name));
            return Task.FromResult<IServiceScope>(new ServiceScope(name, pools));
        }
    }
}
