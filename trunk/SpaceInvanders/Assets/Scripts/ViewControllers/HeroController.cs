using Assets.Scripts.ModelComponents.ActorComponents;
using UnityEngine;

namespace Assets.Scripts.GameActorControllers
{
    public class HeroController: BaseActorController<HeroModel>
    {
        Vector3 _moveVector = Vector3.zero;
        void OnMove (Vector3 moveVector_)
        {
            _moveVector.x = moveVector_.x;
            transform.Translate (_moveVector * 5f);
        }

        #region implemented abstract members of BaseActorController

        protected override void OnInit()
        {
            model.MoveEvent += OnMove;
        }

        protected override void Release ()
        {

        }

        #endregion
    }
}
