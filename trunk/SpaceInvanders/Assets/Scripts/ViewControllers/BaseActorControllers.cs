using Assets.Scripts.ModelComponents.ActorComponents;
using UnityEngine;

namespace Assets.Scripts.GameActorControllers
{
    public abstract class BaseActorController <TModel> : MonoBehaviour where TModel:BaseActorModel
    {
        protected TModel model;
        public void Init(TModel model_)
        {
            this.model = model_;
            OnInit();
        }

        void OnDestroy()
        {
            model.Remove();
            model = null;
            Release();
        }

        abstract protected void OnInit();
        abstract protected void Release();
    }
}
