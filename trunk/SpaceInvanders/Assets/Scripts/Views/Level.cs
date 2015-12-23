using System;
using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
	public GameObject levelBounds;
	public GameObject heroSpawnPoint;
	public GameObject[] enemySpawnPoints;
	public int SpawnRowDistance = 0;

	private int _enemySpawnPointIndex;
	private int _enemySpawnRow;

	void Start()
	{
		EventManager.Get<LevelStartEvent> ().Publish ();
	}

	private LevelModel _model;
	public void Attach (LevelModel model)
	{
		_model = model;
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

	void SpawnHero (HeroModel model)
	{
		Hero hero =  CreateActor<Hero> (model);
		hero.transform.localPosition = heroSpawnPoint.transform.localPosition;
	}

	void SpawnEnemy(EnemyModel model)
	{
		Enemy enemy = CreateActor<Enemy> ( model);

		Vector3 position = enemySpawnPoints[_enemySpawnPointIndex].transform.localPosition;
		position.y += _enemySpawnRow * (SpawnRowDistance + enemy.GetYSize());
		enemy.transform.localPosition = position;

		_enemySpawnPointIndex ++;
		if (_enemySpawnPointIndex >= enemySpawnPoints.Length) {
			_enemySpawnPointIndex = 0;
			_enemySpawnRow ++;
		}
	}

	T CreateActor<T>(BaseActorModel model)  where T:BaseActor
	{
		GameObject prefab = (GameObject)Resources.Load ("Prefabs/GameEntities/" + model.dataType);
		GameObject go = GameObject.Instantiate (prefab) as GameObject;
		Transform t = go.transform;
		t.parent = this.transform;
		t.localRotation = Quaternion.identity;
		go.SetActive (true);

		T actor = go.GetComponent<T> ();
		actor.Init (model);
		return actor;
	}



}

