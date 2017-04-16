using Assets.Scripts.ModelComponents.Entities;
using UnityEngine;

namespace Assets.Scripts.ViewControllers
{
    public class BulletController : BaseEntityController<BulletModel> 
    {
        #region implemented abstract members of BaseEntityController

        protected override void Release ()
        {
           // throw new System.NotImplementedException ();
        }

        private Vector3 _speed;
        protected override void OnInit ()
        {
           _speed = new Vector3(0f, model.speed, 0f);
        }

        #endregion
        
        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update ()
        {
	          transform.Translate(_speed); 
        }
    }
}
