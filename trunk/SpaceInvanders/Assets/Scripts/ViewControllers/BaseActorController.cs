using System;
using Assets.Scripts.ModelComponents.Actors;
using Assets.Scripts.ModelComponents.Collisions;
using Assets.Scripts.ModelComponents.Equipments;
using Assets.Scripts.ViewControllers.Equipment;
using UnityEngine;

namespace Assets.Scripts.ViewControllers
{
    public abstract class BaseActorController<TModel> : MonoBehaviour, IBaseActorController 
                                                        where TModel : BaseEntityModel
    {
        protected AbstractEquipmentHolder[] equipmentHolders;

        protected TModel model;
        public BaseEntityModel Model
        {
            get { return model; }
        }

        public void Init(BaseEntityModel model_)
        {
            equipmentHolders = GetComponentsInChildren<AbstractEquipmentHolder>();

            model = (TModel) model_;
            InitEvents();
            InitCollisionController();
            OnInit();
        }

        private void InitEvents()
        {
            model.DeathEvent += () =>
            {
                model.Release();
                Destroy(gameObject);
            };
        }

        private void InitCollisionController()
        {
            if (model.CollisionInfoData != null) {
                gameObject.AddComponent<CollisionController>().Init(model.CollisionInfoData);
            }
        }

        void Update()
        {
            if (model != null) {
                model.Update();
            }
        }

        void OnTriggerEnter(Collider other_)
        {
            CollisionController cc = other_.GetComponent<CollisionController>();
            if (cc != null && cc.CollisionInfoData != null) {
                cc.CollisionInfoData.Apply(this.model);
            }
        }

        protected void ApplyCollisionInfo(CollisionInfo info_)
        {
            info_.Apply(this.model);
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
                break;
            }
        }

        protected abstract void OnInit();
        protected abstract void Release();
    }

    public interface IBaseActorController
    {
        void Init(BaseEntityModel model_);
        BaseEntityModel Model { get; }
    }
}