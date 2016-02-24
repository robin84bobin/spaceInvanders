using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Data.DataSource.Impacts;
using Assets.Scripts.ModelComponents.Actors;
using Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies;

namespace Assets.Scripts.ModelComponents.Impacts
{
    public class SkillImpact : IImpact
    {
        private readonly string[] _affectedSkills;
        private readonly double _impactValue;

        public ISkillModifyStrategy Strategy { private get; set; }
        public TimerData Timer { private get; set; }

        public SkillImpact(double impactValue_, params string[] affectedSkills_ )
        {
            _affectedSkills = affectedSkills_;
            _impactValue = impactValue_;
        }

        public void Apply(BaseActorModel actor_)
        {
            Strategy.Apply(_impactValue, _affectedSkills, actor_.Skills);
        }
    }
}