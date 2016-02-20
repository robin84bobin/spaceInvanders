using System;
using System.Collections.Generic;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Behaviours;
using Assets.Scripts.ModelComponents.Equipments;
using Assets.Scripts.ModelComponents.Skills;
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
            data.maxHealth = 4;

            if (data.Weapon != null) {
                _weaponModel = new WeaponModel (data.Weapon);
            }

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
            //;
        }

        protected override void OnInit ()
        {
            AddComponent (new GuidedBehaviuorComponent ());
        }

        #endregion

        protected override void InitSkills()
        {
            //TODO read skills from data
            Skills = new Dictionary<string, Skill> {
                {SKILLS.HEALTH, new Skill(SKILLS.HEALTH, 100f, 100f, 0f).MinValueCallback(Death)},
                {SKILLS.SPEED,  new Skill(SKILLS.SPEED, 10f, 100f, -10f)},
            };
        }

        protected override void InitCollisionInfo()
        {
            //TODO
        }
    }
}


