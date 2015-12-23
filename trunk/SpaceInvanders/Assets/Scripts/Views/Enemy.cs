using UnityEngine;
using System.Collections;

public class Enemy : BaseActor
{
	private EnemyModel _model;

	public override void Init (BaseActorModel model)
	{
		_model = (EnemyModel)model;
		_model.MoveEvent += OnMove;
	}

	void OnMove (Vector3 moveVector)
	{
		transform.Translate (moveVector * 3f);
	}

	public float GetYSize ()
	{
		return transform.localScale.y;
	}

	public override void OnCollision(BaseActor other)
	{
		_model.AddComponent (new GuidedBehaviuorComponent ());
	}

}
