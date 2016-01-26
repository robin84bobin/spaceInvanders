using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.BehaviourComponents;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.ActorComponents
{
    public class HeroModel: BaseActorModel
    {
        private WeaponModel _weapon;

        public HeroModel (HeroData data_):base(data_)
        {
            var data = data_;
            //
            data.MaxHealth = 4;

            if (data.Weapon != null) {
                _weapon = new WeaponModel (data.Weapon);
            }
        }


        public void Move(Vector3 vector_)
        {
            OnMoveEvent (vector_);
        }

	
        #region implemented abstract members of BaseComponent

        protected override void OnRelease ()
        {
            //;
        }


        protected override void OnInit ()
        {
            AddComponent (new GuidedBehaviuorComponent ());
        }

        #endregion

    }
}


