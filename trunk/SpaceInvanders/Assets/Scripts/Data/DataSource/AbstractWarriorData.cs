using Assets.Scripts.Data.Attributes;
using System;

namespace Assets.Scripts.Data.DataSource
{
    public abstract class AbstractWarriorData : BaseData
    {
        [DbField] public int maxHealth;// { get; set;}
        [DbField] public string weaponId;// { get; set;}
        [DbField] public float weight;// { get; set;}
        public SkillInfo[] skillInfos;

        [NonSerialized]
        private WeaponData _weapon;
        public WeaponData Weapon {
            get {
                if (_weapon == null){
                    _weapon = Main.Inst.Data.Get<WeaponData>(weaponId);
                }
                return _weapon;
            }
        }
    }

    public class SkillInfo
    {
        public string name;
        public double value;
        public double maxValue = double.MaxValue;
        public double minValue = double.MinValue;
    }
}


