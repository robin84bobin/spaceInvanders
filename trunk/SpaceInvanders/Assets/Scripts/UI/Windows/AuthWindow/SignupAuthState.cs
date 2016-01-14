﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SignupAuthState : AuthWindowState 
{
	public event Action<AuthData> OnLogInClick = delegate {};

	public InputField usernameInput;
	public InputField emailInput;
	public InputField passwordInput;
	
	public Button submitButton;
	public Text errorText;
	public Button logInButton;

	#region implemented abstract members of AuthWindowState

	public override void Init (AuthData authData)
	{
		_authData = authData;
		
		usernameInput.text = _authData.username;
		emailInput.text = _authData.email;
		passwordInput.text = _authData.password;
		errorText.text = string.Empty;

		usernameInput.onValueChanged.AddListener (onUserNameChange);
		emailInput.onValueChanged.AddListener (onEmailChange);
		passwordInput.onValueChanged.AddListener (onPasswordChange);
	}

	public override void OnEnterState ()
	{
		this.gameObject.SetActive (true);

		logInButton.onClick.AddListener ( () => { OnLogInClick(_authData);});
	}

	public override void OnExitState ()
	{
		this.gameObject.SetActive (false);

		logInButton.onClick.RemoveAllListeners ();
		usernameInput.onValueChanged.RemoveAllListeners();
		emailInput.onValueChanged.RemoveAllListeners();
		passwordInput.onValueChanged.RemoveAllListeners();

		usernameInput.text = string.Empty;
		emailInput.text = string.Empty;
		passwordInput.text = string.Empty;
		errorText.text = string.Empty;
	}

	#endregion

}
