using System.Collections.Generic;

namespace CodeRunner.Commands
{
    public abstract class Symbol
    {
        protected Symbol(string name, string description = "")
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public IList<string> Aliases { get; } = new List<string>();
    }
}
