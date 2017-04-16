using Assets.Scripts.CommonComponents.StateSwitcher;
using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts.UI.Windows.AuthWindow
{
    public abstract class AuthWindowState : MonoBehaviour , IBaseState
    {
        protected AuthData authData;
        public AuthData AuthData {
            get { return authData;}
        }

        protected void OnUserNameChange(string value_)
        {
            authData.username = value_;
        }

        protected void OnEmailChange(string value_)
        {
            authData.email = value_;
        }

        protected void OnPasswordChange(string value_)
        {
            authData.password = value_;
        }

        public abstract void Init(AuthData authData_);

        #region IBaseState implementation
        public abstract void OnEnterState ();
        public abstract void OnExitState ();
        #endregion
    }
}
