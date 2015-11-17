using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PreloaderWindow : BaseWindow 
{
	public Text loadingStatusText;
	public Button startButton;

	public static void Show()
	{
		Main.inst.windows.Show(	"PreloaderWindow");
	}

	public override void OnShowComplete(WindowParams param = null)
	{
		base.OnShowComplete (param);
		//
		//EventManager.instance.Subscribe(EventTypes.LOAD_PROGRESS, onLoadProgress);
		//EventManager.instance.Subscribe(EventTypes.DATA_INIT_COMPLETE, onLoadComplete);

		//
		EventManager.Get<LoadProgressEvent> ().Subscribe (OnLoadProgressEvent);
		EventManager.Get<DataInitCompleteEvent> ().Subscribe (OnLoadComplete);

		loadingStatusText.text = "please wait...";
		startButton.onClick.AddListener (OnStartButton);
		startButton.gameObject.SetActive(false);
	}

	/*private void onLoadProgress (EventParam args)
	{
		Debug.Log ("onLoadProgress:"+((LoadEventArgs)args).message);
		loadingStatusText.text = ((LoadEventArgs)args).message;
	}*/

	void OnLoadProgressEvent (string message)
	{
		Debug.Log ("onLoadProgress:" + message);
		loadingStatusText.text = message;
	}

	void OnLoadComplete ()
	{
		loadingStatusText.text = "Loading Complete!";
		startButton.gameObject.SetActive(true);
	}

	protected override void OnHide ()
	{
		base.OnHide ();
		//EventManager.Instance.Unsubscribe(EventTypes.LOAD_PROGRESS, onLoadProgress);
		//EventManager.Instance.Unsubscribe(EventTypes.DATA_INIT_COMPLETE, onLoadComplete);
		EventManager.Get<LoadProgressEvent> ().Unsubscribe (OnLoadProgressEvent);
		EventManager.Get<DataInitCompleteEvent> ().Unsubscribe (OnLoadComplete);
	}

	public void OnStartButton ()
	{
		Hide ();
		Main.inst.game.LoadCurrentLevel();
	}
}
