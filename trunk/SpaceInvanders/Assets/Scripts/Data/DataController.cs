using System;
using System.Collections.Generic;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Data.DataSource.Impacts;
using Assets.Scripts.Data.DataStorages;
using Assets.Scripts.Data.User;
using Assets.Scripts.Events;
using Assets.Scripts.Events.CustomEvents;
using Assets.Scripts.Network;
using UnityEngine;
using SkillImpactData = Assets.Scripts.Data.DataSource.Impacts.Damage.SkillImpactData;

namespace Assets.Scripts.Data
{
    public sealed class DataController
    {
        //------------------
        private Dictionary<Type, IBaseStorage> _storageTypeMap;
        private Dictionary<string, IBaseStorage> _storageTypeNameMap;
        //------------------
        private UserStorage _userStorage;
        public UserStorage UserStorage
        {
            get { return _userStorage; }
        }

        public UserData userSessionData;


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
            UserData user = _userStorage.Get("robin84bobin@gmail.com");
            user.level ++;
            _userStorage.SaveData();
        }

        private void InitStorages()
        {
            _storageTypeMap = new Dictionary<Type, IBaseStorage>();
            _storageTypeNameMap = new Dictionary<string, IBaseStorage>();

            RegisterBaseStorage<LevelData>(DataTypes.LEVEL);
            RegisterBaseStorage<EnemyData>(DataTypes.ENEMY);
            RegisterBaseStorage<BulletData>(DataTypes.BULLET);
            RegisterBaseStorage<HeroData>(DataTypes.HERO);
            RegisterBaseStorage<WeaponData>(DataTypes.WEAPON);
            RegisterBaseStorage<BonusData>(DataTypes.BONUS);
            RegisterBaseStorage<SkillImpactData>(DataTypes.SKILL_IMPACT);
            RegisterBaseStorage<TimerData>(DataTypes.TIMER);
            RegisterBaseStorage<BehaviourImpactData>(DataTypes.BEHAVIOUR_IMPACT);
            RegisterBaseStorage<PeriodImpactData>(DataTypes.PERIOD_IMPACT);

            _userStorage = new UserStorage();
            _storageTypeMap.Add(UserStorage.GetType(), _userStorage);
        }

        public IBaseStorage StorageByName(string typeName_)
        {
            if (_storageTypeNameMap.ContainsKey(typeName_))
            {
                return ((IBaseStorage)_storageTypeNameMap[typeName_]);
            }
            Debug.LogError("Storage not found for data type: " + typeName_);
            return null;
        }

        public BaseStorage<T> Storage<T>() where T:BaseData, new()
        {
            Type type = typeof(T);
            if (_storageTypeMap.ContainsKey(type)){
                return ((BaseStorage<T>)_storageTypeMap[type]);
            }
            Debug.LogError("Storage not found for data type: "+ type.Name);
            return null;
        }

        public IBaseData Get(string dataType_, string objectId_)
        {
            return StorageByName(dataType_).Get(objectId_);
        }

        public T Get<T>(string objectId_) where T:BaseData, new() 
        {
            return Storage<T>().Get(objectId_);
        }

        public T Get<T>(Func<T,bool> predicate_) where T:BaseData, new() 
        {
            return Storage<T>().Get(predicate_);
        }

        private BaseStorage<T> RegisterBaseStorage<T>(string name_) where T:BaseData, new()
        {
            BaseStorage<T> storage = new BaseStorage<T>(name_);
            _storageTypeMap.Add(typeof(T),storage);
            _storageTypeNameMap.Add(name_, storage);
            return storage;
        }

        int _nextStorageIndex = -1;
        private Queue<IBaseStorage> _storageLoadQueue;
        void StartLoadStorages ()
        {
            _nextStorageIndex = 0;
            _storageLoadQueue = new Queue<IBaseStorage>( _storageTypeMap.Values);
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
            if (!_storageTypeMap.ContainsKey(dataType_)){
                Debug.Log("Data storage does not exist:" + dataType_.Name);
                return;
            }
            _storageTypeMap[dataType_].LoadData();
        }
    }
}


