using CodeRunner.Diagnostics;
using CodeRunner.Loggings;
using CodeRunner.Managements;
using CodeRunner.Pipelines;
using System.CommandLine;
using System.IO;

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

        public static IConsole GetConsole(this IServiceScope scope)
        {
            Assert.ArgumentNotNull(scope, nameof(scope));
            return scope.GetService<IConsole>();
        }

        public static IHost GetHost(this IServiceScope scope)
        {
            Assert.ArgumentNotNull(scope, nameof(scope));
            return scope.GetService<IHost>();
        }

        public static TextReader GetInput(this IServiceScope scope)
        {
            Assert.ArgumentNotNull(scope, nameof(scope));
            return scope.GetService<TextReader>();
        }

        public static ILogger GetLogger(this IServiceScope scope)
        {
            Assert.ArgumentNotNull(scope, nameof(scope));
            return scope.GetService<ILogger>();
        }
    }
}