using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Events;
using Assets.Scripts.Events.CustomEvents;
using Assets.Scripts.Input.InputStates;
using UnityEngine;

namespace Assets.Scripts.Input
{
    public enum InputMode
    {
        UI,
        GAME
    }

    /// <summary>
    /// Input manager.
    /// Just an input manager...
    /// </summary>
    public sealed class InputManager : MonoBehaviour 
    {
        public string horizontalAxis = "Horizontal";
        public string verticalAxis = "Vertical";
        public static KeyCode fireButton = KeyCode.Space;

        private Dictionary<InputMode,IInputState> _states;
        private IInputState _currentState;

        void Awake()
        {
            _states = new Dictionary<InputMode, IInputState>()
            {
                {InputMode.UI, new UiInputState()},
                {InputMode.GAME, new GameInputState()}
            };
        }

        public bool FirePressed{
            get{
                return UnityEngine.Input.GetKey (InputManager.fireButton);
            }
        }

        float _xInput;
        float _yInput;
        Vector3 _moveVector;
        public Vector3 GetMoveVector ()
        {
            _xInput = UnityEngine.Input.GetAxis ( Main.Inst.input.horizontalAxis );
            _yInput = UnityEngine.Input.GetAxis ( Main.Inst.input.verticalAxis );
            return new Vector3(_xInput, _yInput, 0f);
        }

        public void SetState(InputMode mode_)
        {
            if (_currentState == _states[mode_]){
                return;
            }
            _currentState = _states[mode_];

            Cursor.lockState = _currentState.CursorLockMode;
            Cursor.visible = _currentState.CursorLockMode != CursorLockMode.Locked;
        }


        void Update()
        {
            _moveVector = GetMoveVector();
            if (_moveVector != Vector3.zero) {
                EventManager.Get<MoveControlsEvent>().Publish(_moveVector);
            }

            CheckButton(fireButton);
        }

        void CheckButton(KeyCode key_)
        {
            if (UnityEngine.Input.GetKey(key_))
            {
                EventManager.Get<ButtonEvent>().Publish(key_);
            }
        }
    }
}