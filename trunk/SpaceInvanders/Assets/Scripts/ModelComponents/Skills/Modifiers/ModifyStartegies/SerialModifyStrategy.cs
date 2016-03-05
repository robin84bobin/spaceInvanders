using System.Collections.Generic;

namespace Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies
{
    /// <summary>
    /// Apply impact in order of affectedSkills array
    /// Apply residual value to next skill
    /// </summary>
    public class SerialModifyStrategy : ISkillModifyStrategy
    {
        private string _skillName = string.Empty;
        private double _remainderValue = 0;

        public void Apply(double value_, string[] targetSkills_, Dictionary<string, Skill> skills_)
        {
            _remainderValue = value_;

            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < targetSkills_.Length; i++) {
                _skillName = targetSkills_[i];
                if (skills_.ContainsKey(_skillName)) {
                    _remainderValue = skills_[_skillName].ChangeValue(_remainderValue);
                }
            }
        }
    }
}