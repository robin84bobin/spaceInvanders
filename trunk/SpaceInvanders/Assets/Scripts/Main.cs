using Assets.Scripts.Controllers;
using Assets.Scripts.Data;
using Assets.Scripts.Events;
using Assets.Scripts.Events.CustomEvents;
using Assets.Scripts.Input;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Windows;
using Parse;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class Main : MonoBehaviour 
    {
        //----------------------------------
        public InputManager input;
        public WindowManager windows;
        public GameManager game;
        //----------------------------------

        public ParseInitializeBehaviour parse;

        private DataController _dataController;
        public DataController Data {
            get {
                return _dataController;
            }
        }

        private static Main _instance;
        public static Main Inst {
            get {
                return _instance;
            }
        }

        public AuthController auth;

        void Start()
        {
            _instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
            Init ();
        }

        void Init ()
        {
            EventManager.Get<DataInitCompleteEvent> ().Subscribe (OnDataInited);
            PreloaderWindow.Show();
            StartLoadData();
        }

        void StartLoadData()
        {
            _dataController = new DataController();
            _dataController.Init ();
            _dataController.StartUserSession ();
        }

        void OnDataInited ()
        {
            EventManager.Get<DataInitCompleteEvent> ().Unsubscribe (OnDataInited);
            Debug.Log ("COMPLETE!");
        }
    }
}
