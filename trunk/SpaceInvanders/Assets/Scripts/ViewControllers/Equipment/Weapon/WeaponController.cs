using Assets.Scripts.Data.DataSource;
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

        private readonly CreateParams _createParams = new CreateParams();
        private void OnAttack(Vector3 direction_, BulletData bulletData_)
        {
            Animate(attackAnimation);

            _createParams.rotation = bulletSpawnPos.transform.rotation.eulerAngles 
                + new Vector3( 0f,Random.Range(-10f,10f), Random.Range(-5f, 5f));
            _createParams.position = bulletSpawnPos.position;
            _createParams.data = bulletData_;
            GameObjectsBuilder.GameObjectsBuilder.Create(_createParams);
        }

        private void Animate(Animation animation_)
        {
            if (animation_ != null) {
                animation_.Play();
            }
        }

        protected override void OnEquip()
        {
            //
        }

        protected override void OnUnequip()
        {
           //
        }

        public override void Release()
        {
            _model.Release();
        }
    }
}