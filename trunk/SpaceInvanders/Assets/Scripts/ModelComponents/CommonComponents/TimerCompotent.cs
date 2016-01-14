using System;
using UnityEngine;
using System.Collections.Generic;

public class TimerComponent : BaseComponent
{
	private float _targetTime;
	private Action _callback;

	public TimerComponent(float deltaTime, Action callback)
	{
		_targetTime = deltaTime + Time.time;
		_callback = callback;
	}

	protected override void OnUpdate()
	{
		if (Time.time >= _targetTime) {
			if (_callback!= null){
				_callback.Invoke();
			}
		}
	}

	protected override void OnInit ()
	{

	}

	protected override void OnRelease ()
	{
		_callback = null;
	}

}
