namespace Assets.Scripts.ModelComponents.ActorComponents
{
    internal interface ISkillHolder
    {
        double GetSkill(string skill_);
        void SetSkill(string skill_, double value_);
    }
}