using System.Collections;
using UnityEngine;


public abstract class BaseActorController <TModel> : MonoBehaviour where TModel:BaseActorModel
{
	protected TModel _model;
	public void Init(TModel model)
	{
		_model = model;
		OnInit();
	}

	void OnDestroy()
	{
		_model.Remove();
		_model = null;
		Release();
	}

	abstract protected void OnInit();
	abstract protected void Release();
}
