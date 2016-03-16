using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Events;
using Assets.Scripts.Events.CustomEvents;
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
            GameObjectsBuilder.GameObjectsBuilder.Enable(this);    
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

            GameObjectsBuilder.GameObjectsBuilder.Disable();
        }

        void OnEnemyWaveStart()
        {
            _enemySpawnPointIndex = 0;
            _enemySpawnRow = 0;
        }

        void SpawnHero (HeroData data_)
        {
            CreateParams param = new CreateParams {
                data = data_,
                position = heroSpawnPoint.transform.position
            };

            GameObjectsBuilder.GameObjectsBuilder.Create(param);
        }


        void SpawnEnemy(EnemyData data_)
        {
            Vector3 spawnPosition = enemySpawnPoints[_enemySpawnPointIndex].transform.position;
            CreateParams param = new CreateParams
            {
                data = data_,
                position = spawnPosition
            };
            GameObjectsBuilder.GameObjectsBuilder.Create(param);

            _enemySpawnPointIndex ++;
            if (_enemySpawnPointIndex >= enemySpawnPoints.Length) {
                _enemySpawnPointIndex = 0;
                _enemySpawnRow ++;
            }
        }


    }
}

