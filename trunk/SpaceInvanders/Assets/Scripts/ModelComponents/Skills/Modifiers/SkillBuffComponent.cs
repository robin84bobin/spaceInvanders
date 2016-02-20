using System;
using System.Collections.Generic;
using Assets.Scripts.ModelComponents.Actors;
using Assets.Scripts.ModelComponents.Skills.Modifiers.ModifyStartegies;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Skills.Modifiers
{
    public class SkillBuffComponent : BaseComponent
    {
        public string[] skills;
        public double value;
        public double delay;
        public double activeTime;
        public double period;
        public bool revertable;
        public bool applyImmediately = true;

        public IModifyStrategy modifyStrategy;

        private Dictionary<string, Skill> _targetSkills;
        private double _applyTime;

        internal void Apply(BaseActorModel actor_)
        {
            if (applyImmediately){
                Modify(actor_.Skills);
            }
            else {
                SetTargetSkills(actor_.Skills);
                actor_.AddComponent(this);
            }
        }

        private double _stopTime;

        public SkillBuffComponent()
        {
        }

        public void SetTargetSkills(Dictionary<string, Skill> skills_)
        {
            _targetSkills = skills_;
        }

        protected override void OnInit()
        {
            base.OnInit();
            _applyTime = Time.time + delay;
            _stopTime  = Time.time + delay + activeTime;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (_stopTime < Time.time) {
                Lock();
                Remove();
                return;
            }

            bool needApply = _applyTime < Time.time;

            if (needApply) {
                _applyTime += period;
                Modify();
            }
        }

        public void Modify()
        {
            if (_targetSkills != null)
            modifyStrategy.Apply(value, skills, _targetSkills);
        }

        internal void Modify(Dictionary<string, Skill> targetSkills_)
        {
            if (targetSkills_ != null)
                modifyStrategy.Apply(value, skills, targetSkills_);
        }

        protected override void OnRelease()
        {
            base.OnRelease();
            _targetSkills = null;
        }
    }
}