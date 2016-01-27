using Assets.Scripts.ModelComponents.Equipments;

namespace Assets.Scripts.ViewControllers.Equipment.Weapon
{
    public class WeaponController : AbstractEquipmentController
    {
        private WeaponModel _model;

        public override void Init(IEquipmentModel model_)
        {
            _model = (WeaponModel)model_;
        }

        protected override void OnEnable()
        {
            _model.OnEquip();
        }

        protected override void OnDisable()
        {
            _model.OnUnequip();
        }

        public override void Release()
        {
            _model.Release();
        }
    }
}