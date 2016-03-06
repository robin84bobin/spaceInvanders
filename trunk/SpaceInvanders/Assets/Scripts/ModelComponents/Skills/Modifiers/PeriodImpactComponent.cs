using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Common;
using Assets.Scripts.ModelComponents.Entities;
using Assets.Scripts.ModelComponents.Impacts;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Skills.Modifiers
{
    /// <summary>
    /// Applies impacts on timer execute
    /// </summary>
    
    public class PeriodImpactComponent : BaseComponent
    {
        private BaseEntityModel _target;
        private readonly Impact[] _impacts;
        private readonly TimerComponent _timer;

        public PeriodImpactComponent(Impact[] impacts_, TimerData timerData_ = null)
        {
            _impacts = impacts_;

            if (timerData_ != null) {
               _timer = new TimerComponent(timerData_);
               _timer.OnPeriod(Apply);
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
            _target = (BaseEntityModel) Parent;
        }

        protected override void OnRelease()
        {
            base.OnRelease();
         
            _target = null;
        }
    }
}