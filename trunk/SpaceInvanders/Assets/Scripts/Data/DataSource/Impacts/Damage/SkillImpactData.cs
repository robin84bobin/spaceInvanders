using Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies;

namespace Assets.Scripts.Data.DataSource.Impacts.Damage
{
    public class SkillImpactData : BaseData
    {
        public string name;
        public float value;
        public string[] skills;
        public ISkillModifyStrategy strategy;
    }
}
