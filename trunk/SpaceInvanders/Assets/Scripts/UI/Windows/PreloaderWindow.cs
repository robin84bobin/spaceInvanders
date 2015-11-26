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

		EventManager.Get<LoadProgressEvent> ().Subscribe (OnLoadProgressEvent);
		EventManager.Get<DataInitCompleteEvent> ().Subscribe (OnLoadComplete);

		loadingStatusText.text = "please wait...";
		startButton.onClick.AddListener (OnStartButton);
		startButton.gameObject.SetActive(false);
	}


	void OnLoadProgressEvent (string message)
	{
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
		EventManager.Get<LoadProgressEvent> ().Unsubscribe (OnLoadProgressEvent);
		EventManager.Get<DataInitCompleteEvent> ().Unsubscribe (OnLoadComplete);
	}

	public void OnStartButton ()
	{
		Hide ();
		Main.inst.game.LoadCurrentLevel();
	}
}
