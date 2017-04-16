using System;
using System.Collections.Generic;
using Assets.Scripts.Data.User;
using Assets.Scripts.Network;
using Assets.Scripts.UI.Windows.AuthWindow;
using Assets.Scripts.UI.Windows.InfoWindows;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class AuthData
    {
        public string username = string.Empty;
        public string password = string.Empty;
        public string email = string.Empty;
    }

    public class AuthController
    {

        public void ShowAuthWindow(AuthState state_ = AuthState.LOGIN)
        {
            AuthData authData = new AuthData ();
            UserData user = DataProxy.Instance.WebDb.CurrentUser ();
            if (user != null){
                authData.email = user.email;
                authData.username = user.username;
            }
            AuthWindow.Show (new AuthWindowParams (state_, authData));
        }

        public void Login(AuthData authData_, Action onLoginSuccess_, Action onLoginFail_)
        {
            if (!DataProxy.Instance.CheckConnection ()) {
                OnConnectFail();
                return;
            } 
            DataProxy.Instance.WebDb.LogIn (authData_.username, authData_.password, onLoginSuccess_, onLoginFail_);
        }

        public void Signup (AuthData authData_, Action onSignUpSuccess_, Action onSignUpFail_)
        {
            if (!DataProxy.Instance.CheckConnection ()) {
                OnConnectFail();
                return;
            } 
            DataProxy.Instance.WebDb.SignUp (authData_, onSignUpSuccess_, onSignUpFail_);
        }

        void OnConnectFail()
        {
            InfoWindow.Show ("Server connection failed!");
        }

    }
}