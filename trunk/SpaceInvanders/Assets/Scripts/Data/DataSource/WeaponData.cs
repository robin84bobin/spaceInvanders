using System;

public class WeaponData : BaseData
{
	public int frequency;
	public int bulletSpeed;

	public string bulletId;

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


