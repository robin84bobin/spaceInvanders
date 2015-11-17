using System;
using Parse;
using System.Collections.Generic;
using UnityEngine;

class WeaponParseFactory : IConcreteParseFactory
{
	public IBaseData Create (ParseObject po)
	{
		WeaponData weaponData = new WeaponData();
		weaponData.Type = po.ClassName;
		weaponData.ObjectId = po.ObjectId;
		weaponData.bulletId = po.TryGetPointerObjectId (DataTypes.BULLET);
		return weaponData;
	}
}



