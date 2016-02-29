using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Actors;
using Assets.Scripts.ModelComponents.Skills.Modifiers;

namespace Assets.Scripts.ModelComponents.Impacts
{
    public class PeriodImpact : IImpact
    {
        private readonly IImpact[] _impacts;
        private readonly TimerData _timerData;

        public PeriodImpact( TimerData timerData_, params IImpact[] impacts_)
        {
            _impacts = impacts_;
            _timerData = timerData_;
        }

        public void Apply(BaseActorModel actor_)
        {
            var timerImpactComponent = new PeriodImpactComponent(_impacts, _timerData);
            actor_.AddComponent(timerImpactComponent);
        }
    }
}