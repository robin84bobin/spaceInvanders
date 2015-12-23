using System;
using UnityEngine;
using Data;

public class EnemyModel : BaseActorModel
{
	private EnemyData _enemyData;
	private double _moveEnemiesTime;
	private double _movePeriod;
	private Vector3 _speedVector;

	public EnemyModel (EnemyData enemyData): base(enemyData)
	{
		_enemyData = enemyData;
	}

	public void Init (double speed, double movePeriod)
	{
		_speedVector = new Vector3(0f,-(float)speed,0f);
		_movePeriod = movePeriod;
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


	public void Move(Vector3 vector)
	{
		OnMoveEvent (vector);
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


