using Assets.Scripts.Data.Attributes;
using Assets.Scripts.ModelComponents.Skills;

namespace Assets.Scripts.Data.DataSource.Impacts
{
    public class SkillImpactData : ImpactData
    {
        [DbField]
        public string SkillsString { get; set; }

        [DbField]
        public double Value { get; set; }

        [DbField]
        public string StrategyType { get; set; }


        public Skill[] Skills
        {
            get
            {
                var skillsstr = SkillsString.Split(',');
                var skills = new Skill[skillsstr.Length];

                return skills;
            }
        }
    }
}