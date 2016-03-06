using Assets.Scripts.Data;
using Assets.Scripts.ModelComponents.Entities;
using Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies;

namespace Assets.Scripts.ModelComponents.Impacts
{
    public class SkillImpact : Impact
    {
        private readonly string[] _targetSkills;
        private readonly double _impactValue;

        public ISkillModifyStrategy Strategy { private get; set; }


        public SkillImpact(string[] targetTypes, double impactValue_, params string[] targetSkills_ ) 
        {
            this.targetTypes = targetTypes;
            _targetSkills = targetSkills_;
            _impactValue = impactValue_;
        }

        protected override void Execute(BaseEntityModel entity_)
        {
            Strategy.Apply(_impactValue, _targetSkills, entity_.Skills);
        }
    }
}