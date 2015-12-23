
using System;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class LevelModel : BaseComponent
{
	public event Action<HeroModel> OnHeroCreate = delegate{};
	public event Action<EnemyModel> OnEnemyCreate = delegate{};
	public event Action OnStartEnemyWave = delegate{};
	public event Action OnGameOver = delegate{};

	private LevelData _levelData;
	private HeroModel _hero;

	private double _waveSpawnTime;
	private double _enemySpeed;
	
	private BaseStateMachine<LevelStates,ILevelState>  _states;

	public LevelModel (LevelData levelData)
	{
		_levelData = levelData;
		Init ();
	}

	public void Init()
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
			_enemySpeed = _levelData.enemyStartSpeed + _waveNum * _levelData.enemySpeedFactor * _levelData.enemyStartSpeed;
			_waveSpawnTime = Time.time + _levelData.enemyWaveRate;
			CreateEnemies ();
		}
	}

	void CreateHero ()
	{
		_hero = new HeroModel(_levelData.hero);
		OnHeroCreate (_hero);
		AddComponent (_hero);
	}

	void CreateEnemies ()
	{
		OnStartEnemyWave ();
		for (int i = 0; i < _levelData.enemyWaveSize; i++) {
			EnemyModel enemy = new EnemyModel(_levelData.enemy);
			enemy.Init(_enemySpeed, _levelData.enemyMovePeriod);
			OnEnemyCreate (enemy);
		}
		Debug.Log ("");
	}



	void OnActorDeath (BaseActorModel actor)
	{
		if (actor.dataType == DataTypes.HERO){
			GameOver();
		}
		RemoveActor (actor);
	}

	void RemoveActor(BaseActorModel actor)
	{
		actor.Destroy();
	}

	void GameOver ()
	{
		OnGameOver ();
	}


	void SetState (LevelStates levelState)
	{
		_states.SetState (levelState);
	}

	private float _pauseTime;
	private bool _pause;
	public void Pause ()
	{
		_pause = true;
	}

	public void Continue()
	{
		_pause = false;
	}


	#region implemented abstract members of BaseComponent
	protected override void OnRelease ()
	{
		//
	}
	#endregion
}


