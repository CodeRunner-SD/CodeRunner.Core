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

        public static (string, string, string) SplitFullName(string source)
        {
            Assert.ArgumentNotNull(source, nameof(source));
            string pub = "", ext = "", sub = "";
            {
                int ind = source.IndexOf("::", System.StringComparison.InvariantCultureIgnoreCase);
                if (ind != -1)
                {
                    sub = source.Substring(ind + 2);
                    source = source.Substring(0, ind);
                }
            }
            {
                int ind = source.IndexOf(".", System.StringComparison.InvariantCultureIgnoreCase);
                if (ind != -1)
                {
                    ext = source.Substring(ind + 1);
                    pub = source.Substring(0, ind);
                }
            }
            return (pub, ext, sub);
        }
    }
}
