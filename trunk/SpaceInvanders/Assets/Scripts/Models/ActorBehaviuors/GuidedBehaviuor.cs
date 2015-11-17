using System;
using UnityEngine;

public class GuidedBehaviuor: BaseActorBehaviour<IGuided>
{
	public GuidedBehaviuor (IGuided owner): base(owner)
	{
		EventManager.Get<MoveControlsEvent> ().Subscribe (OnUpdateControl);
		OnRelease += onReleaseCompleted;
	}

	void OnUpdateControl (Vector3 moveVector)
	{
		if (_locked) return;

		_owner.MoveVector = moveVector;
	}

	void onReleaseCompleted()
	{
		_owner = null;
		EventManager.Get<MoveControlsEvent> ().Unsubscribe (OnUpdateControl);
	}
}


