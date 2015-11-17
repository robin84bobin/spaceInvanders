using UnityEngine;
using System.Collections;

public class Enemy : BaseActor
{
	private EnemyModel _model;

	public override void Init (BaseActorModel model)
	{
		_model = (EnemyModel)model;
		_model.OnMove += OnMove;
		_model.onMoveGuided += OnMoveGuided;
	}

	void OnMove (float speed)
	{
		transform.position += new Vector3 (0f, - speed, 0f);
	}

	void OnMoveGuided (Vector3 moveVector)
	{
		transform.Translate (moveVector * 3f);
	}

	public float GetYSize ()
	{
		return transform.localScale.y;
	}

	public override void OnCollision(BaseActor other)
	{
		_model.AddBehaviour (new GuidedBehaviuor (_model));
	}

}
