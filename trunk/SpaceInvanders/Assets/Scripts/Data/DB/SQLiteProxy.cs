using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Assets.Scripts.Data.Attributes;
using Assets.Scripts.Data.DataSource;
using Mono.Data.SqliteClient;
using UnityEngine;

namespace Assets.Scripts.Data.DB
{
    public class SqLiteProxy : IDataBaseProxy
    {
        private const string SqlTableName = "TestTable";
        private const string SqlDbName = "SpaceInvandersDB";

        private static readonly string SqlDbLocation = 
            "URI=file:"
            + Application.dataPath + Path.DirectorySeparatorChar
            + "Plugins" + Path.DirectorySeparatorChar
            + "SQLiter" + Path.DirectorySeparatorChar
            + "Databases" + Path.DirectorySeparatorChar
            + SqlDbName + ".db";
	
        public SqLiteProxy ()
        {
        }

        private bool _mCreateNewTable = false;

        /// <summary>
        /// DB objects
        /// </summary>
        private IDbConnection _mConnection = null;
        private IDbCommand _mCommand = null;
        private IDataReader _mReader = null;
        private string _mSqlString;

        public void Init()
        {
            _mCreateNewTable = false;

            Debug.Log("SQLiter - Opening SQLite Connection at " + SqlDbLocation);
            _mConnection = new SqliteConnection(SqlDbLocation);
            _mCommand = _mConnection.CreateCommand();
            _mConnection.Open();
		
            // WAL = write ahead logging, very huge speed increase
            _mCommand.CommandText = "PRAGMA journal_mode = WAL;";
            _mCommand.ExecuteNonQuery();
		
            // journal mode = look it up on google, I don't remember
            _mCommand.CommandText = "PRAGMA journal_mode";
            _mReader = _mCommand.ExecuteReader();
            if (_mReader.Read())
                Debug.Log("SQLiter - WAL value is: " + _mReader.GetString(0));
            _mReader.Close();
		
            // more speed increases
            _mCommand.CommandText = "PRAGMA synchronous = OFF";
            _mCommand.ExecuteNonQuery();
		
            // and some more
            _mCommand.CommandText = "PRAGMA synchronous";
            _mReader = _mCommand.ExecuteReader();
            if (_mReader.Read())
                Debug.Log("SQLiter - synchronous value is: " + _mReader.GetInt32(0));
            _mReader.Close();
		
        }

        public double LastUpdateTime (string tableName_)
        {
            return 0;
        }
	
        #region READ DATABASE

        public bool IsTableExist(string tableName_)
        {
            _mCommand.CommandText = "SELECT name FROM sqlite_master WHERE name='" + tableName_ + "'";
            _mReader = _mCommand.ExecuteReader ();
            if (!_mReader.Read ()) {
                _mReader.Close ();
                return false;
            }
            _mReader.Close ();
            return true;
        }

        public void GetTableData<TBaseData> (string tableName_, Action<string, Dictionary<string, TBaseData>> callback_) where TBaseData:IBaseData, new()
        {
            Dictionary<string,TBaseData> resultObjects = new Dictionary<string, TBaseData> ();

            _mCommand.CommandText = "SELECT * FROM " + tableName_;
            _mReader = _mCommand.ExecuteReader();

            while (_mReader.Read()) {
                TBaseData data = ReadDataItem<TBaseData>(_mReader);
                resultObjects.Add(data.objectId, data);
            }

            callback_ (tableName_,resultObjects);
        }


        string _fieldName;
        object _fieldValue;
        Type _fieldType;
        MethodInfo _targetSetter;
        PropertyInfo _targetProperty;

        private TBaseData ReadDataItem<TBaseData> (IDataReader mReader_) where TBaseData:IBaseData, new()
        {
            TBaseData resultData = new TBaseData ();
            Type dataType = resultData.GetType();

            for (int fieldIndex = 0; fieldIndex < mReader_.FieldCount; ++ fieldIndex) 
            {
                _fieldName = mReader_.GetName (fieldIndex);
                _fieldValue = mReader_.GetValue (fieldIndex); 
                _fieldType = mReader_.GetFieldType(fieldIndex);

                _targetProperty = dataType.GetProperty(_fieldName);
                if (_targetProperty == null){
                    continue;
                }

                _targetSetter = _targetProperty.GetSetMethod();
                if (_targetSetter == null) {
                    continue;
                }

                if (_targetProperty.PropertyType != _fieldType){
                    Debug.LogWarningFormat("Types mismatch (expected '{0}' => get '{1}') on reading data: {2}.{3}", 
                        _targetProperty.PropertyType.Name,
                        _fieldType,
                        dataType.Name,
                        _targetProperty.Name);
                }
                else {
                    _targetSetter.Invoke( resultData, new object[] {_fieldValue});
                }
            }

            return resultData;
        }

        #endregion

        #region WRITE DATABASE

        public void SaveTableData<TBaseData>(string tableName_, Dictionary<string, IBaseData> dataDictionary_) where TBaseData:IBaseData
        {
            CreateTable<TBaseData>(tableName_);
            InsertData<TBaseData> (tableName_, dataDictionary_);
        }

