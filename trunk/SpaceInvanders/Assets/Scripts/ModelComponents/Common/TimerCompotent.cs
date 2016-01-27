using System;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Common
{
    public class TimerComponent : BaseComponent
    {
        private float _targetTime;
        private Action _callback;

        public TimerComponent(float deltaTime_, Action callback_)
        {
            _targetTime = deltaTime_ + Time.time;
            _callback = callback_;
        }

        protected override void OnUpdate()
        {
            if (Time.time >= _targetTime) {
                if (_callback!= null){
                    _callback.Invoke();
                }
            }
        }

        protected override void OnInit ()
        {

        }

        protected override void OnRelease ()
        {
            _callback = null;
        }

    }
}
