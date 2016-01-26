using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Extensions;
using Parse;

namespace Assets.Scripts.Data.DataFactories.ParseFactories
{
    class EnemyParseFactory : IConcreteParseFactory
    {
        public IBaseData Create (ParseObject po_)
        {
            EnemyData enemyData = new EnemyData();
            enemyData.Type = po_.ClassName;
            enemyData.ObjectId = po_.ObjectId;
            enemyData.WeaponId = po_.TryGetPointerObjectId (DataTypes.WEAPON);
            //TODO
            return enemyData;
        }
    }
}
