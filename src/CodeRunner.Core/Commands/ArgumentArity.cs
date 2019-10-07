namespace CodeRunner.Commands
{
    public class ArgumentArity
    {
        public ArgumentArity(int minimumNumberOfValues, int maximumNumberOfValues)
        {
            MinimumNumberOfValues = minimumNumberOfValues;
            MaximumNumberOfValues = maximumNumberOfValues;
        }

        public int MinimumNumberOfValues { get; set; }

        public int MaximumNumberOfValues { get; set; }

        public static ArgumentArity Zero => new ArgumentArity(0, 0);

        public static ArgumentArity ZeroOrOne => new ArgumentArity(0, 1);

        public static ArgumentArity ExactlyOne => new ArgumentArity(1, 1);

        public static ArgumentArity ZeroOrMore => new ArgumentArity(0, 255);

        public static ArgumentArity OneOrMore => new ArgumentArity(1, 255);
    }
}
