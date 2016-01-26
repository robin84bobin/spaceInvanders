using UnityEngine;

namespace Assets.Scripts.GameActorControllers
{
    public class Spinner : MonoBehaviour 
    {
        public float xSpeed;
        public float ySpeed;
        public float zSpeed;

        private Vector3 _rotVector;
        void Start()
        {
            _rotVector = new Vector3 (xSpeed, ySpeed, zSpeed);
        }
	
        void Update () 
        {
            //this.transform.rotation = Quaternion.Euler (rotateVector);
            transform.Rotate (_rotVector);
        }
    }
}
