using Assets.Scripts.Data;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Extensions;
using Parse;

namespace Assets.Scripts.Factories.DataFactories.ParseFactories
{
    public class LevelParseFactory : IConcreteParseFactory
    {
        #region IConcreteParseFactory implementation

        public IBaseData Create(ParseObject po_)
        {
            var levelData = new LevelData {
                Type = po_.ClassName,
                ObjectId = po_.ObjectId,
                Id = po_.TryGet<int>("ID"),
                LevelSceneName = po_.TryGet<string>("LevelSceneName"),
                HeroId = po_.TryGetPointerObjectId(DataTypes.HERO),
                EnemyId = po_.TryGetPointerObjectId(DataTypes.ENEMY),
                EnemyWaveRate = po_.TryGet<int>("EnemyWaveRate"),
                EnemyWaveSize = po_.TryGet<int>("EnemyWaveSize"),
                EnemyStartSpeed = po_.TryGet<int>("EnemyStartSpeed"),
                EnemySpeedFactor = po_.TryGet<double>("EnemySpeedFactor"),
                EnemyMovePeriod = po_.TryGet<double>("EnemyMovePeriod")
            };
            return levelData;
        }

        #endregion
    }
}