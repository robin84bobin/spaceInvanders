using Assets.Scripts.Data.DataSource;
using Parse;

namespace Assets.Scripts.Factories.DataFactories.ParseFactories
{
    internal class BulletParseFactory : IConcreteParseFactory
    {
        public IBaseData Create(ParseObject po_)
        {
            var bulletData = new BulletData {
                type = po_.ClassName,
                objectId = po_.ObjectId
            };
            return bulletData;
        }
    }
}