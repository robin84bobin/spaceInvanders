using System;
using Parse;
using UnityEngine;
using System.Reflection;

public class LevelParseFactory : IConcreteParseFactory
{
	#region IConcreteParseFactory implementation
	public IBaseData Create(ParseObject po) 
	{
		LevelData levelData = new LevelData();

		levelData.Type = po.ClassName;
		levelData.ObjectId = po.ObjectId;
		po.TryGetValue<int>("ID", out levelData.ID);
		po.TryGetValue<string>("LevelSceneName", out levelData.levelSceneName);
		levelData.heroId = po.TryGetPointerObjectId (DataTypes.HERO);
		levelData.enemyId = po.TryGetPointerObjectId (DataTypes.ENEMY);

		po.TryGetValue<int> ("EnemyWaveRate", out levelData.enemyWaveRate);
		po.TryGetValue<int> ("EnemyWaveSize", out levelData.enemyWaveSize);
		po.TryGetValue<int> ("EnemyStartSpeed", out levelData.enemyStartSpeed);
		po.TryGetValue<float> ("EnemySpeedFactor", out levelData.enemySpeedFactor);
		po.TryGetValue<float> ("EnemyMovePeriod", out levelData.enemyMovePeriod);

		return levelData;
	}

	#endregion


}





