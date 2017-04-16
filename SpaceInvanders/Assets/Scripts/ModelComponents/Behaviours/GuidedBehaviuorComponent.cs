using System;
using System.Collections.Generic;
using Assets.Scripts.Events;
using Assets.Scripts.Events.CustomEvents;
using Assets.Scripts.Input;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Behaviours
{
    public class GuidedBehaviuorComponent : BaseComponent
    {
        private Dictionary<KeyCode, Action> _keyActions; 

        protected override void OnInit()
        {
            EventManager.Get<MoveControlsEvent>().Subscribe(OnUpdateControl);
            EventManager.Get<ButtonEvent>().Subscribe(OnButton);

            _keyActions = new Dictionary<KeyCode, Action>() {
                { InputManager.fireButton, () => Parent.SendMessage("Attack") }
            };
        }

        private void OnButton(KeyCode obj)
        {
            if (locked){
                return;
            }
            if (_keyActions.ContainsKey(obj)) {
                _keyActions[obj].Invoke();
            }
        }

        private void OnUpdateControl(Vector3 moveVector_)
        {
            if (locked) {
                return;
            }
            Parent.SendMessage("Move", moveVector_);
        }

        protected override void OnRelease()
        {
            EventManager.Get<MoveControlsEvent>().Unsubscribe(OnUpdateControl);
        }
    }
}