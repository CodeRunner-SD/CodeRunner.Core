using CodeRunner.Diagnostics;

namespace CodeRunner.Extensions.Terminals
{
    public static class TerminalExtensions
    {
        public static void EnsureAtLeft(this ITerminal terminal)
        {
            Assert.ArgumentNotNull(terminal, nameof(terminal));

            if (terminal.CursorLeft != 0)
            {
                terminal.Output.WriteLine();
                terminal.SetCursorPosition(0, terminal.CursorTop);
            }
        }

        public static void WriteLine(this IStandardWriter writer)
        {
            Assert.ArgumentNotNull(writer, nameof(writer));
            writer.Write("\n");
        }

        public static void WriteLine(this IStandardWriter writer, string content)
        {
            Assert.ArgumentNotNull(writer, nameof(writer));
            writer.Write(content);
            writer.Write("\n");
        }

        public static void WriteErrorLine(this IStandardWriter writer, string content)
        {
            Assert.ArgumentNotNull(writer, nameof(writer));
            writer.WriteError(content);
            writer.WriteLine();
        }

        public static void WriteWarningLine(this IStandardWriter writer, string content)
        {
            Assert.ArgumentNotNull(writer, nameof(writer));
            writer.WriteWarning(content);
            writer.WriteLine();
        }

        public static void WriteInformationLine(this IStandardWriter writer, string content)
        {
            Assert.ArgumentNotNull(writer, nameof(writer));
            writer.WriteInformation(content);
            writer.WriteLine();
        }

        public static void WriteFatalLine(this IStandardWriter writer, string content)
        {
            Assert.ArgumentNotNull(writer, nameof(writer));
            writer.WriteFatal(content);
            writer.WriteLine();
        }

        public static void WriteDebugLine(this IStandardWriter writer, string content)
        {
            Assert.ArgumentNotNull(writer, nameof(writer));
            writer.WriteDebug(content);
            writer.WriteLine();
        }
    }
}
