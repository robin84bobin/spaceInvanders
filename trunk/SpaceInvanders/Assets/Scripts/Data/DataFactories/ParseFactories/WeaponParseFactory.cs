using Assets.Scripts.Data;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Extensions;
using Parse;

namespace Assets.Scripts.Factories.DataFactories.ParseFactories
{
    internal class WeaponParseFactory : IConcreteParseFactory
    {
        public IBaseData Create(ParseObject po_)
        {
            var weaponData = new WeaponData();
            weaponData.type = po_.ClassName;
            weaponData.objectId = po_.ObjectId;
            weaponData.bulletId = po_.TryGetPointerObjectId(DataTypes.BULLET);
            return weaponData;
        }
    }
}