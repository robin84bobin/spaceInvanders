using System;

public enum LevelStates
{
	PLAY,
	PAUSE
}

public interface ILevelState : IBaseState
{
	void Update();
	InputMode LevelInputMode { get; }
}


