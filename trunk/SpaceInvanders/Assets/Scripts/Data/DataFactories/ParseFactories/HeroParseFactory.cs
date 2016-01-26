using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Extensions;
using Parse;

namespace Assets.Scripts.Data.DataFactories.ParseFactories
{
    public class HeroParseFactory : IConcreteParseFactory
    {
        #region IConcreteParseFactory implementation

        public IBaseData Create (ParseObject po_)
        {
            HeroData heroData = new HeroData();
            heroData.Type = po_.ClassName;
            heroData.ObjectId = po_.ObjectId;
            heroData.WeaponId = po_.TryGetPointerObjectId (DataTypes.WEAPON);

            return heroData;
        }

        #endregion
    }
}

