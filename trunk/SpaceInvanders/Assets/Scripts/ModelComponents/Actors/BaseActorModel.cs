using System;
using System.Collections.Generic;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Collisions;
using Assets.Scripts.ModelComponents.Skills;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Actors
{
    public abstract class BaseActorModel : BaseComponent
    {
        public string DataType { get; private set; }

        protected BaseActorModel(IBaseData data_)
        {
            base.Init();
            DataType = data_.Type;
            InitSkills();
            InitCollisionInfo();
        }

        private void Death()
        {
            OnDeathEvent(this);
        }

        #region EVENTS

        public event Action<Vector3> MoveEvent = delegate{};
        protected virtual void OnMoveEvent (Vector3 obj_)
        {
            var handler = MoveEvent;
            if (handler != null)
                handler (obj_);
        }

        public event Action<int> DamageEvent = delegate{};
        protected virtual void OnDamageEvent (int obj_)
        {
            var handler = DamageEvent;
            if (handler != null)
                handler (obj_);
        }

        public event Action<BaseActorModel> DeathEvent = delegate{};
        protected virtual void OnDeathEvent (BaseActorModel obj_)
        {
            var handler = DeathEvent;
            if (handler != null)
                handler (obj_);
        }

        #endregion

        #region SKILLS

        protected Dictionary<string, Skill> skills;

        private void InitSkills()
        {
            //TODO read skills from data
            skills = new Dictionary<string, Skill> {
                {SKILLS.HEALTH, new Skill(SKILLS.HEALTH, 100f, 100f, 0f).MinValueCallback(Death)},
                {SKILLS.SPEED,  new Skill(SKILLS.SPEED, 10f, 100f, -10f)},
            };
        }

        public void ChangeSkill(string skill_, double amount_)
        {
            if (!skills.ContainsKey(skill_))
            {
                Debug.LogWarning(string.Format("Try to modify unexisted skill {0}::{1}", this.GetType().Name, skill_));
            }
            skills[skill_].ChangeValue(amount_);
        }

        public void SetSkill(string skill_, double value_)
        {
            if (!skills.ContainsKey (skill_)) {
                Debug.LogWarning(string.Format ("Try to modify unexisted skill {0}::{1}", this.GetType().Name, skill_));
            }
            skills [skill_].SetValue(value_);
            //TODO dispatch skill update event
        }

        #endregion

        #region COLLISIONS

        protected CollisionInfo collisionInfo;

        protected abstract void InitCollisionInfo();

        public void ProcessCollisionInfo(CollisionInfo info_)
        {
            foreach (ImpactInfo damageInfo in info_.Impact) {
                damageInfo.Apply(skills);
            }
        }

        #endregion

        public void Destroy()
        {
            Release ();
        } 


    }
}


