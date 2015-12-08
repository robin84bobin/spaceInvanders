using System;

namespace Data{

public class WeaponData : BaseData
{
	public int frequency { get; internal set;}
	public int bulletSpeed { get; internal set;}
	public string bulletId { get; internal set;}

	private BulletData _bullet;
	public BulletData bullet {
		get {
			if(_bullet == null){
				_bullet = Main.inst.Data.BulletStorage.Get(bulletId);
			}
			return _bullet;
		}
	}
}
}