        private void CreateTable<TBaseData>(string tableName_) where TBaseData:IBaseData
        {
            Debug.Log (string.Format ("SQLiter - Dropping old SQLite table if Exists: {0}", tableName_));
		
            // insurance policy, drop table
            _mCommand.CommandText = "DROP TABLE IF EXISTS " + tableName_;
            _mCommand.ExecuteNonQuery();
		
            Debug.Log (string.Format ("SQLiter - Creating new SQLite table: {0}", tableName_));

            string tableColumns = CreateTableColumnsString<TBaseData>();
            _mSqlString = string.Format ("CREATE TABLE IF NOT EXISTS {0} ({1})", tableName_, tableColumns);
            _mCommand.CommandText = _mSqlString;
            _mCommand.ExecuteNonQuery();

            Debug.Log (_mSqlString);
        }

        private void InsertData<TBaseData>(string tableName_, Dictionary<string, IBaseData> dataDictionary_) where TBaseData:IBaseData
        {
            string columnNames = string.Empty;
            string values = string.Empty;
            foreach (KeyValuePair<string, IBaseData> pair in dataDictionary_) {
                columnNames = GetFieldsNames(pair.Value);
                values = GetDataValues(pair.Value);

                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT OR REPLACE INTO ");
                sb.Append(tableName_);
                sb.Append("("); 
                sb.Append(columnNames); 
                sb.Append(") VALUES (");
                sb.Append(values); sb.Append(");");
                _mSqlString = sb.ToString();

                Debug.Log (_mSqlString);
                _mCommand.CommandText = _mSqlString;
                _mCommand.ExecuteNonQuery ();
            }

        }

        #endregion

        private string CreateTableColumnsString<TBaseData>() where TBaseData:IBaseData
        {
            List<PropertyInfo> propertyInfos = new List<PropertyInfo>(
                typeof(TBaseData).GetProperties().Where(prop_ => Attribute.IsDefined(prop_, typeof(DbFieldAttribute)))
                );

            StringBuilder sb = new StringBuilder ();
            string fieldType = string.Empty;

            for (int i = 0; i < propertyInfos.Count; i++) {

                fieldType = SqLiteHelper.GetColumnType( propertyInfos[i].PropertyType.Name );
                if (i==0){
                    sb.AppendFormat("{0} {1}", propertyInfos[i].Name, fieldType);
                }
                else{
                    sb.AppendFormat(", {0} {1}", propertyInfos[i].Name, fieldType);
                }
            }

            sb.Append (", Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP");

            return sb.ToString ();
        }

        private string GetFieldsNames (IBaseData data_)
        {
             List<PropertyInfo> propertyInfos = new List<PropertyInfo>(
                 data_.GetType().GetProperties().Where(prop_ => Attribute.IsDefined(prop_, typeof(DbFieldAttribute)))
                 );

            StringBuilder sb = new StringBuilder ();
            for (int i = 0; i < propertyInfos.Count; i++) {
                if (propertyInfos[i].GetSetMethod(true) == null) {
                    continue;
                }
                if (i==0){
                    sb.Append(propertyInfos[i].Name);
                }
                else{
                    sb.Append (string.Format (",{0}", propertyInfos[i].Name));
                }
            }
            return sb.ToString();
        }

        private string GetDataValues (IBaseData data_)
        {
            PropertyInfo[] propertyInfos = data_.GetType().GetProperties();
            string type;
            PropertyInfo propertyInfo;
            object fieldValue;
            string valueString = string.Empty;
            StringBuilder sb = new StringBuilder ();
            for (int i = 0; i < propertyInfos.Length; i++) {
                propertyInfo = propertyInfos[i];
                if (propertyInfo.GetSetMethod(true) == null) {
                    continue;
                }
                fieldValue = propertyInfo.GetValue(data_, null);
                type = SqLiteHelper.GetColumnType(propertyInfo.PropertyType.Name);
                valueString = type == SqLiteHelper.TEXT ? string.Format("{0}{1}{2}", "'",fieldValue.ToString(),"'") : fieldValue.ToString();
                if (i==0){
                    sb.Append( valueString );
                }
                else{
                    sb.Append (string.Format (",{0}", valueString));
                }
			
            }

            return sb.ToString();
        }
    }


    public class SqLiteHelper
    {
        public const string TEXT = "TEXT";
        public const string TEXT_UNIQUE = "TEXT_UNIQUE";
        public const string REAL = "REAL";
        public const string INTEGER = "INTEGER";

        public static string GetColumnType (string name_)
        {
            if (_typeMap.ContainsKey (name_)) {
                return _typeMap [name_];
            } else {
                Debug.LogException(new Exception(string.Format ("SQLite type mismatch: {0} - not declared in SQLiteHelper", name_)));
                return null;
            }
        }

        private static Dictionary<string,string> _typeMap = new Dictionary<string, string> ()
        {
            {"Int64", INTEGER},
            {"Int32", INTEGER},
            {"float", REAL},
            {"Double", REAL},
            {"Single", REAL},
            {"String", TEXT}
        };
    }
}