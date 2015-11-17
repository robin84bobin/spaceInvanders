
using System;
using System.Collections.Generic;

public class DataLoader
{
	private SQLiteProxy _dbProxy;
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

	public void SaveDataToDB<TData> (string tableName, Dictionary<string,IBaseData> dataDict) where TData:IBaseData
	{
		_dbProxy.SaveDataTable<TData> (tableName, dataDict);
	}

	public void LoadDBData<TData>(string tableName, Action<Dictionary<string,IBaseData>> callback) where TData:IBaseData, new()
	{
		_dbProxy.GetTableData<TData>(tableName, callback);
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


