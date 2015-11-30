using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Parse;


public sealed class DataController
{
	//-----storages-----
	public LevelStorage levelStorage;
	public BaseStorage<HeroData> HeroStorage;
	public BaseStorage<BulletData> BulletStorage;
	public BaseStorage<WeaponData> WeaponStorage;
	public BaseStorage<EnemyData> EnemyStorage;
	//------------------
	private List<IBaseStorage> _referenceStorages;
	//------------------
	private UserStorage _userStorage;


	public void Init ()
	{
		EventManager.Get<StorageLoadCompleteEvent> ().Subscribe (LoadNextStorage);
		EventManager.Get<StorageUpdateCompleteEvent> ().Subscribe (LoadStorage);
		DataProxy.Instance.Init ();
		InitStorages();
		StartLoadStorages ();
	}

	private void InitStorages()
	{
		_referenceStorages = new List<IBaseStorage>();

		levelStorage = new LevelStorage(DataTypes.LEVEL);
		_referenceStorages.Add (levelStorage);
		EnemyStorage = RegisterBaseStorage<EnemyData>(DataTypes.ENEMY);
		BulletStorage = RegisterBaseStorage<BulletData>(DataTypes.BULLET);
		HeroStorage = RegisterBaseStorage<HeroData>(DataTypes.HERO);
		WeaponStorage = RegisterBaseStorage<WeaponData>(DataTypes.WEAPON);
	}

	private BaseStorage<T> RegisterBaseStorage<T>(string table) where T:BaseData, new()
	{
		BaseStorage<T> storage = new BaseStorage<T>(table);
		_referenceStorages.Add(storage);
		return storage;
	}

	int nextStorageIndex = -1;
	void StartLoadStorages ()
	{
		nextStorageIndex = 0;
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
		if ( nextStorageIndex >= _referenceStorages.Count){
			OnStoragesUpdateComplete();
			return;
		}

		if(_referenceStorages[nextStorageIndex] == null){
			nextStorageIndex++;
			LoadNextStorage();
			return;
		}

		_referenceStorages [nextStorageIndex].LoadData();
		nextStorageIndex ++;
	}

	void LoadStorage (string dataType)
	{
		_referenceStorages.FirstOrDefault (storage => storage.DataType == dataType).LoadData();
	}
}


