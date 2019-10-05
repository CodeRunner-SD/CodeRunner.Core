using System.Threading.Tasks;

namespace CodeRunner.Templates
{
    public interface ITemplate
    {
        VariableCollection GetVariables();

        Task DoResolve(ResolveContext context);
    }

    public interface ITemplate<TResult> : ITemplate
    {
        Task<TResult> Resolve(ResolveContext context);
    }
}
