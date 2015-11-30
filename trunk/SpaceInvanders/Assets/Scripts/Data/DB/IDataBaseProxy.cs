using System;
using System.Collections.Generic;

public interface IDataBaseProxy
{
	void Init();
	double lastUpdateTime (string tableName);
	bool IsTableExist(string tableName);
	void SaveTableData<TBaseData>(string tableName, Dictionary<string, IBaseData> dataDictionary) where TBaseData:IBaseData;
	void GetTableData<TBaseData> (string tableName, Action<string, Dictionary<string, IBaseData>> callback) where TBaseData:IBaseData, new();
}


