using System;
using UnityEngine;

public class UpdateControlsEventArgs : EventParam
{
	private Vector3 _moveVector;
	public Vector3 moveVector {
		get {
			return _moveVector;
		}
	}

	public UpdateControlsEventArgs (Vector3 moveVector)
	{
		_moveVector = moveVector;
	}
}


