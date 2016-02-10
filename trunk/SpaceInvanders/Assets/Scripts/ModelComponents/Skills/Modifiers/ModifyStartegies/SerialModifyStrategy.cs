using System.Collections.Generic;

namespace Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies
{
    /// <summary>
    /// Apply impact in order of affectedSkills array
    /// Apply residual value to next skill
    /// </summary>
    public class SerialModifyStrategy : IModifyStrategy
    {
        private string _skillName = string.Empty;
        private double _remainderValue = 0;

        public void Apply(double value_, string[] skills_, Dictionary<string, Skill> targetSkills_)
        {
            _remainderValue = value_;

            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < skills_.Length; i++) {
                _skillName = skills_[i];
                if (targetSkills_.ContainsKey(_skillName)) {
                    _remainderValue = targetSkills_[_skillName].ChangeValue(_remainderValue);
                }
            }

        }
    }
}