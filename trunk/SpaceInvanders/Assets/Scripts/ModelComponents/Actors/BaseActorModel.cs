using System;
using System.Collections.Generic;
using Assets.Scripts.Data.DataSource;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Actors
{
    public abstract class BaseActorModel : BaseComponent
    {
        protected Dictionary<string,double> skills = new Dictionary<string, double> ();

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

        public string DataType { get; private set; }

        protected BaseActorModel (IBaseData data_)
        {
            DataType = data_.Type;
        }

        public void SetSkill(string skill_, double value_)
        {
            if (!skills.ContainsKey (skill_)) {
                Debug.LogWarning(string.Format ("Try to modify unexisted skill {0}::{1}", this.GetType().Name, skill_));
            }
            skills [skill_] = value_;
            //TODO dispatch skill update event
        }

        public void Destroy()
        {
            Release ();
        } 


    }
}


