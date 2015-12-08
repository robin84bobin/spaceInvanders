using System;
using UnityEngine;
using Data;

public class HeroModel: BaseActorModel, IGuided
{
	private WeaponModel _weapon;
	private HeroData _data;

	public HeroModel (HeroData data):base(data)
	{
		_data = data;
		_data.maxHealth = 4;
		if (_data.weapon != null) {
			_weapon = new WeaponModel (_data.weapon);
		}

		AddBehaviour (new GuidedBehaviuor (this));
	}


	#region IGuided implementation
	public void Attack ()
	{
		Debug.Log ("ATTACK");
	}

	public Vector3 MoveVector {
		set {
			OnMove_Handle (value);
		}
	}
	#endregion

	protected override void Release()
	{
		base.Release ();
	}
}


