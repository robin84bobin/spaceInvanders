using System;
using UnityEngine;


public class GameInputState : IInputState
{
	#region IInputState implementation
	public CursorLockMode CursorLockMode {
		get {
			return CursorLockMode.Locked;
		}
	}
	#endregion
}

