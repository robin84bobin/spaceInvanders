using System;
using Assets.Scripts.Data.DataSource;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Common
{
    public class TimerComponent : BaseComponent
    {
        private readonly float _delay;
        private readonly float _period;
        private readonly float _totalTime;

        private float _startTime;
        private float _periodTime;
        private float _finishTime;

        private Action _periodCallback;
        private Action _startCallback;
        private Action _finishCallback;


        public TimerComponent(float totalTime_, float period_ = 0f, float delay_ = 0f)
        {
            _delay = delay_;
            _period = period_;
            _totalTime = totalTime_;
        }

        public TimerComponent(TimerData timerData_)
        {
            _delay = timerData_.delay;
            _period = timerData_.period;
            _totalTime = timerData_.totalTime;
            _timerId = timerData_.objectId;
        }

        public TimerComponent OnStart(Action startCallback_)
        {
            _startCallback = startCallback_;
            return this;
        }

        public TimerComponent OnPeriod(Action periodCallback_)
        {
            _periodCallback = periodCallback_;
            return this;
        }

        public TimerComponent OnComplete(Action finishCallback_)
        {
            _finishCallback = finishCallback_;
            return this;
        }

        private bool _firstStart = true;
        private bool _removeOnComplete = false;
        private string _timerId;

        public void Start()
        {
            Unlock();
            _firstStart = false;
           
            _startTime = Time.time + _delay;
            _finishTime = _totalTime + Time.time;
            if (_period > 0) {
                _periodTime = _period + Time.time;
            }

           // Execute();
        }


        protected override void OnUpdate()
        {
            if (_startTime > 0 && Time.time >= _startTime) {
                OnStart();
            }

            if (_periodTime > 0 && Time.time >= _periodTime) {
                OnPeriod();
            }
            
            if (_finishTime > 0 && Time.time >= _finishTime) {
                Complete();
            }
        }

        private void OnStart()
        {
            if (_startCallback != null)
            {
                _startCallback.Invoke();
            }

            _startTime = 0f;
        }

        private void OnPeriod()
        {
            if (_periodCallback != null){
                _periodCallback.Invoke();
            }

            if (_period > 0) {
                _periodTime = _period + Time.time;
            }
        }

        private void Complete()
        {
            _periodTime = 0f;
            _finishTime = 0f;

            if (_finishCallback != null) {
                _finishCallback.Invoke();
            }
            
            if (_removeOnComplete) {
                Remove();
            }
        }

        protected override void OnInit ()
        {

        }

        protected override void OnRelease ()
        {
            _periodCallback = null;
            _startCallback = null;
            _finishCallback = null;
        }



    }
}
