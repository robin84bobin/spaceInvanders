using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Collisions;
using Assets.Scripts.ModelComponents.Skills.Modifiers;

namespace Assets.Scripts.ModelComponents.Actors
{
    public class BulletModel : BaseActorModel
    {
        public float speed = 50f;

        public BulletModel(BulletData data_) : base(data_) {}

        protected override void InitSkills()
        {
            //
        }

        protected override void InitCollisionInfo()
        {
            CollisionInfoData = new CollisionInfo(new HitSkillModifier(-10f));
        }
    }
}