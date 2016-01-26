using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Events;
using Assets.Scripts.Events.CustomEvents;
using Assets.Scripts.GameActorControllers;
using Assets.Scripts.ModelComponents.Level;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public sealed class GameManager : MonoBehaviour 
    {
        private int _currentLevelId = 1;

        private LevelController _currentLevel;
        private LevelModel _levelModel;
        private LevelData _currentLevelData;

        public void LoadCurrentLevel()
        {
            EventManager.Get<LevelStartEvent> ().Subscribe (OnLevelStart);
            _currentLevelData =	Main.Inst.Data.Get<LevelData>(level_ => level_.Id == _currentLevelId);
            SceneManager.LoadScene(_currentLevelData.LevelSceneName);
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
}
