using System;
using Assets.Scripts.Data;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Data.DataSource.Impacts;
using Assets.Scripts.ModelComponents.Behaviours;
using Assets.Scripts.ModelComponents.Common;
using Assets.Scripts.ModelComponents.Entities;

namespace Assets.Scripts.ModelComponents.Impacts
{
    public class BehaviourImpact  : Impact
    {
        private readonly TimerData _timerData;
        private readonly BehaviourImpactData _behaviourImpactData;

        public BehaviourImpact(BehaviourImpactData behaviourImpactData_)
        {
            _behaviourImpactData = behaviourImpactData_;
            targetTypes = _behaviourImpactData.targetTypes;
            _timerData = Main.Inst.Data.Get<TimerData>(_behaviourImpactData.timerId);
        }

        protected override void Execute(BaseEntityModel entity_)
        {
            var behaviourComponent = BehaviourFactory.Instance.Create(_behaviourImpactData);
            if (_timerData != null) {
                TimerComponent timer = new TimerComponent(_timerData);
                timer.OnComplete(behaviourComponent.Remove);
                behaviourComponent.AddComponent(timer);
                timer.Start();
            }
            entity_.AddComponent(behaviourComponent);
        }

    }
}