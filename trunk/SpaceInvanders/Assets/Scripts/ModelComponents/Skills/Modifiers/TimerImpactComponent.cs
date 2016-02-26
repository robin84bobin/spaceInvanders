using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Actors;
using Assets.Scripts.ModelComponents.Common;
using Assets.Scripts.ModelComponents.Impacts;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Skills.Modifiers
{
    /// <summary>
    /// Applies impacts on timer execute
    /// </summary>
    
    public class TimerImpactComponent : BaseComponent
    {
        private BaseActorModel _target;
        private readonly IImpact[] _impacts;
        private readonly TimerComponent _timer;

        public TimerImpactComponent(IImpact[] impacts_, TimerData timerData_ = null)
        {
            _impacts = impacts_;

            if (timerData_ != null) {
               _timer = new TimerComponent(timerData_);
               _timer.OnTargetTime(Apply);
               _timer.OnComplete(Remove);
               AddComponent(_timer);
            }
        }

        protected override void OnInit()
        {
            if (_timer != null) {
                _timer.Start();
            }
            else {
                Apply();
            }
        }

        private void Apply()
        {
            if (_target == null) {
                Debug.LogError(this.GetType().Name + " has no target!");
                return;
            }

            for (int i = 0; i < _impacts.Length; i++) {
                _impacts[i].Apply(_target);
            }
        }

        protected override void OnSetParent()
        {
            base.OnSetParent();
            _target = (BaseActorModel) Parent;
        }

        protected override void OnRelease()
        {
            base.OnRelease();
         
            _target = null;
        }
    }
}