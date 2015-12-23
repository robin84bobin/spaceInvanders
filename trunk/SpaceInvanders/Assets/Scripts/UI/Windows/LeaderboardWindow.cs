using UnityEngine;
using System.Collections;

public class Leaderboardwindow : MonoBehaviour {

	public static void Show ()
	{
		Main.inst.windows.Show(	"Leaderboardwindow");
	}
}
