using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActorModel
{
	public event Action<Vector3> OnMove = delegate{};
	public event Action<int> OnDamage = delegate{};
	public event Action<BaseActorModel> OnDeath = delegate{};

	public string dataType;

	private Dictionary<Type,List<IActorBehaviour>> _behaviours = new Dictionary<Type, List<IActorBehaviour>> ();

	public BaseActorModel (BaseData data)
	{
		dataType = data.Type;
	}

	public void Destroy()
	{
		Release ();
	} 

	public virtual void Update ()
	{
		foreach (var list in _behaviours) {
			for (int i = 0; i < list.Value.Count; i++) {
				list.Value[i].Update();
			}
		}
	}

	public void AddBehaviour(IActorBehaviour behaviour)
	{
		Type type = behaviour.GetType();
		if (behaviour.IsSingle && _behaviours.ContainsKey (type) && _behaviours [type].Count > 0) {
			behaviour.Release();
			return;
		}

		behaviour.OnRemoveCallback (OnRemoveBehaviour);
		if (!_behaviours.ContainsKey (type)) {
			_behaviours.Add(type, new List<IActorBehaviour>());
		}
		_behaviours[type].Add (behaviour);
		behaviour.Init ();
	}

	void OnRemoveBehaviour (IActorBehaviour behaviour)
	{
		Type type = behaviour.GetType();
		if (_behaviours.ContainsKey(type) && _behaviours[type].Contains (behaviour)) {
			_behaviours[type].Remove(behaviour);
		}
	}


	protected virtual void Release ()
	{
		OnDeath = null;
		OnDamage = null;

		foreach (var item in _behaviours) {
			for (int i = 0; i < item.Value.Count; i++) {
				item.Value[i].Release();
			}
		}

		_behaviours = null;
	}
}


