using UnityEngine;
using System.Collections;

public class EnemyController : BaseActorController
{
	private EnemyModel _model;

	protected override void OnInit (BaseActorModel model)
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

	public override void OnCollision(BaseActorController other)
	{
		_model.AddComponent (new GuidedBehaviuorComponent ());
	}

}
