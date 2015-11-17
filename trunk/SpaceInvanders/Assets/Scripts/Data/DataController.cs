using System;
using UnityEngine;
using Parse;
using System.Collections.Generic;

public sealed class DataController
{
	//-----storages-----
	public LevelStorage levelStorage;
	public BaseStorage<HeroData> HeroStorage;
	public BaseStorage<BulletData> BulletStorage;
	public BaseStorage<WeaponData> WeaponStorage;
	public BaseStorage<EnemyData> EnemyStorage;
	//------------------

	private List<IBaseStorage> _storages;

	public DataController()
	{

	}
	
	bool _needUpdate = false;

	public void Init ()
	{
		EventManager.Get<StorageLoadCompleteEvent> ().Subscribe (UpdateNextStorage);

		DataLoader.Instance.Init ();
		InitStorages();
		StartUpdateStorages ();
	}



	private void InitStorages()
	{
		_storages = new List<IBaseStorage>();

		levelStorage = new LevelStorage(DataTypes.LEVEL);
		_storages.Add (levelStorage);
		EnemyStorage = RegisterBaseStorage<EnemyData>(DataTypes.ENEMY);
		BulletStorage = RegisterBaseStorage<BulletData>(DataTypes.BULLET);
		HeroStorage = RegisterBaseStorage<HeroData>(DataTypes.HERO);
		WeaponStorage = RegisterBaseStorage<WeaponData>(DataTypes.WEAPON);
	}

	private BaseStorage<T> RegisterBaseStorage<T>(string table) where T:BaseData, new()
	{
		BaseStorage<T> storage = new BaseStorage<T>(table);
		_storages.Add(storage);
		return storage;
	}

	int nextStorageIndex = -1;
	void StartUpdateStorages ()
	{
		nextStorageIndex = 0;
		UpdateNextStorage();
	}

	void OnStoragesUpdateComplete()
	{
		EventManager.Get<StorageLoadCompleteEvent>().Unsubscribe(UpdateNextStorage);
		EventManager.Get<DataInitCompleteEvent> ().Publish ();
	}

	void UpdateNextStorage ()
	{
		if ( nextStorageIndex >= _storages.Count){
			OnStoragesUpdateComplete();
			return;
		}

		if(_storages[nextStorageIndex] == null){
			nextStorageIndex++;
			UpdateNextStorage();
			return;
		}

		if (_needUpdate) {
			_storages [nextStorageIndex].UpdateWebData ();
		} else {
			_storages [nextStorageIndex].LoadDBData();
		}
		nextStorageIndex ++;
	}

}


