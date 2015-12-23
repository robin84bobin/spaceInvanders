using UnityEngine;
using System.Collections;


public class BaseWindow : MonoBehaviour 
{
	protected WindowParams _windowsParameters;

	public void Hide ()
	{
		OnHide();
		Main.inst.windows.HideWindow(this);
	}

	public virtual void OnShowComplete(WindowParams param = null)
	{
		_windowsParameters = param;
	}

	protected virtual void OnHide()
	{
	}
}
