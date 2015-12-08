using System;
using Parse;
using System.Collections.Generic;
using UnityEngine;
using Data;
namespace Data{
class EnemyParseFactory : IConcreteParseFactory
{
	public IBaseData Create (ParseObject po)
	{
		EnemyData enemyData = new EnemyData();
		enemyData.type = po.ClassName;
		enemyData.objectId = po.ObjectId;
		enemyData.weaponId = po.TryGetPointerObjectId (DataTypes.WEAPON);
		//TODO
		return enemyData;
	}
}



}