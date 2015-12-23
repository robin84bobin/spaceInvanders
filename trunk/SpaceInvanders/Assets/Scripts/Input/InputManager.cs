using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum InputMode
{
	UI,
	GAME
}

/// <summary>
/// Input manager.
/// Just an input manager...
/// </summary>
public sealed class InputManager : MonoBehaviour 
{
	public string horizontalAxis = "Horizontal";
	public string verticalAxis = "Vertical";
	public KeyCode fireButton = KeyCode.Space;

	private Dictionary<InputMode,IInputState> _states;
	private IInputState _currentState;

	void Awake()
	{
		_states = new Dictionary<InputMode, IInputState>()
		{
			{InputMode.UI, new UIInputState()},
			{InputMode.GAME, new GameInputState()}
		};
	}

	public bool FirePressed{
		get{
			return Input.GetKey (Main.inst.input.fireButton);
		}
	}

	float xInput;
	float yInput;
	Vector3 _moveVector;
	public Vector3 GetMoveVector ()
	{
		xInput = Input.GetAxis ( Main.inst.input.horizontalAxis );
		yInput = Input.GetAxis ( Main.inst.input.verticalAxis );
		return new Vector3(xInput, yInput, 0f);
	}

	public void SetState(InputMode mode)
	{
		if (_currentState == _states[mode]){
			return;
		}
		_currentState = _states[mode];

		Cursor.lockState = _currentState.CursorLockMode;
		Cursor.visible = _currentState.CursorLockMode != CursorLockMode.Locked;
	}


	void Update()
	{
		_moveVector = GetMoveVector();
		if (_moveVector != Vector3.zero) {
			EventManager.Get<MoveControlsEvent>().Publish(_moveVector);
		}
	}
}
