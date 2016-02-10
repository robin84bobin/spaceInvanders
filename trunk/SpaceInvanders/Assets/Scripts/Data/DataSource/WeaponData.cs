using System;
using Assets.Scripts.Data.Attributes;

namespace Assets.Scripts.Data.DataSource
{
    public class WeaponData : BaseData
    {
        [DbField] public int Frequency;// { get; set;}

        [DbField] public int BulletSpeed;// { get; set;}

        [DbField] public string BulletId;// { get; set;}

        private BulletData _bullet;
        public BulletData Bullet {
            get {
                if(_bullet == null){
                    _bullet = Main.Inst.Data.Get<BulletData>(BulletId);
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



