using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Collisions;
using Assets.Scripts.ModelComponents.Impacts;

namespace Assets.Scripts.ModelComponents.Entities
{
    public class BulletModel : BaseEntityModel
    {
        public float speed = 10f;
        private readonly BulletData _bulletData;

        public BulletModel(BulletData data_) : base(data_)
        {
            _bulletData = data_;
            base.Init();
        }

        protected override void OnInitSkills()
        {
            //
        }

        protected override void InitCollisionInfo()
        {
            if (_bulletData.impactInfos != null && _bulletData.impactInfos.Length > 0)
            {
                Impact[] impacts = new Impact[_bulletData.impactInfos.Length];
                for (int index = 0; index < _bulletData.impactInfos.Length; index++) {
                    var impactInfo = _bulletData.impactInfos[index];
                    impacts[index] = ImpactFactory.Instance.Create(impactInfo);
                }
                CollisionInfoData = new CollisionInfo(impacts);
            }
        }
    }
}