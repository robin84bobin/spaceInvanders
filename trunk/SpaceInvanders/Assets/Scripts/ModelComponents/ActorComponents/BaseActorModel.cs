using System;
using System.Collections.Generic;
using UnityEngine;
using Data;

public abstract class BaseActorModel : BaseComponent
{
	protected Dictionary<string,double> skills = new Dictionary<string, double> ();

	public event Action<Vector3> MoveEvent = delegate{};
	protected virtual void OnMoveEvent (Vector3 obj)
	{
		var handler = MoveEvent;
		if (handler != null)
			handler (obj);
	}

	public event Action<int> DamageEvent = delegate{};
	protected virtual void OnDamageEvent (int obj)
	{
		var handler = DamageEvent;
		if (handler != null)
			handler (obj);
	}

	public event Action<BaseActorModel> DeathEvent = delegate{};
	protected virtual void OnDeathEvent (BaseActorModel obj)
	{
		var handler = DeathEvent;
		if (handler != null)
			handler (obj);
	}

	public string dataType { get; private set; }

	public BaseActorModel (BaseData data)
	{
		dataType = data.type;
	}

	public void Destroy()
	{
		Release ();
	} 


}


