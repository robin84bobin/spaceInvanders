using System;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Common
{
    public class TimerComponent : BaseComponent
    {
        private readonly float _targetTime;
        private Action _callback;
        private Action _finishCallback;
        private Action _startCallback;

        public TimerComponent(float activeTime_, float period_ = 0f, float delay = 0f)
        {
            _targetTime = activeTime_ + Time.time;
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
