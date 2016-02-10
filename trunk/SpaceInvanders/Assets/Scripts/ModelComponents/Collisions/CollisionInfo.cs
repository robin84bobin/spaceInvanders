using Assets.Scripts.ModelComponents.Actors;
using Assets.Scripts.ModelComponents.Skills.Modifiers;

namespace Assets.Scripts.ModelComponents.Collisions
{
    public class CollisionInfo
    {
        private readonly SkillModifier[] _skillModifiers;

        public CollisionInfo(params SkillModifier[] skillModifiers_)
        {
            _skillModifiers = skillModifiers_;
        }

        public void Apply(BaseActorModel actor_)
        {
            foreach (var modifier in _skillModifiers) {
                modifier.Apply(actor_);
            }
        }
    }
}