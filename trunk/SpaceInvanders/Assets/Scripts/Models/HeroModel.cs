using System;
using UnityEngine;

public class HeroModel: BaseActorModel, IGuided
{
	public event Action<Vector3> OnMove = delegate{};

	private HeroData _data;

	public HeroModel (HeroData data):base(data)
	{
		_data = data;
		AddBehaviour (new GuidedBehaviuor (this));
	}


	#region IGuided implementation
	public void Attack ()
	{
		Debug.Log ("ATTACK");
	}

	public Vector3 MoveVector {
		set {
			OnMove (value);
		}
	}
	#endregion

	protected override void Release()
	{
		base.Release ();
		OnMove = null;
	}
}


