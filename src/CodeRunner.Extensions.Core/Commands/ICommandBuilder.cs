using CodeRunner.Commands;

namespace CodeRunner.Extensions.Commands
{
    public interface ICommandBuilder
    {
        string Name { get; }

        Command Build();
    }
}
