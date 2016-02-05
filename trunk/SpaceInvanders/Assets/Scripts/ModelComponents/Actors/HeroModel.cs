using System;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Behaviours;
using Assets.Scripts.ModelComponents.Equipments;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Actors
{
    public class HeroModel: BaseActorModel
    {


        private WeaponModel _weaponModel;
        public WeaponModel Weapon
        {
            get { return _weaponModel; }
        }

        public HeroModel (HeroData data_):base(data_)
        {
            var data = data_;
            //
            data.MaxHealth = 4;

            if (data.Weapon != null) {
                _weaponModel = new WeaponModel (data.Weapon);
            }
        }

        public void Attack()
        {
            Weapon.Attack();
        }

        public void Move(Vector3 vector_)
        {
            OnMoveEvent (vector_);
        }

	
        #region implemented abstract members of BaseComponent

        protected override void OnRelease ()
        {
            //;
        }

        protected override void OnInit ()
        {
            AddComponent (new GuidedBehaviuorComponent ());
        }

        #endregion

        protected override void InitCollisionInfo()
        {
            //TODO
        }
    }
}


