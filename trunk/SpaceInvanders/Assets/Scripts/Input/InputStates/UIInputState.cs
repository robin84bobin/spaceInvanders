using System;
using UnityEngine;


public class UIInputState : IInputState
{
	#region IInputState implementation
	public CursorLockMode CursorLockMode {
		get {
			return CursorLockMode.None;
		}
	}
	#endregion
}

