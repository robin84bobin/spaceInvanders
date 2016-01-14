using System;
using Parse;
using System.Collections.Generic;
using UnityEngine;

class BulletParseFactory : IConcreteParseFactory
{
	public IBaseData Create (ParseObject po)
	{
		BulletData bulletData = new BulletData();
		bulletData.type = po.ClassName;
		bulletData.objectId = po.ObjectId;
		return bulletData;
	}
}



