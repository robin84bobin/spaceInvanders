using System;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Skills;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Entities
{
    public class EnemyModel : BaseEntityModel
    {
        public event Action<BonusData[]> DropBonusEvent = delegate { };

        private double _moveEnemiesTime;
        private double _movePeriod;
        private Vector3 _speedVector;
        private readonly EnemyData _enemyData;

        public EnemyModel (EnemyData enemyData_): base(enemyData_)
        {
            _enemyData = enemyData_;
            InitSkills(_enemyData.skillInfos);
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
            DropBonusEvent.Invoke(_enemyData.Bonuses);
            base.OnRelease();
        }

        #endregion





        protected override void OnInitSkills()
        {
            Skills[SKILLS.HEALTH].MinValueCallback(Death);
        }

        protected override void InitCollisionInfo()
        {
           //
        }
    }
}


