using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Assets.Scripts.Data.Attributes;
using Assets.Scripts.Data.DataSource;
using UnityEngine;

namespace Assets.Scripts.Data.DB
{
    public class JsonProxy  : IDataBaseProxy
    {
        public  const string PATH = "JSON/";

        public void Init()
        {
            //init smthng...
        }

        public double LastUpdateTime(string tableName_)
        {
            //throw new NotImplementedException();
            return 0f;
        }

        public bool IsTableExist(string tableName_)
        {
           TextAsset textAsset = Resources.Load(PATH + tableName_ ) as TextAsset;
            if (textAsset == null) {
                Debug.LogError("JSON FILE NOT FOUND: " + tableName_);
                return false;
            }
            
            return true;
        }

        public void SaveTableData<TBaseData>(string tableName_, Dictionary<string, IBaseData> dataDictionary_) where TBaseData : IBaseData
        {
            throw new NotImplementedException();
        }



        public void GetTableData<TBaseData>(string tableName_, Action<string, Dictionary<string, TBaseData>> callback_) where TBaseData : IBaseData, new()
        {
            if (!IsTableExist(tableName_)) {
                return;
            }

            TextAsset textAsset = Resources.Load(PATH + tableName_) as TextAsset;
            if (textAsset == null){
                Debug.LogError("JSON FILE NOT FOUND: " + tableName_);
                return;
            }

            //List<TBaseData> resultArray = new List<T>();
            Dictionary<string, TBaseData> resultDict = new Dictionary<string, TBaseData>();
            JSONObject jsonRoot = new JSONObject(textAsset.text);
            foreach (JSONObject jsonObject in jsonRoot["collection"].list)
            {
                TBaseData dataItem = new TBaseData {type = tableName_};
                JsonUtility.FromJsonOverwrite(jsonObject.ToString(), dataItem);
                resultDict.Add(dataItem.objectId, dataItem);
            }
            //List<TBaseData> dataList = ExtractDataCollection<TBaseData>(textAsset.text);

            //Dictionary<string, TBaseData> resultDict = dataList.ToDictionary(data_ => data_.objectId);
            callback_.Invoke( tableName_, resultDict );
        }

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

    }
}
