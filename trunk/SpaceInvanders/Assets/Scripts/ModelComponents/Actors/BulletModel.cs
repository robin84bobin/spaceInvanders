using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Collisions;
using Assets.Scripts.ModelComponents.Impacts;
using UnityEngine;


namespace Assets.Scripts.ModelComponents.Actors
{
    public class BulletModel : BaseActorModel
    {
        public float speed = 10f;
        private BulletData _bulletData;

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
                IImpact[] impacts = new IImpact[_bulletData.impactInfos.Length];
                for (int index = 0; index < _bulletData.impactInfos.Length; index++) {
                    var impactInfo = _bulletData.impactInfos[index];
                    impacts[index] = ImpactFactory.Instance.Create(impactInfo);
                }
                CollisionInfoData = new CollisionInfo(impacts);
            }
        }
    }
}