using CodeRunner.Managements.Configurations;
using CodeRunner.Packaging;
using CodeRunner.Templates;

namespace CodeRunner.Managements
{
    public interface ITemplateManager : IItemManager<TemplateSettings, Package<ITemplate>>
    {

    }
}
