using Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies;

namespace Assets.Scripts.ModelComponents.Skills.Modifiers
{
    public class HitSkillModifier : SkillModifier
    {
        public HitSkillModifier(double value_)
        {
            value = value_;
            modifyStrategy = ModifyStrategy.Serial;
            skills = new string[2] { SKILLS.ARMOR, SKILLS.HEALTH };
        }
    }
}