using System.Collections.Generic;

namespace Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies
{
    public class ParallelModifyStrategy : ISkillModifyStrategy
    {
        private string _skillName;

        public void Apply(double value_, string[] targetSkills_, Dictionary<string, Skill> skills_)
        {
            for (int i = 0; i < targetSkills_.Length; i++)
            {
                _skillName = targetSkills_[i];
                if (skills_.ContainsKey(_skillName))
                {
                    skills_[_skillName].ChangeValue(value_);
                }
            }
        }
    }
}