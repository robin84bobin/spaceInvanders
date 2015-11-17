using UnityEngine;
using System.Collections;


public class BaseWindow : MonoBehaviour 
{
	public void Hide ()
	{
		OnHide();
		Main.inst.windows.HideWindow(this);
	}

	public virtual void OnShowComplete(WindowParams param = null)
	{

	}

	protected virtual void OnHide()
	{
	}
}
