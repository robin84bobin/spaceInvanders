using System;

public class LevelStatePause  : ILevelState
{
	LevelModel _ownerModel;
	public LevelStatePause (LevelModel ownermodel)
	{
		_ownerModel = ownermodel;
	}

	#region ILevelState implementation

	public void Update ()
	{
		//
	}

	public void OnEnterState ()
	{
		Main.inst.input.SetState (InputMode.UI);
		_ownerModel.Pause ();
	}

	public void OnExitState ()
	{
		//
	}

	public InputMode LevelInputMode {
		get {
			return InputMode.UI;
		}
	}

	#endregion
}

