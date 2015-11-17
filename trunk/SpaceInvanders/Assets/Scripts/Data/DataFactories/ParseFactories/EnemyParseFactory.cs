using System;
using Parse;
using System.Collections.Generic;
using UnityEngine;

class EnemyParseFactory : IConcreteParseFactory
{
	public IBaseData Create (ParseObject po)
	{
		EnemyData enemyData = new EnemyData();
		enemyData.Type = po.ClassName;
		enemyData.ObjectId = po.ObjectId;
		enemyData.weaponId = po.TryGetPointerObjectId (DataTypes.WEAPON);
		//TODO
		return enemyData;
	}
}



