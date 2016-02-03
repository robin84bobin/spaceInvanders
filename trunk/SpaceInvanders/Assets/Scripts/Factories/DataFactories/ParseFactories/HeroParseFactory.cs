using Assets.Scripts.Data;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Extensions;
using Parse;

namespace Assets.Scripts.Factories.DataFactories.ParseFactories
{
    public class HeroParseFactory : IConcreteParseFactory
    {
        #region IConcreteParseFactory implementation

        public IBaseData Create(ParseObject po_)
        {
            var heroData = new HeroData {
                Type = po_.ClassName,
                ObjectId = po_.ObjectId,
                WeaponId = po_.TryGetPointerObjectId(DataTypes.WEAPON)
            };

            return heroData;
        }

        #endregion
    }
}