using System.Collections.Generic;
using Assets.Scripts.ModelComponents.Actors;
using Assets.Scripts.ModelComponents.Skills;
using Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies;

namespace Assets.Scripts.ModelComponents.Collisions
{
    public class ImpactInfo
    {
        private readonly string[] _affectedSkills;
        private readonly double _impactValue;

        public IModifyStrategy modifyStrategy;

        public ImpactInfo(double impactValue_, params string[] affectedSkills_ )
        {
            _affectedSkills = affectedSkills_;
            _impactValue = impactValue_;
        }

        public void Apply(Dictionary<string, Skill> skills_)
        {
            modifyStrategy.Apply(_impactValue, _affectedSkills, skills_);
        }

        public void Apply(BaseActorModel actor_)
        {
           
        }
    }
}