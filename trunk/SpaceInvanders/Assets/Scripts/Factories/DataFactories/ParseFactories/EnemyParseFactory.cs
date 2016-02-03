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
                Type = po_.ClassName,
                ObjectId = po_.ObjectId,
                WeaponId = po_.TryGetPointerObjectId(DataTypes.WEAPON)
            };
            //TODO
            return enemyData;
        }
    }
}