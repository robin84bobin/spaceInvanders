using System.Runtime.CompilerServices;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Events;
using Assets.Scripts.Events.CustomEvents;
using Assets.Scripts.ModelComponents.Equipments;
using UnityEngine;

namespace Assets.Scripts.ViewControllers.Equipment.Weapon
{
    public class WeaponController : AbstractEquipmentController
    {
        public Animation attackAnimation;
        public Transform bulletSpawnPos;

        private WeaponModel _model;

        public override void Init(IEquipmentModel model_)
        {
            _model = (WeaponModel)model_;
            _model.AttackEvent += OnAttack;

            if (bulletSpawnPos == null) {
                bulletSpawnPos = transform;
            }
        }

        private BulletParams _bulletParams = new BulletParams();
        private void OnAttack(Vector3 direction_, BulletData bulletData_)
        {
            Animate(attackAnimation);

            _bulletParams.position = bulletSpawnPos.position;
            _bulletParams.direction = direction_;
            _bulletParams.data = bulletData_;
            EventManager.Get<BulletEvent>().Publish(_bulletParams);
        }

        private void Animate(Animation animation_)
        {
            if (animation_ != null) {
                animation_.Play();
            }
        }

        protected override void OnEnable()
        {
            //
        }

        protected override void OnDisable()
        {
           //
        }

        public override void Release()
        {
            _model.Release();
        }
    }
}