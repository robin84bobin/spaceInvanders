using System;

public class LevelStatePlay  : ILevelState
{
	LevelModel _ownerModel;

	public LevelStatePlay (LevelModel ownerModel)
	{
		_ownerModel = ownerModel;
	}

	#region ILevelState implementation

	public void Update ()
	{
		_ownerModel.UpdateGamePlay ();
	}

	public void OnEnterState ()
	{
		Main.inst.input.SetState (LevelInputMode);
	}

	public void OnExitState ()
	{
		//
	}

	public InputMode LevelInputMode {
		get {
			return InputMode.GAME;
		}
	}
	#endregion


}

