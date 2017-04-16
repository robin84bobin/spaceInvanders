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
            for (int i = 0; i < _impacts.Length; i++) {
                Parent.SendMessage("ApplyImpact", _impacts[i]);
            }
        }
    }
}