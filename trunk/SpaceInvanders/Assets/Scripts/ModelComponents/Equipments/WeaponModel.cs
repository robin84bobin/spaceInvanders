using System;
using Assets.Scripts.Data.DataSource;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Equipments
{
    public class WeaponModel : BaseComponent, IEquipmentModel
    {
        public event Action<Vector3, BulletData> AttackEvent = delegate {};

        private readonly WeaponData _data;
        private Vector3 _direction;

        public string PrefabName
        {
            get { return _data.PrefabName; }  
        }

        public string Type
        {
            get { return _data.type; }
        }

        public WeaponModel (WeaponData data_)
        {
            _data = data_;
        }

        public void SetDirection(Vector3 direction_)
        {
            _direction = direction_;
        }



        public void OnEquip()
        {
           Debug.Log("Equipped:"+_data.objectId);
        }

        public void OnUnequip()
        {
            Debug.Log("Unequipped:" + _data.objectId);
        }

        public void Attack()
        {
            AttackEvent(_data.bulletSpeed * _direction, _data.Bullet);
        }

    }
}


