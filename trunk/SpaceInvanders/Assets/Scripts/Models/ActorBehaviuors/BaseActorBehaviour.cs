using System;
using UnityEngine;

public interface IActorBehaviour
{
	void Lock ();
	void Unlock ();
	void Release();
	void Update();
	bool IsSingle { get; }
	void OnRemoveCallback (Action<IActorBehaviour> callback);
}

public abstract class BaseActorBehaviour<TOwner> : IActorBehaviour 
{
	public event Action<IActorBehaviour> RemoveMe;
	public event Action OnRelease;
	protected TOwner _owner;
	protected bool _locked = false;

	protected bool _isSingle = true;	
	public bool IsSingle{
		get {
			return _isSingle;
		}
	}

	public BaseActorBehaviour(TOwner owner)
	{
		//if (owner is TOwner) {
			_owner = owner;
		/*} else {
			Debug.LogWarning(string.Format("Wrong ActorBehaviour couple: {0} to {1}", owner.ToString(), this.ToString()));
			_locked = true;
			_needToRemove = true;
		}*/
	}

	public void OnRemoveCallback(Action<IActorBehaviour> callback)
	{
		RemoveMe = callback;
	}

	public void Lock()
	{
		_locked = true;
	}

	public void Unlock()
	{
		_locked = false;
	}

	public void Release()
	{
		Lock ();
		RemoveMe (this);
		RemoveMe = null;
		OnRelease ();
		OnRelease = null;
	}

	private bool _needToRemove = false;

	public virtual void Update()
	{
		if (_needToRemove) {
			Release();
		}

		if (_locked) {
			return;
		}
	}
}


