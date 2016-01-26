using Assets.Scripts.ModelComponents.ActorComponents;
using UnityEngine;

namespace Assets.Scripts.GameActorControllers
{
    public class EnemyController : BaseActorController<EnemyModel>
    {
        #region implemented abstract members of BaseActorController

        protected override void OnInit ()
        {
            model.MoveEvent += OnMove;
        }

        protected override void Release ()
        {
            model = null;
        }

        #endregion

        void OnMove (Vector3 moveVector_)
        {
            transform.Translate (moveVector_ * 3f);
        }

        public float GetYSize ()
        {
            return transform.localScale.y;
        }

    }
}