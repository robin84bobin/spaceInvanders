using System.Collections.Generic;

namespace Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies
{
    public class ParallelModifyStrategy : ISkillModifyStrategy
    {
        private string _skillName;

        public void Apply(double value_, string[] skills_, Dictionary<string, Skill> targetSkills_)
        {
            for (int i = 0; i < skills_.Length; i++)
            {
                _skillName = skills_[i];
                if (targetSkills_.ContainsKey(_skillName))
                {
                   targetSkills_[_skillName].ChangeValue(value_);
                }
            }
        }
    }
}