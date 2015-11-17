using UnityEngine;
using System.Collections;



public class InfoWindow : MonoBehaviour {

	public static void Show(string message)
	{
		InfoWindowParams param = new InfoWindowParams (message);
		Show (param);
	}

	public static void Show(InfoWindowParams param = null)
	{
		Main.inst.windows.Show(	"InfoWindow", param);
	}
}
