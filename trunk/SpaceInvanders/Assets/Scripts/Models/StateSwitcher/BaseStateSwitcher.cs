//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.34209
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

public class BaseStateSwitcher <K,T> where T:IBaseState
{
	private Dictionary<K,T> _states;

	private T _currentState;
	public T CurrentState {
		get { return _currentState;}
	}

	public BaseStateSwitcher()
	{
		_states = new Dictionary<K, T> ();
	}

	public void Add(K key,T value)
	{
		_states.Add(key,value);
	}

	public void Remove(K key)
	{
		_states.Remove (key);
	}

	public void SetState(K key)
	{
		if (_currentState != null) {
			_currentState.OnExitState();
		}

		_currentState = _states [key];
		_currentState.OnEnterState ();
	}
}


