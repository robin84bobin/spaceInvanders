namespace Assets.Scripts.ModelComponents.Entities
{
    internal interface ISkillHolder
    {
        double GetSkill(string skill_);
        void SetSkill(string skill_, double value_);
    }
}