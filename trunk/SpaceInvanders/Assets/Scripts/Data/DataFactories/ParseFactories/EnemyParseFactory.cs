using Assets.Scripts.Data;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Extensions;
using Parse;

namespace Assets.Scripts.Factories.DataFactories.ParseFactories
{
    internal class EnemyParseFactory : IConcreteParseFactory
    {
        public IBaseData Create(ParseObject po_)
        {
            var enemyData = new EnemyData {
                type = po_.ClassName,
                objectId = po_.ObjectId,
                weaponId = po_.TryGetPointerObjectId(DataTypes.WEAPON)
            };
            //TODO
            return enemyData;
        }
    }
}