using Assets.Scripts.Data.DataSource;
using Parse;

namespace Assets.Scripts.Factories.DataFactories.ParseFactories
{
    public interface IConcreteParseFactory
    {
        IBaseData Create(ParseObject po_);
    }
}