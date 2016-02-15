using System;
using System.Collections.Generic;
using Assets.Scripts.Data.DataSource;

namespace Assets.Scripts.Data.DB
{
    public interface IDataBaseProxy
    {
        void Init();
        double LastUpdateTime (string tableName_);
        bool IsTableExist(string tableName_);
        void SaveTableData<TBaseData>(string tableName_, Dictionary<string, IBaseData> dataDictionary_) where TBaseData:IBaseData;
        void SaveTableData<T>(string tableName_, Dictionary<string, T> dataDictionary_);
        void GetTableData<TBaseData> (string tableName_, Action<string, Dictionary<string, TBaseData>> callback_) where TBaseData:IBaseData, new();
        
    }
}


