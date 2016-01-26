using System;
using Assets.Scripts.Data.DataSource;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.ActorComponents
{
    public class WeaponModel
    {
        public event Action<Vector3, BulletData> OnShot = delegate {};

        private readonly WeaponData _data;
        private Vector3 _direction;

        public WeaponModel (WeaponData data_)
        {
            _data = data_;
        }

        public void SetDirection(Vector3 direction_)
        {
            _direction = direction_;
        }

        public void Shot()
        {
            OnShot (_data.BulletSpeed * _direction, _data.Bullet);
        }
    }
}


