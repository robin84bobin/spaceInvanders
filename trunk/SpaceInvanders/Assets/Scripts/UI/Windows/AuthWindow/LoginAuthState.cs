using System;
using Assets.Scripts.Controllers;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Windows.AuthWindow
{
    public class LoginAuthState : AuthWindowState 
    {
        public event Action<AuthData> OnSignUpClick = delegate {};

        public InputField emailInput;
        public InputField passwordInput;

        public Button submitButton;
        public Text errorText;
        public Button signUpButton;

        #region implemented abstract members of AuthWindowState

        public override void Init (AuthData authData_)
        {
            base.authData = authData_;
		
            emailInput.text = base.authData.email;
            passwordInput.text = base.authData.password;
            errorText.text = string.Empty;

            emailInput.onValueChanged.AddListener (OnEmailChange);
            passwordInput.onValueChanged.AddListener (OnPasswordChange);
        }

        public override void OnEnterState ()
        {
            this.gameObject.SetActive (true);

            signUpButton.onClick.AddListener ( () => { OnSignUpClick(authData);});
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
            Main.Inst.auth.Login (authData, OnLoginSuccess, OnLoginFail);
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
}
