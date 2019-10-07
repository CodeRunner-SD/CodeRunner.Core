namespace CodeRunner.Commands
{
#pragma warning disable CA1716 // 标识符不应与关键字匹配
    public class Option : Symbol
#pragma warning restore CA1716 // 标识符不应与关键字匹配
    {
        public Option(string name, string description = "") : base(name, description) { }

        public Argument? Argument { get; set; }
    }
}
