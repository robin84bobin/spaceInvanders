using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public interface IBaseStorage
{
	string DataType {get;}
	void LoadData();
}

public class BaseStorage <TData> : IBaseStorage where TData : IBaseData, new()
{
	protected Dictionary<string,IBaseData> _objects;
	protected string _dataType;

	public BaseStorage(string dataType)
	{
		_dataType = dataType;
		_objects = new Dictionary<string, IBaseData> ();
	}

	public string DataType {
		get { 
			return _dataType; 
		}
	}

	public void LoadData()
	{
		EventManager.Get<LoadProgressEvent> ().Publish ( GetLoadMessage ());
		DataLoader.Instance.LoadData<TData> (_dataType, OnLoadDataComplete);
	}

	protected void OnLoadDataComplete(Dictionary<string,IBaseData> objects)
	{
		Debug.Log (string.Format("LOADED DATA: {0} ", _dataType));
		_objects = objects;
		Log (_objects);
		EventManager.Get<StorageLoadCompleteEvent>().Publish();
	}

	public TData Get (string objectId)
	{
		if (!_objects.ContainsKey(objectId)){
			Debug.LogError(string.Format("Can't get data from '{0}' storage by objectId:{1}", _dataType ,objectId));
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
		return string.Format("Loading: \"{0}\" ...", _dataType);
	}

	string GetUpdateMessage()
	{
		return string.Format("Updating: \"{0}\" ...", _dataType);
	}
}
