using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Actors;
using Assets.Scripts.ModelComponents.Skills.Modifiers;

namespace Assets.Scripts.ModelComponents.Impacts
{
    public class TimerImpact : IImpact
    {
        private readonly IImpact[] _impacts;
        private readonly TimerData _timerData;

        public TimerImpact( TimerData timerData_, params IImpact[] impacts_)
        {
            _impacts = impacts_;
            _timerData = timerData_;
        }

        public void Apply(BaseActorModel actor_)
        {
            var timerImpactComponent = new TimerImpactComponent(_impacts, _timerData);
            actor_.AddComponent(timerImpactComponent);
        }
    }
}