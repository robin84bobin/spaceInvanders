using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Extensions;
using Parse;

namespace Assets.Scripts.Data.DataFactories.ParseFactories
{
    public class LevelParseFactory : IConcreteParseFactory
    {
        #region IConcreteParseFactory implementation
        public IBaseData Create(ParseObject po_) 
        {
            LevelData levelData = new LevelData();
            levelData.Type = po_.ClassName;
            levelData.ObjectId = po_.ObjectId;
            levelData.Id = po_.TryGet<int> ("ID");
            levelData.LevelSceneName = po_.TryGet<string> ("LevelSceneName");
            levelData.HeroId = po_.TryGetPointerObjectId (DataTypes.HERO);
            levelData.EnemyId = po_.TryGetPointerObjectId (DataTypes.ENEMY);
            levelData.EnemyWaveRate = po_.TryGet<int> ("EnemyWaveRate");
            levelData.EnemyWaveSize = po_.TryGet<int> ("EnemyWaveSize");
            levelData.EnemyStartSpeed = po_.TryGet<int> ("EnemyStartSpeed");
            levelData.EnemySpeedFactor = po_.TryGet<double> ("EnemySpeedFactor");
            levelData.EnemyMovePeriod = po_.TryGet<double> ("EnemyMovePeriod");
            return levelData;
        }

        #endregion


    }
}





