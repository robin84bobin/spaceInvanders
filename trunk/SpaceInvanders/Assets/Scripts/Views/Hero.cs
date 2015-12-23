using UnityEngine;
using System.Collections;

public class Hero : BaseActor {

	private HeroModel _model;

	public override void Init(BaseActorModel model)
	{
		_model = (HeroModel)model;
		_model.MoveEvent += OnMove;
	}

	Vector3 _moveVector = Vector3.zero;
	void OnMove (Vector3 moveVector)
	{
		_moveVector.x = moveVector.x;
		transform.Translate (_moveVector * 5f);
	}

}
