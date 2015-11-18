using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindowManager : MonoBehaviour 
{
	public Canvas uiRoot;

	void Awake()
	{
		Init ();
	}

	private List<BaseWindow> _shownWindows;
	void Init()
	{
		GameObject.DontDestroyOnLoad(uiRoot.gameObject);
		GameObject.DontDestroyOnLoad(this.gameObject);
		_shownWindows = new List<BaseWindow>();
	}

	public GameObject LoadPrefab(string _prefabName)
	{
		string path = string.Format ("UI/Windows/{0}", _prefabName);
		GameObject go = Resources.Load(path) as GameObject;
		go.name = _prefabName;
		return go;
	}

	public void Show(string windowName, WindowParams param = null)
	{
		HideAllWindows ();
		GameObject windowGO = Main.inst.windows.LoadPrefab(windowName);
		BaseWindow newWindow = InstantiateWindow(windowGO);
		newWindow.OnShowComplete(param);
		_shownWindows.Add(newWindow);
	}

	BaseWindow InstantiateWindow(GameObject windowGO)
	{
		GameObject parent = uiRoot.gameObject;
		GameObject go = GameObject.Instantiate(windowGO) as GameObject;
		if (go != null && parent != null)
		{
			Transform t = go.transform;
			t.parent = parent.transform;
			t.localPosition = Vector3.zero;
			t.localRotation = Quaternion.identity;
			t.localScale = Vector3.one;
			go.layer = parent.layer;
			go.SetActive(true);
		}

		return go.GetComponent<BaseWindow> ();
	}

	void HideAllWindows ()
	{
		for (int i = 0; i < _shownWindows.Count; i++) {
			_shownWindows[i].Hide();
		}
	}

	public void HideWindow (BaseWindow window)
	{
		_shownWindows.Remove(window);

		window.transform.parent = null;
		GameObject.Destroy(window.gameObject);
	}
}
