using System;
namespace Data{
public abstract class AbstractWarriorData : BaseData
{
	private int _maxHealth;
	public int maxHealth { 
		get{ return _maxHealth; }
		internal set { _maxHealth = value;}
	}
	public string weaponId { get; internal set;}
	public float weight { get; internal set;}

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
}

