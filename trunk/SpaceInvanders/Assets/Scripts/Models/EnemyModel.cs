using System;
using UnityEngine;
using Data;

public class EnemyModel : BaseActorModel, IGuided
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

	public override void Update ()
	{
		CheckMove ();
	}
	
	void CheckMove ()
	{
		if (Time.time > _moveEnemiesTime) {
			_moveEnemiesTime += _movePeriod;
			OnMove_Handle(_speedVector);
		}
	}

	#region IGuided implementation

	public void Attack ()
	{
		//throw new NotImplementedException ();
	}

	public Vector3 MoveVector {
		set {
			OnMove_Handle(value);
		}
	}

	#endregion

	protected override void Release()
	{
		base.Release ();
	}
}


