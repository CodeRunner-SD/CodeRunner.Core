using System.Collections.Generic;

namespace CodeRunner.Commands
{
    public class ParserContext
    {
        public ParserContext(IReadOnlyCollection<string> unparsedTokens, IReadOnlyCollection<string> unmatchedTokens)
        {
            UnparsedTokens = unparsedTokens;
            UnmatchedTokens = unmatchedTokens;
        }

        public IReadOnlyCollection<string> UnparsedTokens { get; }

        public IReadOnlyCollection<string> UnmatchedTokens { get; }
    }
}
