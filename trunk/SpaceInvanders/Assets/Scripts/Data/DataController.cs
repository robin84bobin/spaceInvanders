using System;
using System.Collections.Generic;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Data.DataStorages;
using Assets.Scripts.Events;
using Assets.Scripts.Events.CustomEvents;
using Assets.Scripts.Network;
using UnityEngine;

namespace Assets.Scripts.Data
{
    public sealed class DataController
    {
        //------------------
        private Dictionary<Type, IBaseStorage> _storageMap;
        //------------------
        private UserStorage _userStorage;

        public UserData.UserData userSessionData;

        public void Init ()
        {
            EventManager.Get<StorageLoadCompleteEvent> ().Subscribe (LoadNextStorage);
            EventManager.Get<StorageUpdateCompleteEvent> ().Subscribe (LoadStorage);
            DataProxy.Instance.Init ();
            InitStorages();
            StartLoadStorages ();
        }

        public void StartUserSession()
        {
            userSessionData = new UserData.UserData ();
        }

        private void InitStorages()
        {
            _storageMap = new Dictionary<Type, IBaseStorage>();

            RegisterBaseStorage<LevelData>(DataTypes.LEVEL);
            RegisterBaseStorage<EnemyData>(DataTypes.ENEMY);
            RegisterBaseStorage<BulletData>(DataTypes.BULLET);
            RegisterBaseStorage<HeroData>(DataTypes.HERO);
            RegisterBaseStorage<WeaponData>(DataTypes.WEAPON);
        }

        public BaseStorage<T> Storage<T>() where T:BaseData, new()
        {
            Type type = typeof(T);
            if (_storageMap.ContainsKey(type)){
                return ((BaseStorage<T>)_storageMap[type]);
            }
            return null;
        }

        public T Get<T>(string objectId_) where T:BaseData, new() 
        {
            return Storage<T>().Get(objectId_);
        }

        public T Get<T>(Func<T,bool> predicate_) where T:BaseData, new() 
        {
            return Storage<T>().Get(predicate_);
        }

        private BaseStorage<T> RegisterBaseStorage<T>(string table_) where T:BaseData, new()
        {
            BaseStorage<T> storage = new BaseStorage<T>(table_);
            _storageMap.Add(typeof(T),storage);
            return storage;
        }

        int _nextStorageIndex = -1;
        private Queue<IBaseStorage> _storageLoadQueue;
        void StartLoadStorages ()
        {
            _nextStorageIndex = 0;
            _storageLoadQueue = new Queue<IBaseStorage>( _storageMap.Values);
            LoadNextStorage();
        }

        void OnStoragesUpdateComplete()
        {
            EventManager.Get<StorageLoadCompleteEvent> ().Unsubscribe (LoadNextStorage);
            EventManager.Get<StorageUpdateCompleteEvent> ().Unsubscribe (LoadStorage);
            EventManager.Get<DataInitCompleteEvent> ().Publish ();
        }

        void LoadNextStorage ()
        {
            if ( _storageLoadQueue.Count <= 0){
                OnStoragesUpdateComplete();
                return;
            }

            IBaseStorage currentStorage = _storageLoadQueue.Dequeue();
            if(currentStorage == null){
                LoadNextStorage();
                return;
            }

            currentStorage.LoadData();
        }

        void LoadStorage (Type dataType_)
        {
            if (!_storageMap.ContainsKey(dataType_)){
                Debug.Log("Data storage does not exist:" + dataType_.Name);
                return;
            }
            _storageMap[dataType_].LoadData();
        }
    }
}


