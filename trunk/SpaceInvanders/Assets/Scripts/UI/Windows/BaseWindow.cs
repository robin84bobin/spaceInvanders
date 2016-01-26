using UnityEngine;

namespace Assets.Scripts.UI.Windows
{
    public class BaseWindow : MonoBehaviour 
    {
        protected WindowParams windowsParameters;

        public void Hide ()
        {
            OnHide();
            Main.Inst.windows.HideWindow(this);
        }

        public virtual void OnShowComplete(WindowParams param_ = null)
        {
            windowsParameters = param_;
        }

        protected virtual void OnHide()
        {
        }
    }
}
