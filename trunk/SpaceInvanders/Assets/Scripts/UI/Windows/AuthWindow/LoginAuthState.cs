using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class LoginAuthState : AuthWindowState 
{
	public event Action<AuthData> OnSignUpClick = delegate {};

	public InputField emailInput;
	public InputField passwordInput;

	public Button submitButton;
	public Text errorText;
	public Button signUpButton;

	#region implemented abstract members of AuthWindowState

	public override void Init (AuthData authData)
	{
		_authData = authData;
		
		emailInput.text = _authData.email;
		passwordInput.text = _authData.password;
		errorText.text = string.Empty;

		emailInput.onValueChanged.AddListener (onEmailChange);
		passwordInput.onValueChanged.AddListener (onPasswordChange);
	}

	public override void OnEnterState ()
	{
		this.gameObject.SetActive (true);

		signUpButton.onClick.AddListener ( () => { OnSignUpClick(_authData);});
		submitButton.onClick.AddListener ( Submit );
	}

	public override void OnExitState ()
	{
		this.gameObject.SetActive (false);

		signUpButton.onClick.RemoveAllListeners ();
		emailInput.onValueChanged.RemoveAllListeners();
		passwordInput.onValueChanged.RemoveAllListeners();

		emailInput.text = string.Empty;
		passwordInput.text = string.Empty;
		errorText.text = string.Empty;
	}

	#endregion

	void Submit ()
	{
		Main.inst.auth.Login (_authData, OnLoginSuccess, OnLoginFail);
	}

	void OnLoginSuccess ()
	{
		throw new NotImplementedException ();
	}

	void OnLoginFail ()
	{
		throw new NotImplementedException ();
	}
}
