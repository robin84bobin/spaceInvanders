using Assets.Scripts.ModelComponents.Actors;
using Assets.Scripts.ModelComponents.Equipments;
using Assets.Scripts.ViewControllers.Equipment;
using UnityEngine;

namespace Assets.Scripts.ViewControllers
{
    public abstract class BaseActorController<TModel> : MonoBehaviour, IBaseActorController 
                                                        where TModel : BaseActorModel
    {
        protected AbstractEquipmentHolder[] equipmentHolders;

        protected TModel model;
        public TModel Model
        {
            get { return model; }
        }

        public void Init(BaseActorModel model_)
        {
            equipmentHolders = GetComponentsInChildren<AbstractEquipmentHolder>();

            model = (TModel) model_;
            OnInit();
        }

        void Update()
        {
            if (model != null) {
                model.Update();
            }
        }

        private void OnDestroy()
        {
            model.Remove();
            model = null;
            Release();
        }

        EquipmentType _newType = EquipmentType.CLOTH;
        public void EquipItem(IEquipmentModel equipment_)
        {
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

    public interface IBaseActorController
    {
        void Init(BaseActorModel model_);
    }
}