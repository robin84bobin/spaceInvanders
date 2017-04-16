using System;
using Assets.Scripts.Controllers;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Windows.AuthWindow
{
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

        public override void Init (AuthData authData_)
        {
            base.authData = authData_;
		
            usernameInput.text = base.authData.username;
            emailInput.text = base.authData.email;
            passwordInput.text = base.authData.password;
            errorText.text = string.Empty;

            usernameInput.onValueChanged.AddListener (OnUserNameChange);
            emailInput.onValueChanged.AddListener (OnEmailChange);
            passwordInput.onValueChanged.AddListener (OnPasswordChange);
        }

        public override void OnEnterState ()
        {
            this.gameObject.SetActive (true);

            logInButton.onClick.AddListener ( () => { OnLogInClick(authData);});
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
}
