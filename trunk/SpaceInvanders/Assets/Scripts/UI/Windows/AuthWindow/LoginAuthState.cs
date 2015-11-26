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

		emailInput.onValueChange.AddListener (onEmailChange);
		passwordInput.onValueChange.AddListener (onPasswordChange);
	}

	public override void OnEnterState ()
	{
		this.gameObject.SetActive (true);

		signUpButton.onClick.AddListener ( () => { OnSignUpClick(_authData);});
	}

	public override void OnExitState ()
	{
		this.gameObject.SetActive (false);

		signUpButton.onClick.RemoveAllListeners ();
		emailInput.onValueChange.RemoveAllListeners();
		passwordInput.onValueChange.RemoveAllListeners();

		emailInput.text = string.Empty;
		passwordInput.text = string.Empty;
		errorText.text = string.Empty;
	}

	#endregion


}
