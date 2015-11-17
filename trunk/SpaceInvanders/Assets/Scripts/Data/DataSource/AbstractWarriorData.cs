using System;

public abstract class AbstractWarriorData : BaseData
{
	public int maxHealth;
	public string weaponId;
	public float weight;

	[NonSerialized]
	private WeaponData _weapon;
	public WeaponData weapon {
		get {
			if (_weapon == null){
				_weapon = Main.inst.Data.WeaponStorage.Get(weaponId);
			}
			return _weapon;
		}
	}
}

