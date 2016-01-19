
using System;
using System.Collections.Generic;
using UnityEngine;

public class DataProxy
{
	private IDataBaseProxy _dbProxy;
	public IDataBaseProxy LocalDB {
		get {
			return _dbProxy;
		}
	}

	private IWebDataProxy _webProxy;
	public IWebDataProxy WebDB{
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
		_dbProxy = new SQLiteProxy ();
		_dbProxy.Init ();
	}

	private bool NeedUpdate(string tableName)
	{
		if (!CheckConnection ()) {
			return false;
		}

		if (!_dbProxy.IsTableExist (tableName)) {
			return true;
		}
		else
		if (_webProxy.lastUpdateTime (tableName) > _dbProxy.lastUpdateTime (tableName)) {
			return true;
		}
		return false;
	}

	public void LoadData<TData>(string tableName, Action<string, Dictionary<string,TData>> callback) where TData:IBaseData, new()
	{
		if (NeedUpdate (tableName)) {
			_webProxy.GetTableData (tableName, OnUpdateComplete<TData>);
			callback = null;
		} else {
			_dbProxy.GetTableData <TData> (tableName, callback);
		}
	}

	void OnUpdateComplete<TData> (string tableName, Dictionary<string, IBaseData> dataDict)  where TData:IBaseData
	{
		_dbProxy.SaveTableData<TData> (tableName, dataDict);
		EventManager.Get<StorageUpdateCompleteEvent> ().Publish (typeof(TData));
	}

	public void SaveResults(string playerName, int score)
	{
		_webProxy.SaveScores(playerName,score);
	}

	public bool CheckConnection()
	{
		ConnectionTesterStatus connectionStatus = Network.TestConnection ();
		if (connectionStatus == ConnectionTesterStatus.Error){
			return false;
		}
		return true;
	}

	private DataProxy (){}
}


