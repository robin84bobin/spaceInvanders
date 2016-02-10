using Assets.Scripts.ModelComponents.Collisions;
using UnityEngine;

namespace Assets.Scripts.ViewControllers
{
    public class CollisionController : MonoBehaviour
    {
        public void Init(CollisionInfo collisionInfo_)
        {
            CollisionInfoData = collisionInfo_;
        }

        public CollisionInfo CollisionInfoData { get; private set; }
    }
}