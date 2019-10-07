using CodeRunner.Managements.Configurations;
using CodeRunner.Operations;
using CodeRunner.Packaging;

namespace CodeRunner.Managements
{
    public interface IOperationManager : IItemManager<OperationSettings, Package<IOperation>>
    {

    }
}
