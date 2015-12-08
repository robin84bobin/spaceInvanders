using UnityEngine;
using System.Collections;
using Data;

public sealed class GameManager : MonoBehaviour 
{
	private int _currentLevelID = 1;

	private Level _currentLevel;
	private LevelModel _levelModel;
	private LevelData _currentLevelData;

	public void LoadCurrentLevel()
	{
		EventManager.Get<LevelStartEvent> ().Subscribe (OnLevelStart);
		_currentLevelData = Main.inst.Data.levelStorage.GetByID (_currentLevelID);
		Application.LoadLevel(_currentLevelData.levelSceneName);
	}

	private void OnLevelStart()
	{
		EventManager.Get<LevelStartEvent> ().Unsubscribe (OnLevelStart);

		Level level = (Level) FindObjectOfType (typeof(Level));
		_levelModel = new LevelModel (_currentLevelData);
		level.Attach (_levelModel);

		_levelModel.Start ();
	}

	private void OnLevelClose()
	{
		_currentLevel.Release ();
	}

	public void Update()
	{
		if (_levelModel != null) {
			_levelModel.UpdateModel();
		}
	}
}
