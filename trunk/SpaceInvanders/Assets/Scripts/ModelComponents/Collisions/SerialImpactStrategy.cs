using System.Collections.Generic;
using Assets.Scripts.ModelComponents.Skills;

namespace Assets.Scripts.ModelComponents.Collisions
{
    /// <summary>
    /// Apply impact in order of affectedSkills array
    /// Apply residual value to next skill
    /// </summary>
    public class SerialImpactStrategy : IImpactStrategy
    {
        private string _skillName = string.Empty;
        private double _remainderValue = 0;

        public void Apply(double value_, string[] affectedSkills_, Dictionary<string, Skill> skills_)
        {
            _remainderValue = value_;

            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < affectedSkills_.Length; i++) {
                _skillName = affectedSkills_[i];
                if (skills_.ContainsKey(_skillName)) {
                    _remainderValue = skills_[_skillName].ChangeValue(_remainderValue);
                }
            }

        }
    }
}