using System.Collections;
using Assets.Scripts.CommonComponents.StateSwitcher;
using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts.UI.Windows.AuthWindow
{
    public enum AuthState
    {
        LOGIN,
        SIGNUP
    }

    public class AuthWindowParams : WindowParams
    {
        public AuthData authData;
        public AuthState startState;

        public AuthWindowParams(AuthState state_, AuthData authData_)
        {
            this.startState = state_;
            this.authData = authData_;
        }
    }

    public class AuthWindow : BaseWindow 
    {
        public LoginAuthState loginState;
        public SignupAuthState signupState;

        private BaseStateMachine<AuthState, AuthWindowState> _stateSwitcher;
	
        private AuthData _authData;

        public static void Show (AuthWindowParams parameters_ = null)
        {
            Main.Inst.windows.Show(	"AuthWindow", parameters_);
        }

        public override void OnShowComplete(WindowParams param_ = null)
        {
            base.OnShowComplete (param_);

            AuthState startState;
            if (windowsParameters != null) {
                AuthWindowParams authParams = (AuthWindowParams)windowsParameters;
                startState = authParams.startState;
                _authData = authParams.authData;
            } else {
                startState = AuthState.LOGIN;
                _authData = new AuthData ();
            }

            loginState.OnSignUpClick += (AuthData authData_) => { SwitchState( AuthState.SIGNUP, authData_);};
            signupState.OnLogInClick += (AuthData authData_) => { SwitchState( AuthState.LOGIN, authData_);};

            _stateSwitcher = new BaseStateMachine<AuthState, AuthWindowState> ();
            _stateSwitcher.Add (AuthState.LOGIN, loginState);
            _stateSwitcher.Add (AuthState.SIGNUP, signupState);

            SwitchState (startState, _authData);
        }



        void SwitchState (AuthState state_, AuthData authData_)
        {
            _authData = authData_;
            _stateSwitcher.SetState (state_);
            _stateSwitcher.CurrentState.Init (_authData);
        }
    }
}