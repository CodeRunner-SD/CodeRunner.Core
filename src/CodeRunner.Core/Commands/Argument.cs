using System;

namespace CodeRunner.Commands
{
    public class Argument : Symbol
    {
        public Argument(string name, string description = "", object? defaultValue = null) : base(name, description) => DefaultValue = defaultValue;

        public object? DefaultValue { get; set; }

        public ArgumentArity Arity { get; set; } = ArgumentArity.Zero;

        public Type ArgumentType { get; set; } = typeof(object);
    }

    public class Argument<T> : Argument
    {
        public Argument(string name, string description = "", object? defaultValue = null) : base(name, description, defaultValue) => ArgumentType = typeof(T);
    }
}
