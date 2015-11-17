
using System;
using System.Collections.Generic;

public class DataLoader
{
	private IDataBaseProxy _dbProxy;
	private IWebDataProxy _webProxy;

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

	public bool CheckToUpdate(string tableName)
	{
		if (_webProxy.lastUpdateTime (tableName) > _dbProxy.lastUpdateTime (tableName)) {

		}
		return true;
	}

	public void SaveDataToDB<TData> (string tableName, Dictionary<string,IBaseData> dataDict) where TData:IBaseData
	{
		_dbProxy.SaveDataTable<TData> (tableName, dataDict);
	}

	public void LoadDBData<TData>(string tableName, Action<Dictionary<string,IBaseData>> callback, Action<string> failCallback) where TData:IBaseData, new()
	{
		_dbProxy.GetTableData<TData>(tableName, callback, failCallback);
	}

	public void LoadWebData(string tableName, Action<Dictionary<string,IBaseData>> callback)
	{
		_webProxy.GetData(tableName, callback);
	}

	public void SaveResults(string playerName, int score)
	{
		_webProxy.SaveScores(playerName,score);
	}

	private DataLoader (){}
}


