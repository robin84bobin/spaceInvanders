using System.Collections.Generic;
using Assets.Scripts.ModelComponents.Skills;

namespace Assets.Scripts.ModelComponents.Collisions
{
    public class ImpactInfo
    {
        private readonly string[] _affectedSkills;
        private readonly double _impactValue;

        public IImpactStrategy impactStrategy;

        public ImpactInfo(double impactValue_, params string[] affectedSkills_ )
        {
            _affectedSkills = affectedSkills_;
            _impactValue = impactValue_;
        }

        public void Apply(Dictionary<string, Skill> skills_)
        {
            impactStrategy.Apply(_impactValue, _affectedSkills, skills_);
        }
    }
}