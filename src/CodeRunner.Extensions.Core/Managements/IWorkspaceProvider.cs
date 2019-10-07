using CodeRunner.Managements;
using CodeRunner.Templates;

namespace CodeRunner.Extensions.Managements
{
    public interface IWorkspaceProvider
    {
        string Name { get; }

        ITemplate<IWorkspace> GetProvider();
    }
}
