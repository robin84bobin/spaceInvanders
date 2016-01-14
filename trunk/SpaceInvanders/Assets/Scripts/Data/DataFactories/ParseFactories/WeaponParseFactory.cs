using System;
using Parse;
using System.Collections.Generic;
using UnityEngine;

class WeaponParseFactory : IConcreteParseFactory
{
	public IBaseData Create (ParseObject po)
	{
		WeaponData weaponData = new WeaponData();
		weaponData.type = po.ClassName;
		weaponData.objectId = po.ObjectId;
		weaponData.bulletId = po.TryGetPointerObjectId (DataTypes.BULLET);
		return weaponData;
	}
}




