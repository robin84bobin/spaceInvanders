using UnityEngine;

namespace Assets.Scripts.UI.Windows
{
    public class Leaderboardwindow : MonoBehaviour {

        public static void Show ()
        {
            Main.Inst.windows.Show(	"Leaderboardwindow");
        }
    }
}
