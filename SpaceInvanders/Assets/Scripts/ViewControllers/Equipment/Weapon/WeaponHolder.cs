namespace Assets.Scripts.ViewControllers.Equipment.Weapon
{
    public class WeaponHolder : AbstractEquipmentHolder
    {
        private WeaponController _weaponController;

        public override EquipmentType EquipType
        {
            get { return EquipmentType.WEAPON; }
        }

        public override AbstractEquipmentController EquipController
        {
            get { return _weaponController; }
            protected set { _weaponController = (WeaponController) value; }
        }
    }
}