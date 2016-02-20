using System.Collections.Generic;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Collisions;
using Assets.Scripts.ModelComponents.Skills;
using Assets.Scripts.ModelComponents.Skills.Modifiers;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Actors
{
    public class EnemyModel : BaseActorModel
    {
        private EnemyData _enemyData;
        private double _moveEnemiesTime;
        private double _movePeriod;
        private Vector3 _speedVector;

        public EnemyModel (EnemyData enemyData_): base(enemyData_)
        {
            _enemyData = enemyData_;
            base.Init();
        }


        public void InitMoveParams (double speed_, double movePeriod_)
        {
            _speedVector = new Vector3(0f,-(float)speed_,0f);
            _movePeriod = movePeriod_;
            _moveEnemiesTime = Time.time + _movePeriod;
        }

        protected override void OnUpdate ()
        {
            CheckMove ();
        }
	
        void CheckMove ()
        {
            if (Time.time > _moveEnemiesTime) {
                _moveEnemiesTime += _movePeriod;
                OnMoveEvent(_speedVector);
            }
        }


        public void Move(Vector3 vector_)
        {
            OnMoveEvent (vector_);
        }

        #region implemented abstract members of BaseComponent

        protected override void OnRelease ()
        {
            //
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
            //CollisionInfoData = new CollisionInfo( new HitSkillBuffComponent(100f));
        }
    }
}


