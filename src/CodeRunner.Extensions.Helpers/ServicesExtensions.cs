using CodeRunner.Diagnostics;
using CodeRunner.Extensions.Terminals;
using CodeRunner.Loggings;
using CodeRunner.Managements;
using CodeRunner.Pipelines;

namespace CodeRunner.Extensions.Helpers
{
    public static class ServicesExtensions
    {
        public static IWorkspace GetWorkspace(this IServiceScope scope)
        {
            Assert.ArgumentNotNull(scope, nameof(scope));
            return scope.GetService<IWorkspace>();
        }

        public static IWorkItem? GetWorkItem(this IServiceScope scope)
        {
            Assert.ArgumentNotNull(scope, nameof(scope));
            return scope.TryGet<IWorkItem>(out IWorkItem? workItem) ? workItem : null;
        }

        public static ITerminal GetTerminal(this IServiceScope scope)
        {
            Assert.ArgumentNotNull(scope, nameof(scope));
            return scope.GetService<ITerminal>();
        }

        public static IExtension GetExtension(this IServiceScope scope)
        {
            Assert.ArgumentNotNull(scope, nameof(scope));
            return scope.GetService<IExtension>();
        }

        public static IHost GetHost(this IServiceScope scope)
        {
            Assert.ArgumentNotNull(scope, nameof(scope));
            return scope.GetService<IHost>();
        }

        public static ILogger GetLogger(this IServiceScope scope)
        {
            Assert.ArgumentNotNull(scope, nameof(scope));
            return scope.GetService<ILogger>();
        }
    }
}