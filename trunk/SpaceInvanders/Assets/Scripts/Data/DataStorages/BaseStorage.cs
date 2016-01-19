using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public interface IBaseStorage
{
	string DataTypeName {get;}
	void LoadData();
}

public class BaseStorage <T> : IBaseStorage where T : IBaseData, new()
{

	protected Dictionary<string,T> _objects;

	protected string _dataType;

	public BaseStorage(string dataType)
	{
		_dataType = dataType;
		_objects = new Dictionary<string, T> ();
	}

	public string DataTypeName {
		get {
			return _dataType;
		}
	}

	public void LoadData()
	{
		EventManager.Get<LoadProgressEvent> ().Publish ( GetLoadMessage ());
		DataProxy.Instance.LoadData<T> (_dataType, OnLoadDataComplete);
	}

	protected void OnLoadDataComplete(string tableName, Dictionary<string,T> objects)
	{
		Debug.Log (string.Format("LOADED DATA: {0} ", _dataType));
		_objects = objects;
		Log (_objects);
		EventManager.Get<StorageLoadCompleteEvent>().Publish();
	}

	public T Get (string objectId)
	{
		if (!_objects.ContainsKey(objectId)){
			Debug.LogError(string.Format("Can't get data from '{0}' storage - objectId:{1}", _dataType ,objectId));
			return default(T);
		}

		return _objects[objectId];
	}

	public T Get (Func<T,bool> predicate)
	{
		return _objects.Values.FirstOrDefault(predicate);
	}

	void Log(Dictionary<string,T> dict)
	{
		foreach (var item in dict) {
			Debug.Log(string.Format(" {0} > {1}", item.Value.type, item.Value.objectId));
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
