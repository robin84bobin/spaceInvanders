using System;
using UnityEngine;


public class GuidedBehaviuorComponent: BaseComponent
{

	protected override void OnInit()
	{
		AddComponent( new TimerComponent(5f, Remove));
		EventManager.Get<MoveControlsEvent> ().Subscribe (OnUpdateControl);
	}

	void OnUpdateControl (Vector3 moveVector)
	{
		if (_locked) {
			return;
		}
		parent.SendMessage("Move",moveVector);
	}

	protected override void OnRelease()
	{
		EventManager.Get<MoveControlsEvent> ().Unsubscribe (OnUpdateControl);
	}
}


