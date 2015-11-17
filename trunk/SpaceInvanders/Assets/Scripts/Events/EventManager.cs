using System;
using System.Collections.Generic;
using UnityEngine;

public class EventParam{
	
}


public sealed class EventManager : MonoBehaviour
{
	private static EventManager _instance;
	public static EventManager Instance {
		get {
			return _instance;
		}
	}

	//private Dictionary<string, List<Action<EventParam>>> _callbacks;// = new Dictionary<string, List<Action<EventParam>>>();
	private System.Object event_lock = new System.Object();

	void Awake()
	{
		_instance = this;
		//_callbacks = new Dictionary<string, List<Action<EventParam>>>();
	}

/*	public void Subscribe(string eventName, Action<EventParam> callback)
	{
			if (!_callbacks.ContainsKey(eventName)){
				_callbacks.Add(eventName, new List<Action<EventParam>>());
			}

			if (_callbacks[eventName].Contains(callback)){
				Debug.LogWarning(string.Format ("Dublicate event '{0}' subscription callback: {1}", eventName, callback.ToString()));
				return;
			}

			_callbacks[eventName].Add(callback);
	}

	public void Unsubscribe(string eventName, Action<EventParam> callback)
	{
			if (!_callbacks.ContainsKey(eventName)){
				return;
			}

			if (_callbacks[eventName].Contains(callback)){
				_callbacks[eventName].Remove(callback);
			}
	}*/

	public static T Get<T>() where T: new ()
	{
		return _instance._getEvent<T> ();
	}

	private readonly Dictionary<Type, object> _events = new Dictionary<Type, object>();
	private TEvent _getEvent<TEvent>() where TEvent: new ()
	{
		lock(event_lock){
			var type = typeof(TEvent);

			object result_event = null;
			if (!_events.TryGetValue(type, out result_event)){
				result_event = new TEvent();
				_events.Add( type, result_event);
				return (TEvent)result_event;
			}
			else{
				return (TEvent)result_event;
			}
		}
	}


	private List<IEventCallbackWrapper> _eventParamQueueList = new List<IEventCallbackWrapper>();
	public void AddToQueue(IEventCallbackWrapper callbackWrapper)
	{
		lock (event_lock) {
			_eventParamQueueList.Add(callbackWrapper);
		}
	}


	void Update()
	{
		CheckEventQueue();
	}

	void CheckEventQueue ()
	{
		lock(event_lock){
			while (_eventParamQueueList.Count > 0) {
				 _eventParamQueueList[0].Execute();
				_eventParamQueueList.RemoveAt(0);
			}
		}
	}
}


public interface IEventCallbackWrapper
{
	void Execute();
}

public sealed class EventCallbackWrapper : IEventCallbackWrapper
{
	private Action _action;

	public EventCallbackWrapper(Action action)
	{
		_action = action;
	}

	public void Execute()
	{
		if (_action != null) {
			_action();
		}
	}
}

public sealed class EventParamCallbackWrapper<TParam> : IEventCallbackWrapper
{
	private Action<TParam> _action;
	private TParam _eventParam;

	public EventParamCallbackWrapper(Action<TParam> action, TParam eventParam)
	{
		_action = action;
		_eventParam = eventParam;
	}

	public void Execute()
	{
		if (_action != null) {
			_action(_eventParam);
		}
	}
}

