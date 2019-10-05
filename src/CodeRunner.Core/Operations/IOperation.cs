using CodeRunner.Pipelines;
using CodeRunner.Templates;

namespace CodeRunner.Operations
{
    public interface IOperation : ITemplate<PipelineBuilder<OperationWatcher, Wrapper<bool>>>
    {

    }
}
