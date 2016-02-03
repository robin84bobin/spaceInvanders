using Assets.Scripts.Data.DataSource;
using Parse;

namespace Assets.Scripts.Factories.DataFactories.ParseFactories
{
    internal class BulletParseFactory : IConcreteParseFactory
    {
        public IBaseData Create(ParseObject po_)
        {
            var bulletData = new BulletData {
                Type = po_.ClassName,
                ObjectId = po_.ObjectId
            };
            return bulletData;
        }
    }
}