using System;
using Assets.Scripts.Data.Attributes;

namespace Assets.Scripts.Data.DataSource
{
    public class WeaponData : BaseData
    {
        [DbField] public int frequency;// { get; set;}

        [DbField] public int bulletSpeed;// { get; set;}

        [DbField] public string bulletId;// { get; set;}

        private BulletData _bullet;
        public BulletData Bullet {
            get {
                if(_bullet == null){
                    _bullet = Main.Inst.Data.Get<BulletData>(bulletId);
                }
                return _bullet;
            }
        }

        public string PrefabName
        {
            get { return "Weapon"; }
        }
    }
}



