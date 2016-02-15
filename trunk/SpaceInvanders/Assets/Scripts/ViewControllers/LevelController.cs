using System;
using Assets.Scripts.Events;
using Assets.Scripts.Events.CustomEvents;
using Assets.Scripts.Factories.GameEntitiesFactories;
using Assets.Scripts.ModelComponents.Actors;
using Assets.Scripts.ModelComponents.Level;
using UnityEngine;

namespace Assets.Scripts.ViewControllers
{
    public class LevelController : MonoBehaviour
    {
        public GameObject levelBounds;
        public GameObject heroSpawnPoint;
        public GameObject[] enemySpawnPoints;
        public int spawnRowDistance = 0;

        private int _enemySpawnPointIndex;
        private int _enemySpawnRow;

        void Start()
        {
            EventManager.Get<LevelStartEvent> ().Publish ();
            GameActorBuilder.Enable(this);    
                 
        }

        private LevelModel _model;
        public void Attach (LevelModel model_)
        {
            _model = model_;
            _model.OnEnemyCreate += SpawnEnemy;
            _model.OnHeroCreate += SpawnHero;
            _model.OnStartEnemyWave += OnEnemyWaveStart;
        }

        public void Release()
        {
            _model.OnEnemyCreate -= SpawnEnemy;
            _model.OnHeroCreate -= SpawnHero;
            _model.OnStartEnemyWave -= OnEnemyWaveStart;
            _model = null;

            GameActorBuilder.Disable();
        }

        void OnEnemyWaveStart()
        {
            _enemySpawnPointIndex = 0;
            _enemySpawnRow = 0;
        }

        void SpawnHero (HeroModel model_)
        {
            CreateObjectParams param = new CreateObjectParams {
                model = model_,
                position = heroSpawnPoint.transform.position
            };

            GameActorBuilder.CreateActor(param);
        }


        void SpawnEnemy(EnemyModel model_)
        {
            Vector3 spawnPosition = enemySpawnPoints[_enemySpawnPointIndex].transform.position;
            CreateObjectParams param = new CreateObjectParams
            {
                model = model_,
                position = spawnPosition
            };
            //EventManager.Get<CreateObjectEvent>().Publish(param);
            GameActorBuilder.CreateActor(param);

            _enemySpawnPointIndex ++;
            if (_enemySpawnPointIndex >= enemySpawnPoints.Length) {
                _enemySpawnPointIndex = 0;
                _enemySpawnRow ++;
            }
        }


    }
}

