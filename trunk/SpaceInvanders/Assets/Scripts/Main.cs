using UnityEngine;
using System.Collections;
using Parse;
using System;

public sealed class Main : MonoBehaviour 
{
	//----------------------------------
	public InputManager input;
	public WindowManager windows;
	public GameManager game;
	//----------------------------------

	public ParseInitializeBehaviour parse;

	private DataController _dataController;
	public DataController Data{
		get {
			return _dataController;
		}
	}

	private static Main _instance;
	public static Main inst {
		get {
			return _instance;
		}
	}

	void Start()
	{
		_instance = this;
		GameObject.DontDestroyOnLoad(this.gameObject);
		Init ();
	}

	void Init ()
	{
		EventManager.Get<DataInitCompleteEvent> ().Subscribe (OnDataInited);
		//PreloaderWindow.Show();

		AuthWindow.Show ();
		//StartLoadData();
	}

	void StartLoadData()
	{
		_dataController = new DataController();
		_dataController.Init ();
	}

	void OnDataInited ()
	{
		EventManager.Get<DataInitCompleteEvent> ().Unsubscribe (OnDataInited);
		Debug.Log ("COMPLETE!");
	}
}
