using Assets.Scripts.Data.DataSource;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.ActorComponents
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
        }

        public void Init (double speed_, double movePeriod_)
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

        protected override void OnInit ()
        {
            //
        }

        #endregion
    }
}


