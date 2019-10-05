using CodeRunner.Diagnostics;

namespace CodeRunner.Loggings
{
    public static class LoggerExtensions
    {
        public static LoggerScope CreateScope(this ILogger logger, string name, LogLevel level)
        {
            Assert.ArgumentNotNull(name, nameof(name));

            return new LoggerScope(logger, $"/{name}", level);
        }

        public static LoggerScope CreateScope(this LoggerScope scope, string name, LogLevel level)
        {
            Assert.ArgumentNotNull(scope, nameof(scope));
            Assert.ArgumentNotNull(name, nameof(name));

            return new LoggerScope(scope.Source, $"{scope.Name}/{name}", level);
        }

        public static ILogger UseLevelFilter(this ILogger logger, LogLevel level)
        {
            Assert.ArgumentNotNull(logger, nameof(logger));

            return logger.UseFilter(LogFilter.Create(item => item.Level >= level));
        }
    }
}
