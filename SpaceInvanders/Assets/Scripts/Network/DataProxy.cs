using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Data.DB;
using Assets.Scripts.Events;
using Assets.Scripts.Events.CustomEvents;
using UnityEngine;

namespace Assets.Scripts.Network
{
    public class DataProxy
    {
        private IDataBaseProxy _dbProxy;
        public IDataBaseProxy LocalDb {
            get {
                return _dbProxy;
            }
        }

        private IWebDataProxy _webProxy;
        public IWebDataProxy WebDb{
            get {
                return _webProxy;
            }
        }

        private static DataProxy _instance;
        public static DataProxy Instance {
            get {
                if (_instance == null){
                    _instance = new DataProxy();
                }
                return _instance;
            }
        }

        public void Init()
        {
            _webProxy = new ParseProxy ();
            _dbProxy = new JsonProxy ();
            _dbProxy.Init ();
        }

        private bool NeedUpdate(string tableName_)
        {
            if (!CheckConnection ()) {
                return false;
            }

            if (!_dbProxy.IsTableExist (tableName_)) {
                return true;
            }
            else
                if (_webProxy.LastUpdateTime (tableName_) > _dbProxy.LastUpdateTime (tableName_)) {
                    return true;
                }
            return false;
        }

        public void LoadData<TData>(string tableName_, Action<string, Dictionary<string,TData>> callback_) where TData:IBaseData, new()
        {
            if (NeedUpdate (tableName_)) {
                _webProxy.GetTableData (tableName_, OnUpdateComplete<TData>);
                callback_ = null;
            } else {
                _dbProxy.GetTableData <TData> (tableName_, callback_);
            }
        }

        void OnUpdateComplete<TData> (string tableName_, Dictionary<string, IBaseData> dataDict_)  where TData:IBaseData
        {
            LocalDb.SaveTableData<TData>(tableName_, dataDict_);
            EventManager.Get<StorageUpdateCompleteEvent>().Publish(typeof(TData));
        }

        public void SaveData<T>(string tableName_, int id_)
        {
            throw new NotImplementedException();
        }

        public void SaveResults(string playerName_, int score_)
        {
            _webProxy.SaveScores(playerName_,score_);
        }

        public bool CheckConnection()
        {
            //TODO
            return false;
            //
            ConnectionTesterStatus connectionStatus = UnityEngine.Network.TestConnection ();
            if (connectionStatus == ConnectionTesterStatus.Error){
                return false;
            }
            return true;
        }

        private DataProxy (){}
    }
}


