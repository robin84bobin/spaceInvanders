using Assets.Scripts.Data.DataSource;

namespace Assets.Scripts.ModelComponents.Actors
{
    public class BulletModel : BaseActorModel
    {
        public float speed = 50f;

        public BulletModel(BulletData data_):base(data_)
        {
           
        }
    }
}

