using System;
using Parse;
using System.Collections.Generic;
using UnityEngine;

public class ParseFactory
{
	private static ParseFactory _instance;

	public static ParseFactory Instance {
		get { 
			if (_instance == null){
				_instance = new ParseFactory();
			}
			return _instance;
		}
	}

	private Dictionary<string, IConcreteParseFactory> _factories; 

	public IBaseData Create (string dataType, ParseObject po)
	{
		if (_factories.ContainsKey (dataType))
			return _factories [dataType].Create(po);
		else {
			Debug.LogError(string.Format("Can't find factory to create {0} from ParseObject:{1}",
			                             dataType,po.ClassName));
			return null;
		}
	}

	private ParseFactory()
	{
		_factories = new Dictionary<string, IConcreteParseFactory>();
		_factories.Add ( DataTypes.LEVEL, new LevelParseFactory());
		_factories.Add ( DataTypes.HERO, new HeroParseFactory());
		_factories.Add ( DataTypes.ENEMY, new EnemyParseFactory());
		_factories.Add ( DataTypes.BULLET, new BulletParseFactory());
		_factories.Add ( DataTypes.WEAPON, new WeaponParseFactory());
	}
}


