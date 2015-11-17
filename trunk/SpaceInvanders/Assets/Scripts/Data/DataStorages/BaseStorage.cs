using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public interface IBaseStorage
{
	void LoadData();
	void UpdateWebData();
	void LoadDBData ();
}

public class BaseStorage <TData> : IBaseStorage where TData : IBaseData, new()
{
	protected Dictionary<string,IBaseData> _objects;
	protected string _tableName;

	public BaseStorage(string tableName)
	{
		_tableName = tableName;
		_objects = new Dictionary<string, IBaseData> ();
	}

	public void LoadData()
	{

	}

	public void UpdateWebData()
	{
		EventManager.Get<LoadProgressEvent> ().Publish (GetUpdateMessage ());
		DataLoader.Instance.LoadWebData(_tableName, OnUpdateComplete);
	}

	protected void OnUpdateComplete( Dictionary<string,IBaseData> objects)
	{
		Debug.Log (string.Format("UPDATED DATA: {0} ", _tableName));
		OnLoadDataComplete (objects);

		DataLoader.Instance.SaveDataToDB<TData> (_tableName, _objects);
	}

	public void LoadDBData()
	{
		EventManager.Get<LoadProgressEvent> ().Publish (GetLoadMessage ());
		DataLoader.Instance.LoadDBData<TData> (_tableName, OnLoadDataComplete, OnLoadDBFail);
	}

	void OnLoadDBFail(string table)
	{
		UpdateWebData ();
	}

	protected void OnLoadDataComplete(Dictionary<string,IBaseData> objects)
	{
		Debug.Log (string.Format("LOADED DATA: {0} ", _tableName));
		_objects = objects;
		Log (_objects);
		EventManager.Get<StorageLoadCompleteEvent>().Publish();
	}

	public TData Get (string objectId)
	{
		if (!_objects.ContainsKey(objectId)){
			Debug.LogError(string.Format("Can't get data from '{0}' storage by objectId:{1}", _tableName ,objectId));
			return default(TData);
		}

		return (TData)_objects[objectId];
	}

	void Log(Dictionary<string,IBaseData> dict)
	{
		foreach (var item in dict) {
			Debug.Log(string.Format(">> {0} : {1}", item.Value.Type, item.Value.ObjectId));
		}
	}

	string GetLoadMessage ()
	{
		return string.Format("Loading: \"{0}\" ...", _tableName);
	}

	string GetUpdateMessage()
	{
		return string.Format("Updating: \"{0}\" ...", _tableName);
	}
}
