using Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies;

namespace Assets.Scripts.Data.DataSource.Impacts.Damage
{
    public class SkillImpactData : BaseData
    {
        public string name;
        public string[] skills;
        public float value;
        public ISkillModifyStrategy strategy;
        public string timerId;
    }
}
