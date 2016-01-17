using UnityEngine;
using System.Collections;

public class EnemyController : BaseActorController<EnemyModel>
{
	#region implemented abstract members of BaseActorController

	protected override void OnInit ()
	{
		_model.MoveEvent += OnMove;
	}

	protected override void Release ()
	{
		_model = null;
	}

	#endregion

	void OnMove (Vector3 moveVector)
	{
		transform.Translate (moveVector * 3f);
	}

	public float GetYSize ()
	{
		return transform.localScale.y;
	}

}
