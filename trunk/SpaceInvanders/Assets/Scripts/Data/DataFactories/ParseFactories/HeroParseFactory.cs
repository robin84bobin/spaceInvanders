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
                type = po_.ClassName,
                objectId = po_.ObjectId,
                weaponId = po_.TryGetPointerObjectId(DataTypes.WEAPON)
            };

            return heroData;
        }

        #endregion
    }
}