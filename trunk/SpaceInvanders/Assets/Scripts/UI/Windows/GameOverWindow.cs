using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverWindow : BaseWindow 
{
	public Text stateHeaderText;
	public Text scoreValueText;
	public InputField nameInputField;

	public static void Show()
	{
		Main.inst.windows.Show("GameOverWindow");
	}

	public void OnSaveScore()
	{
		if (string.IsNullOrEmpty(nameInputField.text)){
			InfoWindow.Show("Please enter your name");
			return;
		}

		//TODO
		//DataLoader.Instance.SaveResults(nameInputField.text,
	}

}
