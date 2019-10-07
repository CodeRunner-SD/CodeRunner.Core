using CodeRunner.Diagnostics;
using CodeRunner.Extensions.Terminals;
using CodeRunner.Templates;
using System.Collections.Generic;

namespace CodeRunner.Extensions.Helpers.Rendering
{
    public static class ConsoleIO
    {
        public static string? InputVariableValue(this ITerminal terminal, Variable variable)
        {
            Assert.ArgumentNotNull(terminal, nameof(terminal));
            Assert.ArgumentNotNull(variable, nameof(variable));
            terminal.Output.Write("  ");
            if (variable.IsRequired)
            {
                terminal.Output.WriteEmphasize(variable.Name);
            }
            else
            {
                terminal.Output.WriteBold(variable.Name);
                terminal.Output.Write($"({ variable.GetDefault<object>().ToString()})");
            }
            terminal.Output.Write(": ");
            return terminal.Input.ReadLine();
        }

        public static bool FillVariables(this ITerminal terminal, IEnumerable<Variable> variables, ResolveContext context)
        {
            Assert.ArgumentNotNull(terminal, nameof(terminal));
            Assert.ArgumentNotNull(variables, nameof(variables));
            Assert.ArgumentNotNull(context, nameof(context));

            bool isFirst = true;
            foreach (Variable v in variables)
            {
                if (context.HasVariable(v.Name))
                {
                    continue;
                }

                if (isFirst)
                {
                    terminal.Output.WriteLine("Please input variable value:");
                    isFirst = false;
                }

                string? line = terminal.InputVariableValue(v);
                if (string.IsNullOrEmpty(line))
                {
                    if (v.IsRequired)
                    {
                        return false;
                    }
                }
                else
                {
                    _ = context.WithVariable(v.Name, line!);
                }
            }
            return true;
        }
    }
}
