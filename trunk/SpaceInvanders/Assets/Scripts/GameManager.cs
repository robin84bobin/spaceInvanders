using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public sealed class GameManager : MonoBehaviour 
{
	private int _currentLevelID = 1;

	private LevelController _currentLevel;
	private LevelModel _levelModel;
	private LevelData _currentLevelData;

	public void LoadCurrentLevel()
	{
		EventManager.Get<LevelStartEvent> ().Subscribe (OnLevelStart);
		_currentLevelData =	Main.inst.Data.Get<LevelData>(level => level.ID == _currentLevelID);
		SceneManager.LoadScene(_currentLevelData.levelSceneName);
	}

	private void OnLevelStart()
	{
		EventManager.Get<LevelStartEvent> ().Unsubscribe (OnLevelStart);
		_currentLevel = (LevelController) FindObjectOfType (typeof(LevelController));
		_levelModel = new LevelModel (_currentLevelData);
		_currentLevel.Attach (_levelModel);

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
