using Assets.Scripts.ModelComponents.Actors;
using UnityEngine;

namespace Assets.Scripts.ViewControllers
{
    public class BulletController : BaseActorController<BulletModel> 
    {
        #region implemented abstract members of BaseActorController

        protected override void Release ()
        {
           // throw new System.NotImplementedException ();
        }

        private Vector3 speed;
        protected override void OnInit ()
        {
           speed = new Vector3(0f, model.speed, 0f);
        }

        #endregion
        
        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	          transform.Translate(speed); 
        }
    }
}
