using Assets.Scripts.Data.DataSource;
using Parse;

namespace Assets.Scripts.Data.DataFactories.ParseFactories
{
    class BulletParseFactory : IConcreteParseFactory
    {
        public IBaseData Create (ParseObject po_)
        {
            BulletData bulletData = new BulletData();
            bulletData.Type = po_.ClassName;
            bulletData.ObjectId = po_.ObjectId;
            return bulletData;
        }
    }
}



