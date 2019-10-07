using CodeRunner.Diagnostics;
using CodeRunner.Extensions.Commands;
using CodeRunner.Extensions.Managements;

namespace CodeRunner.Extensions
{
    public static class ExtensionExtensions
    {
        public static string GetFullName(this IExtension extension)
        {
            Assert.ArgumentNotNull(extension, nameof(extension));
            return $"{extension.Publisher}.{extension.Name}";
        }

        public static string GetFullName(this IExtension extension, ICommandBuilder command)
        {
            Assert.ArgumentNotNull(command, nameof(command));
            return $"{extension.GetFullName()}::{command.Name}";
        }

        public static string GetFullName(this IExtension extension, IWorkspaceProvider workspace)
        {
            Assert.ArgumentNotNull(workspace, nameof(workspace));
            return $"{extension.GetFullName()}::{workspace.Name}";
        }
    }
}
