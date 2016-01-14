using UnityEngine;
using System.Collections;

public abstract class BaseActorController : MonoBehaviour 
{

	public void Init(BaseActorModel model)
	{
		GameObject go = new GameObject();
		go.name = model.GetType().Name;
		go.transform.parent = this.transform;

		OnInit(model);
	}

	public virtual void OnCollision(BaseActorController other)
	{

	}

	abstract protected void OnInit(BaseActorModel model);
}
