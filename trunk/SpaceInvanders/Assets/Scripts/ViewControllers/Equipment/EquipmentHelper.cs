using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.ViewControllers.Equipment
{
    public enum EquipmentType
    {
        CLOTH,
        WEAPON
    }


    class EquipmentHelper
    {
        public static readonly string Cloth = "Cloth";
        public static readonly string Weapon = "Weapon";

        public readonly static Dictionary<string, EquipmentType> EquipmentTypeMap = 
            new Dictionary<string, EquipmentType>() {
                { Cloth, EquipmentType.CLOTH},
                { Weapon, EquipmentType.WEAPON}
            };

        public static EquipmentType GetEquipType(string type_)
        {
            if (!EquipmentTypeMap.ContainsKey(type_)) {
                Debug.LogError(string.Format("Unexpected equipment type {0}", type_.ToString()));
            }

            return EquipmentTypeMap[type_];
        }
    }
}
