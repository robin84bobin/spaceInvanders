using Assets.Scripts.ModelComponents.Actors;
using Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies;

namespace Assets.Scripts.ModelComponents.Impacts
{
    public class SkillImpact : IImpact
    {
        private readonly string[] _affectedSkills;
        private readonly double _impactValue;

        public IModifyStrategy modifyStrategy;

        public SkillImpact(double impactValue_, params string[] affectedSkills_ )
        {
            _affectedSkills = affectedSkills_;
            _impactValue = impactValue_;
        }

        public void Apply(BaseActorModel actor_)
        {
           modifyStrategy.Apply(_impactValue, _affectedSkills, actor_.Skills);
        }
    }
}