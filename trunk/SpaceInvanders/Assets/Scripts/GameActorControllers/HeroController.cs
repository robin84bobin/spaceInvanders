using UnityEngine;
using System.Collections;

public class HeroController: BaseActorController<HeroModel>
{
	Vector3 _moveVector = Vector3.zero;
	void OnMove (Vector3 moveVector)
	{
		_moveVector.x = moveVector.x;
		transform.Translate (_moveVector * 5f);
	}

	#region implemented abstract members of BaseActorController

	protected override void OnInit()
	{
		_model.MoveEvent += OnMove;
	}

	protected override void Release ()
	{

	}

	#endregion
}
