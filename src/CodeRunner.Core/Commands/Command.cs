using System.Collections.Generic;
using System.Reflection;

namespace CodeRunner.Commands
{
    public class Command : Symbol
    {
        public Command(string name, string description = "") : base(name, description)
        {
        }

        public MethodInfo? Handler { get; set; }

        public IList<Option> Options { get; } = new List<Option>();

        public IList<Argument> Arguments { get; } = new List<Argument>();

        public IList<Command> Commands { get; } = new List<Command>();
    }
}
