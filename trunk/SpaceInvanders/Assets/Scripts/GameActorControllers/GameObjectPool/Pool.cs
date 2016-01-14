using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Pool  
{
	private static Pool _instance;
	public static Pool instance { 
		get {
			if (_instance == null){
				_instance = new Pool();
			}
			return _instance;
		}
	}

	private Dictionary<Type,List<BaseActorController>> _pool;

	public void Hide (BaseActorController actor)
	{
		Type type = actor.GetType();
		if (!_pool.ContainsKey(type)){
			_pool.Add(type, new List<BaseActorController>());
		}
		_pool[type].Add(actor);
	}

	public BaseActorController Get(Type type)
	{
		if (_pool.ContainsKey(type)){
			if (_pool[type].Count > 0){
				BaseActorController actor = _pool[type][0];
				_pool[type].RemoveAt(0);
				return actor;
			}
		}

		return null;
	}

	private Pool()
	{
		_pool = new Dictionary<Type, List<BaseActorController>>();
	}
}
