using UnityEngine;
using System.Collections;

public enum AuthState
{
	LOGIN,
	SIGNUP
}

public class AuthWindow : BaseWindow 
{
	public LoginAuthState loginState;
	public SignupAuthState signupState;

	private BaseStateSwitcher<AuthState, AuthWindowState> _stateSwitcher;

	private AuthData _authData;

	public static void Show ()
	{
		Main.inst.windows.Show(	"AuthWindow");
	}

	public override void OnShowComplete(WindowParams param = null)
	{
		base.OnShowComplete (param);

		loginState.OnSignUpClick += (AuthData authData) => { SwitchState(AuthState.SIGNUP, authData);};
		signupState.OnLogInClick += (AuthData authData) => { SwitchState(AuthState.LOGIN, authData);};

		_stateSwitcher = new BaseStateSwitcher<AuthState, AuthWindowState> ();
		_stateSwitcher.Add (AuthState.LOGIN, loginState);
		_stateSwitcher.Add (AuthState.SIGNUP, signupState);

		_authData = new AuthData (); //TODO get auth data from WindowParams
		SwitchState (AuthState.LOGIN, _authData);
	}

	void SwitchState (AuthState state, AuthData authData)
	{
		_authData = authData;
		_stateSwitcher.SetState (state);
		_stateSwitcher.CurrentState.Init (_authData);
	}
}
