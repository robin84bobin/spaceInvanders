using Assets.Scripts.ModelComponents.Actors;
using Assets.Scripts.ModelComponents.Equipments;
using Assets.Scripts.ViewControllers.Equipment;
using UnityEngine;

namespace Assets.Scripts.ViewControllers
{
    public abstract class BaseActorController<TModel> : MonoBehaviour where TModel : BaseActorModel
    {
        protected AbstractEquipmentHolder[] equipmentHolders;

        protected TModel model;

        public void Init(TModel model_)
        {
            equipmentHolders = GetComponentsInChildren<AbstractEquipmentHolder>();

            model = model_;
            OnInit();
        }

        private void OnDestroy()
        {
            model.Remove();
            model = null;
            Release();
        }

        
        public void EquipItem(IEquipmentModel equipment_)
        {
            EquipmentType _newType = EquipmentType.CLOTH;
            foreach (AbstractEquipmentHolder holder in equipmentHolders) {
                _newType = EquipmentHelper.GetEquipType(equipment_.Type);
                if (holder.EquipType != _newType) continue;
                holder.UnequipItem();
                holder.EquipItem(equipment_);
            }
        }

        protected abstract void OnInit();
        protected abstract void Release();
    }
}