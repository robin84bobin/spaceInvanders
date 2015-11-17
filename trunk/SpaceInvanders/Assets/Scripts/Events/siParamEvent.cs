using System;
using System.Collections.Generic;
using UnityEngine;

public class siEvent
{
	private List<Action> _callbacks;

	public siEvent()
	{
		_callbacks = new List<Action> ();
	}
	
	public void Subscribe( Action callback)
	{
		if (_callbacks.Contains(callback)){
			Debug.LogWarning(string.Format ("Dublicate event '{0}' subscription callback: {1}", this.GetType().Name, callback.ToString()));
			return;
		}
		_callbacks.Add(callback);
	}
	
	public void Unsubscribe(Action callback)
	{
		if (!_callbacks.Contains(callback)){
			return;
		}
		
		if (_callbacks.Contains(callback)){
			_callbacks.Remove(callback);
		}
	}
	
	public void Publish()
	{
		for (int i = 0; i < _callbacks.Count; i++) {
			EventManager.Instance.AddToQueue( new EventCallbackWrapper(_callbacks[i]));
		}
	}
}

public class siParamEvent <TParam>
{
	private List<Action<TParam>> _callbacks;

	public siParamEvent()
	{
		_callbacks = new List<Action<TParam>> ();
	}

	public void Subscribe( Action<TParam> callback)
	{
		if (_callbacks.Contains(callback)){
			Debug.LogWarning(string.Format ("Dublicate event '{0}' subscription callback: {1}", this.GetType().Name, callback.ToString()));
			return;
		}
		
		_callbacks.Add(callback);
	}

	public void Unsubscribe(Action<TParam> callback)
	{
		if (!_callbacks.Contains(callback)){
			return;
		}
		
		if (_callbacks.Contains(callback)){
			_callbacks.Remove(callback);
		}
	}

	public void Publish( TParam eventParam)
	{
		for (int i = 0; i < _callbacks.Count; i++) {
			EventManager.Instance.AddToQueue( new EventParamCallbackWrapper<TParam>(_callbacks[i], eventParam));
		}
	}
}



