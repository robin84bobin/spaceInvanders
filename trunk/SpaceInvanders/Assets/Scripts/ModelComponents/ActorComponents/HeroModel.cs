using System;
using UnityEngine;
using Data;

public class HeroModel: BaseActorModel
{
	private WeaponModel _weapon;
	private HeroData _data;

	public HeroModel (HeroData data):base(data)
	{
		_data = data;
		//
		_data.maxHealth = 4;

		if (_data.weapon != null) {
			_weapon = new WeaponModel (_data.weapon);
		}
	}


	public void Move(Vector3 vector)
	{
		OnMoveEvent (vector);
	}

	
	#region implemented abstract members of BaseComponent

	protected override void OnRelease ()
	{
		//;
	}


	protected override void OnInit ()
	{
		AddComponent (new GuidedBehaviuorComponent ());
	}

	#endregion

}


