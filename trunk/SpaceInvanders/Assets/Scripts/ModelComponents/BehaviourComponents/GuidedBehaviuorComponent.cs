using Assets.Scripts.Events;
using Assets.Scripts.Events.CustomEvents;
using Assets.Scripts.ModelComponents.CommonComponents;
using UnityEngine;

namespace Assets.Scripts.ModelComponents.BehaviourComponents
{
    public class GuidedBehaviuorComponent: BaseComponent
    {

        protected override void OnInit()
        {
            AddComponent( new TimerComponent(5f, Remove));
            EventManager.Get<MoveControlsEvent> ().Subscribe (OnUpdateControl);
        }

        void OnUpdateControl (Vector3 moveVector_)
        {
            if (locked) {
                return;
            }
            Parent.SendMessage("Move",moveVector_);
        }

        protected override void OnRelease()
        {
            EventManager.Get<MoveControlsEvent> ().Unsubscribe (OnUpdateControl);
        }
    }
}


