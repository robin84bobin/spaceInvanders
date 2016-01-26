using UnityEngine;

namespace Assets.Scripts.Input.InputStates
{
    public class GameInputState : IInputState
    {
        #region IInputState implementation
        public CursorLockMode CursorLockMode {
            get {
                return CursorLockMode.Locked;
            }
        }
        #endregion
    }
}

