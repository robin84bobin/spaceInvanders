using System;
using System.Collections.Generic;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Collisions;
using Assets.Scripts.ModelComponents.Skills;
using Assets.Scripts.ModelComponents.Skills.Modifiers;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Actors
{
    public abstract class BaseActorModel : BaseComponent
    {
        public string DataType { get; private set; }

        protected BaseActorModel(IBaseData data_)
        {
            DataType = data_.Type;
        }

        protected override void OnInit()
        {
            base.OnInit();
            InitSkills();
            InitCollisionInfo();
        }

        protected void Death()
        {
            OnDeathEvent();
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

        public event Action DeathEvent = delegate{};
        protected virtual void OnDeathEvent ()
        {
            var handler = DeathEvent;
            if (handler != null)
                handler ();
        }

        #endregion

        #region SKILLS

        public Dictionary<string, Skill> Skills { get; protected set; }
        
        protected abstract void InitSkills();
        

        public void ChangeSkill(string skill_, double amount_)
        {
            if (!Skills.ContainsKey(skill_)){
                Debug.LogWarning(string.Format("Try to change unexisted skill {0}::{1}", this.GetType().Name, skill_));
            }
            Skills[skill_].ChangeValue(amount_);
        }

        public void SetSkill(string skill_, double value_)
        {
            if (!Skills.ContainsKey (skill_)) {
                Debug.LogWarning(string.Format ("Try to set unexisted skill {0}::{1}", this.GetType().Name, skill_));
            }
            Skills [skill_].SetValue(value_);
            //TODO dispatch skill update event
        }

        #endregion

        #region COLLISIONS


        public CollisionInfo CollisionInfoData { get; internal set; }


        protected abstract void InitCollisionInfo();


        #endregion

        public void Destroy()
        {
            Release ();
        } 


    }
}


