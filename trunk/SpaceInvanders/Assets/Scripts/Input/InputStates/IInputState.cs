using UnityEngine;

namespace Assets.Scripts.Input.InputStates
{
    /// <summary>
    /// Input state.
    /// Store info used by input manager at current moment.
    /// </summary>
    public interface IInputState
    {
        CursorLockMode CursorLockMode {get;}
    }
}


