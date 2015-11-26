
using System;
using System.Collections.Generic;

public class DataLoader
{
	private IDataBaseProxy _dbProxy;
	public IDataBaseProxy DBProxy {
		get {
			return _dbProxy;
		}
	}

	private IWebDataProxy _webProxy;
	public IWebDataProxy WebProxy{
		get {
			return _webProxy;
		}
	}

	private static DataLoader _instance;
	public static DataLoader Instance {
		get {
			if (_instance == null){
				_instance = new DataLoader();
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
		if (!_dbProxy.IsTableExist (tableName)) {
			return true;
		}
		else
		if (_webProxy.lastUpdateTime (tableName) > _dbProxy.lastUpdateTime (tableName)) {
			return true;
		}
		return false;
	}

	public void LoadData<TData>(string tableName, Action<Dictionary<string,IBaseData>> callback) where TData:IBaseData, new()
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
		EventManager.Get<StorageUpdateCompleteEvent> ().Publish (tableName);
	}

	public void SaveResults(string playerName, int score)
	{
		_webProxy.SaveScores(playerName,score);
	}

	private DataLoader (){}
}


