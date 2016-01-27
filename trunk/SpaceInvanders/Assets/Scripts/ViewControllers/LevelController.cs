using Assets.Scripts.Events;
using Assets.Scripts.Events.CustomEvents;
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
        }

        void OnEnemyWaveStart()
        {
            _enemySpawnPointIndex = 0;
            _enemySpawnRow = 0;
        }

        void SpawnHero (HeroModel model_)
        {
            HeroController hero =  CreateActor<HeroController, HeroModel> (model_);
            hero.transform.localPosition = heroSpawnPoint.transform.localPosition;
        }


        void SpawnEnemy(EnemyModel model_)
        {
            EnemyController enemy = CreateActor<EnemyController, EnemyModel> ( model_);

            Vector3 position = enemySpawnPoints[_enemySpawnPointIndex].transform.localPosition;
            position.y += _enemySpawnRow * (spawnRowDistance + enemy.GetYSize());
            enemy.transform.localPosition = position;

            _enemySpawnPointIndex ++;
            if (_enemySpawnPointIndex >= enemySpawnPoints.Length) {
                _enemySpawnPointIndex = 0;
                _enemySpawnRow ++;
            }
        }

        /// <summary>
        /// Creates the actor.
        /// Where T:BaseActorController<M>
        /// Where M:BaseActorModel 
        /// </summary>
        T CreateActor<T,TM>(TM model_)  where TM:BaseActorModel where T:BaseActorController<TM>
        {
            GameObject prefab = (GameObject)Resources.Load ("Prefabs/GameEntities/" + model_.DataType);
            GameObject go = GameObject.Instantiate (prefab) as GameObject;
            Transform t = go.transform;
            t.parent = this.transform;
            t.localRotation = Quaternion.identity;
            go.SetActive (true);

            T actor = go.GetComponent<T> ();
            actor.Init (model_);
            return actor;
        }



    }
}

