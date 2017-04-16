using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Assets.JSON;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Factories.DataFactories.JsonFactories;
using UnityEngine;

namespace Assets.Scripts.Data.DB
{
    public class JsonProxy : IDataBaseProxy
    {
        public const string FOLDER_PATH = "JSON";

        public void Init()
        {
            //init smthng...
        }

        public double LastUpdateTime(string tableName_)
        {
            //throw new NotImplementedException();
            return 0f;
        }

        public bool IsTableExist(string path_)
        {
            bool exists = File.Exists(path_);
            if (!exists) {
                Debug.LogError("FILE NOT FOUND: " + path_);
            }

            return exists;
        }

        public void SaveTableData<TBaseData>(string tableName_, Dictionary<string, IBaseData> dataDictionary_)
            where TBaseData : IBaseData
        {
            SaveTable(tableName_, dataDictionary_);
        }

        public void SaveTableData<T>(string tableName_, Dictionary<string, T> dataDictionary_)
        {
            SaveTable(tableName_, dataDictionary_);
        }

        private void SaveTable(string tableName_, IDictionary dataDictionary_)
        {
            var filePath = Application.streamingAssetsPath + "/" + FOLDER_PATH + "/" + tableName_ + ".json";
            if (!IsTableExist(filePath))
            {
                return;
            }

            StreamWriter writer = File.CreateText(filePath);
            var sourceString = CreateJsonFromDict(dataDictionary_);
            writer.Write(sourceString.ToCharArray());
            writer.Close();
        }

        private string CreateJsonFromDict(IDictionary dict_)
        {
            StringBuilder sb = new StringBuilder("{ \"collection\": [ \n");
            string dataStr = string.Empty;
            foreach (var dataItem in dict_.Values) {
                dataStr = JsonUtility.ToJson(dataItem);
                sb.Append(dataStr);
                sb.Append(",\n");
            }
            sb.Append("\n]}");

            return sb.ToString();
        }

        public void GetTableData<T>(string tableName_, Action<string, Dictionary<string, T>> callback_)
            where T : IBaseData, new()
        {
            var sourceString = string.Empty;
            var filePath = Application.streamingAssetsPath + "/" + FOLDER_PATH + "/" + tableName_ + ".json";
            if (!IsTableExist(filePath)){
                callback_.Invoke(tableName_, null);
                return;
            }

            sourceString = File.ReadAllText(filePath);

            var resultDict = new Dictionary<string, T>();
            var jsonRoot = new JSONObject(sourceString);
            for (int index = 0; index < jsonRoot["collection"].list.Count; index++) {
                var jsonObject = jsonRoot["collection"].list[index];
                var dataItem = JsonFactory.Instance.Create<T>(jsonObject.ToString());
                resultDict.Add(dataItem.ObjectId, dataItem);
            }

            callback_.Invoke(tableName_, resultDict);
        }

        
    }
}