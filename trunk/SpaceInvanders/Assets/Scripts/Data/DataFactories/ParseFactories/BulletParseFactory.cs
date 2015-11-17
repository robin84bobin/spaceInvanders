using System;
using Parse;
using System.Collections.Generic;
using UnityEngine;

class BulletParseFactory : IConcreteParseFactory
{
	public IBaseData Create (ParseObject po)
	{

		BulletData bulletData = new BulletData();
		bulletData.Type = po.ClassName;
		bulletData.ObjectId = po.ObjectId;
		return bulletData;
	}
}



