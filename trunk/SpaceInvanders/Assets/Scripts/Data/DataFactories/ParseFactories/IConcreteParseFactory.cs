using Assets.Scripts.Data.DataSource;
using Parse;

namespace Assets.Scripts.Data.DataFactories.ParseFactories
{
    public interface IConcreteParseFactory
    {
        IBaseData Create(ParseObject po_) ;
    }
}


