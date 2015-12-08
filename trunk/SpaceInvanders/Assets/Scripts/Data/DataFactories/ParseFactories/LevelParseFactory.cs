using System;
using Parse;
using UnityEngine;
using System.Reflection;
using Data;
namespace Data{
public class LevelParseFactory : IConcreteParseFactory
{
	#region IConcreteParseFactory implementation
	public IBaseData Create(ParseObject po) 
	{
		LevelData levelData = new LevelData();
		levelData.type = po.ClassName;
		levelData.objectId = po.ObjectId;
		levelData.ID = po.TryGet<int> ("ID");
		levelData.levelSceneName = po.TryGet<string> ("LevelSceneName");
		levelData.heroId = po.TryGetPointerObjectId (DataTypes.HERO);
		levelData.enemyId = po.TryGetPointerObjectId (DataTypes.ENEMY);
		levelData.enemyWaveRate = po.TryGet<int> ("EnemyWaveRate");
		levelData.enemyWaveSize = po.TryGet<int> ("EnemyWaveSize");
		levelData.enemyStartSpeed = po.TryGet<int> ("EnemyStartSpeed");
		levelData.enemySpeedFactor = po.TryGet<double> ("EnemySpeedFactor");
		levelData.enemyMovePeriod = po.TryGet<double> ("EnemyMovePeriod");
		return levelData;
	}

	#endregion


}
}




