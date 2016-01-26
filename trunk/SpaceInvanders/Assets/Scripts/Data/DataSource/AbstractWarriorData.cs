using Assets.Scripts.Data.Attributes;
using System;

namespace Assets.Scripts.Data.DataSource
{
    public abstract class AbstractWarriorData : BaseData
    {
        [DbField]
        public int MaxHealth { get; set;}
        [DbField]
        public string WeaponId { get; set;}
        [DbField]
        public float Weight { get; set;}

        [NonSerialized]
        private WeaponData _weapon;
        public WeaponData Weapon {
            get {
                if (_weapon == null){
                    _weapon = Main.Inst.Data.Get<WeaponData>(WeaponId);
                }
                return _weapon;
            }
        }
    }
}


