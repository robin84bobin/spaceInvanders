using Assets.Scripts.ModelComponents.Equipments;
using UnityEngine;

namespace Assets.Scripts.ViewControllers.Equipment
{
    public abstract class AbstractEquipmentController : MonoBehaviour
    {
        public abstract void Init(IEquipmentModel model_);
        protected abstract void OnEnable();
        protected abstract void OnDisable();
        public abstract void Release();

    }
}