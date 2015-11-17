using System;
using UnityEngine;

public class EnemyModel : BaseActorModel, IGuided
{
	public event Action<float> OnMove = delegate{};
	public event Action<Vector3> onMoveGuided = delegate{};

	private EnemyData _enemyData;
	private float _moveEnemiesTime;
	private float _movePeriod;
	private float _speed;

	public EnemyModel (EnemyData enemyData): base(enemyData)
	{
		_enemyData = enemyData;
	}

	public void Init (float speed, float movePeriod)
	{
		_speed = speed;
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
			OnMove(_speed);
		}
	}

	#region IGuided implementation

	public void Attack ()
	{
		//throw new NotImplementedException ();
	}

	public Vector3 MoveVector {
		set {
			onMoveGuided(value);
		}
	}

	#endregion

	protected override void Release()
	{
		base.Release ();
		OnMove = null;
	}
}


