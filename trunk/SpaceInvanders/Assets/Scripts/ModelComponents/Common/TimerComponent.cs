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

        private float _targetTime;
        private float _finishTime;

        private Action _callback;
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
        }

        public TimerComponent OnStart(Action startCallback_)
        {
            _startCallback = startCallback_;
            return this;
        }

        public TimerComponent OnTargetTime(Action callback_)
        {
            _callback = callback_;
            return this;
        }

        public TimerComponent OnComplete(Action finishCallback_)
        {
            _finishCallback = finishCallback_;
            return this;
        }

        private bool _firstStart = true;
        private bool _removeOnComplete = false;

        public void Start()
        {
            Unlock();

            _firstStart = false;

            _finishTime = _totalTime + Time.time;

            if (_delay > 0){
                _targetTime = Time.time + _delay;
                return;
            }

            if (_period > 0) {
                _targetTime = _period + Time.time;
            }
            else {
                _targetTime = _totalTime + Time.time;
            }

            Execute();
        }

        /*  private void OnFirstStart()
          {
              _firstStart = false;
              _finishTime = _totalTime + Time.time;
              if (_delay > 0){
                  _targetTime = Time.time + _delay;
                  return;
              }
              _targetTime = _totalTime + Time.time;

              Execute();
          }
          */

        protected override void OnUpdate()
        {
            if (_targetTime > 0 && Time.time >= _targetTime) {
                Execute();
            }
            else
            if (_finishTime > 0 && Time.time >= _finishTime) {
                Complete();
            }
        }

        private void Execute()
        {
            if (_firstStart) {
                if (_startCallback != null){
                    _startCallback.Invoke();
                }
                else
                    if (_callback != null)
                    {
                        _callback.Invoke();
                    }
                _firstStart = false;
            }
            else
            if (_callback != null){
                _callback.Invoke();
            }

            if (_period > 0) {
                _targetTime = _period + Time.time;
            }
        }

        private void Complete()
        {
            _targetTime = 0f;

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
            _callback = null;
            _startCallback = null;
            _finishCallback = null;
        }



    }
}
