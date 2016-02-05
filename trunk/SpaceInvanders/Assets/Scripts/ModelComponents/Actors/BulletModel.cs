using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Collisions;

namespace Assets.Scripts.ModelComponents.Actors
{
    public class BulletModel : BaseActorModel
    {
        public float speed = 50f;

        public BulletModel(BulletData data_):base(data_)
        {
           
        }

        protected override void InitCollisionInfo()
        {
            ImpactInfo hitImpactInfo = new ImpactInfo(50);
            collisionInfo = new CollisionInfo(hitImpactInfo);
        }
    }
}

