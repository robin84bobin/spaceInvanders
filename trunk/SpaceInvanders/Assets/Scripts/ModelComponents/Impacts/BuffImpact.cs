using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Actors;
using Assets.Scripts.ModelComponents.Skills.Modifiers;

namespace Assets.Scripts.ModelComponents.Impacts
{
    public class BuffImpact : IImpact
    {
        private readonly IImpact[] _impacts;
        private TimerData _timerData;

        public BuffImpact( TimerData timerData_, params IImpact[] impacts_)
        {
            _impacts = impacts_;
            _timerData = timerData_;
        }

        public void Apply(BaseActorModel actor_)
        {
            var buff = new BuffComponent(_impacts, _timerData);
           // buff.Target = actor_;
            actor_.AddComponent(buff);
        }
    }
}