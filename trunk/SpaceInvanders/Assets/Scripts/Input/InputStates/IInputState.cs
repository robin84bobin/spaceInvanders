using System;
using UnityEngine;

/// <summary>
/// Input state.
/// Store info used by input manager at current moment.
/// </summary>
public interface IInputState
{
	CursorLockMode CursorLockMode {get;}
}


