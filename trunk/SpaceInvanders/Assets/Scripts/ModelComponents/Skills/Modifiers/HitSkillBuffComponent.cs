using Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies;

namespace Assets.Scripts.ModelComponents.Skills.Modifiers
{
    public class HitSkillBuffComponent : SkillBuffComponent
    {
        public HitSkillBuffComponent(double value_)
        {
            value = value_;
            modifyStrategy = ModifyStrategy.Serial;
            skills = new string[2] { SKILLS.ARMOR, SKILLS.HEALTH };
        }
    }
}