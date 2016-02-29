using Assets.Scripts.ModelComponents.Equipments;
using UnityEngine;

namespace Assets.Scripts.ViewControllers.Equipment
{
    /// <summary>
    /// place to instantiate equipment
    /// </summary>
    public abstract class AbstractEquipmentHolder : MonoBehaviour
    {
        public abstract EquipmentType EquipType { get ; }
        public abstract AbstractEquipmentController EquipController { get; protected set; }

        public virtual void UnequipItem()
        {
            if (EquipController != null)
            Uninstantiate();
        }

        private void Uninstantiate()
        {
            EquipController.gameObject.transform.parent = null;
            EquipController.Release();
        }


        public virtual void EquipItem(IEquipmentModel model_)
        {
            InstantiateEquipment(model_);
            model_.OnEquip();
        }


        protected void InstantiateEquipment(IEquipmentModel model_)
        {
            GameObject prefab = (GameObject)Resources.Load("Prefabs/GameEntities/Equipment/" + model_.PrefabName);
            GameObject go = GameObject.Instantiate(prefab) as GameObject;
            Transform t = go.transform;
            t.parent = this.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            go.SetActive(true);

            EquipController = go.GetComponent<AbstractEquipmentController>();
            EquipController.Init(model_);
        }
    }
}
