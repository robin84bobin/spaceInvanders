using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Data.DataSource.Impacts;
using Assets.Scripts.ModelComponents.Actors;
using Assets.Scripts.ModelComponents.Behaviours;
using Assets.Scripts.ModelComponents.Common;

namespace Assets.Scripts.ModelComponents.Impacts
{
    public class BehaviourImpact  : IImpact
    {
        private readonly TimerData _timerData;
        private readonly BehaviourImpactData _behaviourImpactData;

        public BehaviourImpact(BehaviourImpactData behaviourImpactData_)
        {
            _behaviourImpactData = behaviourImpactData_;
            _timerData = Main.Inst.Data.Get<TimerData>(_behaviourImpactData.timerId);
        }

        public void Apply(BaseActorModel actor_)
        {
            var behaviourComponent = BehaviourFactory.Instance.Create(_behaviourImpactData);
            if (_timerData != null) {
                TimerComponent timer = new TimerComponent(_timerData);
                timer.OnComplete(behaviourComponent.Remove);
                behaviourComponent.AddComponent(timer);
                timer.Start();
            }
            actor_.AddComponent(behaviourComponent);
        }
    }
}