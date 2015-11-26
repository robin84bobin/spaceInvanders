using UnityEngine;
using System.Collections;

public class AuthWindow : BaseWindow 
{
	public LoginController loginController;
	public SignupController signupController;

	public static void Show ()
	{
		Main.inst.windows.Show(	"AuthWindow");
	}

	public override void OnShowComplete(WindowParams param = null)
	{
		base.OnShowComplete (param);

	}
}
