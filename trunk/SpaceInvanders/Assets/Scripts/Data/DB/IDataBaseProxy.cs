using System;
using System.Collections.Generic;

public interface IDataBaseProxy
{
	void Init();
	double lastUpdateTime (string tableName);
	void SaveDataTable<TBaseData>(string tableName, Dictionary<string, IBaseData> dataDictionary) where TBaseData:IBaseData;
	void GetTableData<TBaseData> (string tableName, Action<Dictionary<string, IBaseData>> callback, Action<string> failCallback) where TBaseData:IBaseData, new();
}


