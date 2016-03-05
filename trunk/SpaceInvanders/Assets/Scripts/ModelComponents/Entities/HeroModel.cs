using System.Collections.Generic;
using System.Linq.Expressions;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Behaviours;
using Assets.Scripts.ModelComponents.Equipments;
using Assets.Scripts.ModelComponents.Skills;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Actors
{
    public class HeroModel: BaseEntityModel
    {
        private WeaponModel _weaponModel;
        public WeaponModel Weapon
        {
            get { return _weaponModel; }
        }

        public HeroModel (HeroData data_):base(data_)
        {
            var data = data_;
            if (data.Weapon != null) {
                _weaponModel = new WeaponModel (data.Weapon);
            }
            InitSkills(data.skillInfos);
            base.Init();
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
            base.OnRelease();
        }

        protected override void OnInit ()
        {
            AddComponent (new GuidedBehaviuorComponent ());
        }

        #endregion

        protected override void OnInitSkills()
        {
            Skills[SKILLS.HEALTH].MinValueCallback(Death);
        }

        protected override void InitCollisionInfo()
        {
            //TODO
        }
    }
}


