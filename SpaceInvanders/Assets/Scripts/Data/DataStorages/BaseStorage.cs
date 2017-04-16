using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Events;
using Assets.Scripts.Events.CustomEvents;
using Assets.Scripts.Network;
using UnityEngine;

namespace Assets.Scripts.Data.DataStorages
{
    public interface IBaseStorage
    {
        string DataTypeName {get;}
        IBaseData Get(string objectId_ );
        void LoadData();
    }

    public class BaseStorage <T> : IBaseStorage where T : IBaseData, new()
    {
        protected readonly bool readOnly;
        protected Dictionary<string,T> objects;

        protected string dataType;

        public BaseStorage(string dataType_, bool readOnly_ = true)
        {
            readOnly = readOnly_;
            this.dataType = dataType_;
            objects = new Dictionary<string, T> ();
        }

        public string DataTypeName {
            get {
                return dataType;
            }
        }

        public void LoadData()
        {
            EventManager.Get<LoadProgressEvent> ().Publish ( GetLoadMessage ());
            DataProxy.Instance.LoadData<T> (dataType, OnLoadDataComplete);
        }

        protected void OnLoadDataComplete(string tableName_, Dictionary<string,T> objects_)
        {
            Debug.Log (string.Format("LOADED DATA: {0} ", dataType));
            this.objects = objects_;
            Log (this.objects);
            EventManager.Get<StorageLoadCompleteEvent>().Publish();
        }

        public void SaveData()
        {
            if (readOnly) {
                Debug.LogError("Can't write into read only storage: " + this.GetType().Name);
                return;
            }
            DataProxy.Instance.LocalDb.SaveTableData<T>(dataType, objects);
        }

        public T Get (string objectId_)
        {
            if (!objects.ContainsKey(objectId_)){
                Debug.LogError(string.Format("Can't get data from '{0}' storage - objectId:{1}", dataType ,objectId_));
                return default(T);
            }

            return objects[objectId_];
        }

        public T Get (Func<T,bool> predicate_)
        {
            return objects.Values.FirstOrDefault(predicate_);
        }

        void Log(Dictionary<string,T> dict_)
        {
            foreach (var item in dict_) {
                Debug.Log(string.Format(" {0} > {1}", item.Value.Type, item.Value.ObjectId));
            }
        }

        string GetLoadMessage ()
        {
            return string.Format("Loading: \"{0}\" ...", dataType);
        }

        string GetUpdateMessage()
        {
            return string.Format("Updating: \"{0}\" ...", dataType);
        }

        IBaseData IBaseStorage.Get(string objectId_)
        {
            if (!objects.ContainsKey(objectId_))
            {
                Debug.LogError(string.Format("Can't get data from '{0}' storage - objectId:{1}", dataType, objectId_));
                return default(T);
            }

            return objects[objectId_];
        }
    }
}