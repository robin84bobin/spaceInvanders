using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Extensions;
using Parse;

namespace Assets.Scripts.Data.DataFactories.ParseFactories
{
    class WeaponParseFactory : IConcreteParseFactory
    {
        public IBaseData Create (ParseObject po_)
        {
            WeaponData weaponData = new WeaponData();
            weaponData.Type = po_.ClassName;
            weaponData.ObjectId = po_.ObjectId;
            weaponData.BulletId = po_.TryGetPointerObjectId (DataTypes.BULLET);
            return weaponData;
        }
    }
}




