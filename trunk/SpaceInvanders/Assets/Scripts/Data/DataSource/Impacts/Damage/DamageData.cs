using Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies;

namespace Assets.Scripts.Data.DataSource.Impacts.Damage
{
    public class DamageData : BaseData
    {
        public string name;
        public string[] skills;
        public float value;
        public IModifyStrategy strategy;
        public string timerId;
    }
}
