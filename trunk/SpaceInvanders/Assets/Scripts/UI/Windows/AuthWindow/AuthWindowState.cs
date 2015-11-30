using UnityEngine;
using System.Collections;


public abstract class AuthWindowState : MonoBehaviour , IBaseState
{
	protected AuthData _authData;
	public AuthData AuthData {
		get { return _authData;}
	}

	protected void onUserNameChange(string value)
	{
		_authData.username = value;
	}

	protected void onEmailChange(string value)
	{
		_authData.email = value;
	}

	protected void onPasswordChange(string value)
	{
		_authData.password = value;
	}

	public abstract void Init(AuthData authData);

	#region IBaseState implementation
	public abstract void OnEnterState ();
	public abstract void OnExitState ();
	#endregion
}
