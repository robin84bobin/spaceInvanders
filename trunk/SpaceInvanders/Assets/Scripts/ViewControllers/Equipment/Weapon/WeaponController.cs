using System.Runtime.CompilerServices;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Events;
using Assets.Scripts.Events.CustomEvents;
using Assets.Scripts.Factories.GameEntitiesFactories;
using Assets.Scripts.ModelComponents.Actors;
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

        private readonly CreateObjectParams _createObjectParams = new CreateObjectParams();
        private void OnAttack(Vector3 direction_, BulletData bulletData_)
        {
            Animate(attackAnimation);

            _createObjectParams.rotation = bulletSpawnPos.transform.rotation.eulerAngles 
                + new Vector3( 0f,Random.Range(-10f,10f), Random.Range(-5f, 5f));
            _createObjectParams.position = bulletSpawnPos.position;
            _createObjectParams.scale = Vector3.one;
            var bullet = new BulletModel(bulletData_);
            _createObjectParams.model = bullet;

            GameActorBuilder.CreateActor(_createObjectParams);
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