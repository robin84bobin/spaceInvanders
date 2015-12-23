using UnityEngine;
using System.Collections;

public enum AuthState
{
	LOGIN,
	SIGNUP
}

public class AuthWindowParams : WindowParams
{
	public AuthData authData;
	public AuthState startState;

	public AuthWindowParams(AuthState state, AuthData authData)
	{
		this.startState = state;
		this.authData = authData;
	}
}

public class AuthWindow : BaseWindow 
{
	public LoginAuthState loginState;
	public SignupAuthState signupState;

	private BaseStateMachine<AuthState, AuthWindowState> _stateSwitcher;
	
	private AuthData _authData;

	public static void Show (AuthWindowParams parameters = null)
	{
		Main.inst.windows.Show(	"AuthWindow", parameters);
	}

	public override void OnShowComplete(WindowParams param = null)
	{
		base.OnShowComplete (param);

		AuthState startState;
		if (_windowsParameters != null) {
			AuthWindowParams authParams = (AuthWindowParams)_windowsParameters;
			startState = authParams.startState;
			_authData = authParams.authData;
		} else {
			startState = AuthState.LOGIN;
			_authData = new AuthData ();
		}

		loginState.OnSignUpClick += (AuthData authData) => { SwitchState( AuthState.SIGNUP, authData);};
		signupState.OnLogInClick += (AuthData authData) => { SwitchState( AuthState.LOGIN, authData);};

		_stateSwitcher = new BaseStateMachine<AuthState, AuthWindowState> ();
		_stateSwitcher.Add (AuthState.LOGIN, loginState);
		_stateSwitcher.Add (AuthState.SIGNUP, signupState);

		SwitchState (startState, _authData);
	}



	void SwitchState (AuthState state, AuthData authData)
	{
		_authData = authData;
		_stateSwitcher.SetState (state);
		_stateSwitcher.CurrentState.Init (_authData);
	}
}
