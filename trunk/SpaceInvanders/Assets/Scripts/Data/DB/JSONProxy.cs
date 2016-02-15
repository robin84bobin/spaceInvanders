using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Assets.Scripts.Data.DataSource;
using UnityEditor;
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
            var filePath = Application.streamingAssetsPath + "/" + FOLDER_PATH + "/" + tableName_ + ".txt";
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

        public void GetTableData<TBaseData>(string tableName_, Action<string, Dictionary<string, TBaseData>> callback_)
            where TBaseData : IBaseData, new()
        {
            var sourceString = string.Empty;
            var filePath = Application.streamingAssetsPath + "/" + FOLDER_PATH + "/" + tableName_ + ".txt";
            if (!IsTableExist(filePath)){
                callback_.Invoke(tableName_, null);
                return;
            }

           // var www = new WWW(filePath);
            sourceString = File.ReadAllText(filePath);

            var resultDict = new Dictionary<string, TBaseData>();
            var jsonRoot = new JSONObject(sourceString);
            foreach (var jsonObject in jsonRoot["collection"].list) {
                var dataItem = new TBaseData {Type = tableName_};
                JsonUtility.FromJsonOverwrite(jsonObject.ToString(), dataItem);
                resultDict.Add(dataItem.ObjectId, dataItem);
            }

            callback_.Invoke(tableName_, resultDict);
        }

        /*
        private T CreateDataItem<T>(JSONObject jsonObject_) where T:IBaseData , new ()
        {
            T  data = new T();
            List<PropertyInfo> propertyInfos = new List<PropertyInfo>(
                typeof(T).GetProperties().Where(prop_ => Attribute.IsDefined(prop_, typeof(DbFieldAttribute)))
                );

            foreach (PropertyInfo propertyInfo in propertyInfos) {
                MethodInfo setter = propertyInfo.GetSetMethod(true);
                if (setter == null) {
                    Debug.LogError( string.Format("JSONProxy:CreateDataItem: {0} has no set method {1}", typeof(T).Name, propertyInfo.Name ));
                    continue;
                }

                object val = jsonObject_[propertyInfo.Name];
                setter.Invoke(data, new object[] {
                    val
                });
            }

            return data;

            // string str = jsonObject_.ToString();
            // return JsonUtility.FromJson<T>(str);
        }
        */

        /*
        private List<T> ExtractDataCollection<T>(string sourceStr_) where T:IBaseData , new()
        {
            JSONObject jsonRoot = new JSONObject(sourceStr_);
            
            JSONObject collection = new JSONObject(JSONObject.Type.ARRAY);
            collection = jsonRoot["collection"];

            List<T> resultArray = new List<T>();
            foreach (JSONObject jsonObject in collection.list) {
                T dataItem = new T();
                JsonUtility.FromJsonOverwrite(jsonObject.ToString(), dataItem);
                resultArray.Add(dataItem);
            }
           
            return resultArray;
        }

        */
    }
}