using System.Threading.Tasks;

namespace CodeRunner.Templates
{
    public abstract class BaseTemplate : ITemplate
    {
        public virtual VariableCollection GetVariables() => new VariableCollection();

        public abstract Task DoResolve(ResolveContext context);
    }

    public abstract class BaseTemplate<TResult> : BaseTemplate, ITemplate<TResult>
    {
        public override Task DoResolve(ResolveContext context) => Resolve(context);

        public abstract Task<TResult> Resolve(ResolveContext context);
    }
}
