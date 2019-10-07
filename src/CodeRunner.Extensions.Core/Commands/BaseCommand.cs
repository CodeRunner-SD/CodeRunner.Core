using CodeRunner.Commands;
using CodeRunner.Pipelines;
using System.Threading;
using System.Threading.Tasks;

namespace CodeRunner.Extensions.Commands
{
    public abstract class BaseCommand<T> : ICommandBuilder
    {
        public abstract string Name { get; }

        public abstract Command Configure();

        public abstract Task<int> Handle(T argument, ParserContext context, PipelineContext operation, CancellationToken cancellationToken);

        public virtual Command Build()
        {
            Command command = Configure();
            command.Handler = GetType().GetMethod(nameof(Handle));
            return command;
        }
    }
}
