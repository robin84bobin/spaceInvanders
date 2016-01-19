using System;

public abstract class AbstractWarriorData : BaseData
{

	public int maxHealth { get; set;}
	public string weaponId { get; set;}
	public float weight { get; set;}

	[NonSerialized]
	private WeaponData _weapon;
	public WeaponData weapon {
		get {
			if (_weapon == null){
				_weapon = Main.inst.Data.Get<WeaponData>(weaponId);
			}
			return _weapon;
		}
	}
}


