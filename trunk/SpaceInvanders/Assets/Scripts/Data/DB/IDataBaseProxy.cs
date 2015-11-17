using System;
using System.Collections.Generic;

public interface IDataBaseProxy
{
	void Init();
	void SaveDataTable<TBaseData>(string tableName, Dictionary<string, IBaseData> dataDictionary) where TBaseData:IBaseData;
	void GetTableData<TBaseData> (string tableName, Action<Dictionary<string, IBaseData>> callback) where TBaseData:IBaseData, new();
}


