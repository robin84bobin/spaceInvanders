using System;
using System.Collections.Generic;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Collisions;
using Assets.Scripts.ModelComponents.Impacts;
using Assets.Scripts.ModelComponents.Skills;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Entities
{
    public abstract class BaseEntityModel : BaseComponent
    {
        public string DataType { get; private set; }

        protected BaseEntityModel(IBaseData data_)
        {
            DataType = data_.Type;
        }

        protected override void OnInit()
        {
            base.OnInit();
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

        protected void InitSkills(SkillInfo[] skillInfos_)
        {
            Skills = new Dictionary<string, Skill>();
            for (int i = 0; i < skillInfos_.Length; i++)
            {
                SkillInfo skillInfo = skillInfos_[i];
                Skill skill = new Skill(skillInfo.name, skillInfo.value, skillInfo.maxValue, skillInfo.minValue);
                Skills.Add(skill.Name, skill);
            }

            OnInitSkills();
        }

        protected abstract void OnInitSkills();

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
        protected virtual void InitCollisionInfo() {}

        #endregion

        #region IMPACTS

        public void ApplyImpact(Impact impact_)
        {
            impact_.Apply(this);
        }

        #endregion

        public void Destroy()
        {
            Release ();
        }

        protected override void OnRelease()
        {
            foreach (var skill in Skills)
            {
                skill.Value.Release();
            }
        }
       
}
}


