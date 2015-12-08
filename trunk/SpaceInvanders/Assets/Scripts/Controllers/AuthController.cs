using System;
using UnityEngine;
using System.Collections.Generic;
using Data;

public class AuthData
{
	public string username = string.Empty;
	public string password = string.Empty;
	public string email = string.Empty;
}

public class AuthController
{

	public void ShowAuthWindow(AuthState state = AuthState.LOGIN)
	{
		AuthData authData = new AuthData ();
		UserData user = DataProxy.Instance.WebDB.CurrentUser ();
		if (user != null){
			authData.email = user.email;
			authData.username = user.username;
		}
		AuthWindow.Show (new AuthWindowParams (state, authData));
	}

	public void Login(AuthData authData, Action OnLoginSuccess, Action OnLoginFail)
	{
		if (!DataProxy.Instance.CheckConnection ()) {
			OnConnectFail();
			return;
		} 
		DataProxy.Instance.WebDB.LogIn (authData.username, authData.password, OnLoginSuccess, OnLoginFail);
	}

	public void Signup (AuthData authData, Action OnSignUpSuccess, Action OnSignUpFail)
	{
		if (!DataProxy.Instance.CheckConnection ()) {
			OnConnectFail();
			return;
		} 
		DataProxy.Instance.WebDB.SignUp (authData, OnSignUpSuccess, OnSignUpFail);
	}

	void OnConnectFail()
	{
		InfoWindow.Show ("Server connection failed!");
	}

}


