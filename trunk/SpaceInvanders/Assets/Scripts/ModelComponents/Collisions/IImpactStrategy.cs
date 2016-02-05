using System.Collections.Generic;
using Assets.Scripts.ModelComponents.Skills;

namespace Assets.Scripts.ModelComponents.Collisions
{
    public interface IImpactStrategy
    {
        void Apply(double value_, string[] affectedSkills_, Dictionary<string, Skill> skills_);
    }
}