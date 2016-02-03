using System;
using Assets.Scripts.CommonComponents.StateSwitcher;
using Assets.Scripts.Data;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Actors;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.Level
{
    public sealed class LevelModel : BaseComponent
    {
        public event Action<HeroModel> OnHeroCreate = delegate{};
        public event Action<EnemyModel> OnEnemyCreate = delegate{};
        public event Action OnStartEnemyWave = delegate{};
        public event Action OnGameOver = delegate{};

        private readonly LevelData _levelData;
        private HeroModel _hero;

        private double _waveSpawnTime;
        private double _enemySpeed;
	
        private BaseStateMachine<LevelStates,ILevelState>  _states;

        public LevelModel (LevelData levelData_)
        {
            _levelData = levelData_;
            Init ();
        }

        public override void Init()
        {	
            _states = new BaseStateMachine<LevelStates,ILevelState>();
            _states.Add (LevelStates.PLAY, new LevelStatePlay (this));
            _states.Add (LevelStates.PAUSE, new LevelStatePause (this));
        }
	
        public void Start()
        {
            _states.SetState (LevelStates.PLAY);
            CreateHero ();
            Unlock ();
        }


        public void UpdateModel ()
        {
            _states.CurrentState.Update ();
        }

        public void UpdateGamePlay ()
        {
            CheckSpawnEnemies ();
            Update ();
        }

        int _waveNum = 0;
        void CheckSpawnEnemies()
        {
            if (Time.time >= _waveSpawnTime) {
                _waveNum ++;
                _enemySpeed = _levelData.EnemyStartSpeed + _waveNum * _levelData.EnemySpeedFactor * _levelData.EnemyStartSpeed;
                _waveSpawnTime = Time.time + _levelData.EnemyWaveRate;
                CreateEnemies ();
            }
        }

        void CreateHero ()
        {
            _hero = new HeroModel(_levelData.Hero);
            OnHeroCreate (_hero);
            //AddComponent (_hero);
        }

        void CreateEnemies ()
        {
            OnStartEnemyWave ();
            for (int i = 0; i < _levelData.EnemyWaveSize; i++) {
                EnemyModel enemy = new EnemyModel(_levelData.Enemy);
                enemy.Init(_enemySpeed, _levelData.EnemyMovePeriod);
                //AddComponent(enemy);
                OnEnemyCreate (enemy);
            }
            Debug.Log ("");
        }



        void OnActorDeath (BaseActorModel actor_)
        {
            if (actor_.DataType == DataTypes.HERO){
                GameOver();
            }
            RemoveActor (actor_);
        }

        void RemoveActor(BaseActorModel actor_)
        {
            actor_.Destroy();
        }

        void GameOver ()
        {
            OnGameOver ();
        }


        void SetState (LevelStates levelState_)
        {
            _states.SetState (levelState_);
        }

        private float _pauseTime;
        private bool _pause;
        public void Pause (bool pause_)
        {
            _pause = pause_;
        }



        #region implemented abstract members of BaseComponent
        protected override void OnRelease ()
        {
            //
        }
        #endregion
    }
}


