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
                type = po_.ClassName,
                objectId = po_.ObjectId,
                id = po_.TryGet<int>("ID"),
                levelSceneName = po_.TryGet<string>("LevelSceneName"),
                heroId = po_.TryGetPointerObjectId(DataTypes.HERO),
                enemyId = po_.TryGetPointerObjectId(DataTypes.ENEMY),
                enemyWaveRate = po_.TryGet<int>("EnemyWaveRate"),
                enemyWaveSize = po_.TryGet<int>("EnemyWaveSize"),
                enemyStartSpeed = po_.TryGet<int>("EnemyStartSpeed"),
                enemySpeedFactor = po_.TryGet<double>("EnemySpeedFactor"),
                enemyMovePeriod = po_.TryGet<double>("EnemyMovePeriod")
            };
            return levelData;
        }

        #endregion
    }
}