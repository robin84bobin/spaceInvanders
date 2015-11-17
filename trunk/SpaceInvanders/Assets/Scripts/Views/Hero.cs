using UnityEngine;
using System.Collections;

public class Hero : BaseActor {

	private HeroModel _model;

	public override void Init(BaseActorModel model)
	{
		_model = (HeroModel)model;
		_model.OnMove += OnMove;
	}

	void OnMove (Vector3 moveVector)
	{
		transform.Translate (moveVector * 5f);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Actor") {
			BaseActor actor = other.gameObject.GetComponent<BaseActor>();
			actor.OnCollision(this);
		}
	}
}
