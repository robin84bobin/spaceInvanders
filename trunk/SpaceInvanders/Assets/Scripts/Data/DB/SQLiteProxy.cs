using System;
using System.Data;
using UnityEngine;
using System.IO;
using Mono.Data.SqliteClient;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

public class SQLiteProxy : IDataBaseProxy
{
	private const string SQL_TABLE_NAME = "TestTable";
	private const string SQL_DB_NAME = "SpaceInvandersDB";

	private static readonly string SQL_DB_LOCATION = 
			"URI=file:"
			+ Application.dataPath + Path.DirectorySeparatorChar
			+ "Plugins" + Path.DirectorySeparatorChar
			+ "SQLiter" + Path.DirectorySeparatorChar
			+ "Databases" + Path.DirectorySeparatorChar
			+ SQL_DB_NAME + ".db";
	
	public SQLiteProxy ()
	{
	}

	private bool mCreateNewTable = false;

	/// <summary>
	/// DB objects
	/// </summary>
	private IDbConnection mConnection = null;
	private IDbCommand mCommand = null;
	private IDataReader mReader = null;
	private string mSQLString;

	public void Init()
	{
		mCreateNewTable = false;

		Debug.Log("SQLiter - Opening SQLite Connection at " + SQL_DB_LOCATION);
		mConnection = new SqliteConnection(SQL_DB_LOCATION);
		mCommand = mConnection.CreateCommand();
		mConnection.Open();
		
		// WAL = write ahead logging, very huge speed increase
		mCommand.CommandText = "PRAGMA journal_mode = WAL;";
		mCommand.ExecuteNonQuery();
		
		// journal mode = look it up on google, I don't remember
		mCommand.CommandText = "PRAGMA journal_mode";
		mReader = mCommand.ExecuteReader();
		if (mReader.Read())
			Debug.Log("SQLiter - WAL value is: " + mReader.GetString(0));
		mReader.Close();
		
		// more speed increases
		mCommand.CommandText = "PRAGMA synchronous = OFF";
		mCommand.ExecuteNonQuery();
		
		// and some more
		mCommand.CommandText = "PRAGMA synchronous";
		mReader = mCommand.ExecuteReader();
		if (mReader.Read())
			Debug.Log("SQLiter - synchronous value is: " + mReader.GetInt32(0));
		mReader.Close();
		
	}

	public double lastUpdateTime (string tableName)
	{
		return 0;
	}
	
	#region READ DATABASE

	public bool IsTableExist(string tableName)
	{
		mCommand.CommandText = "SELECT name FROM sqlite_master WHERE name='" + tableName + "'";
		mReader = mCommand.ExecuteReader ();
		if (!mReader.Read ()) {
			mReader.Close ();
			return false;
		}
		mReader.Close ();
		return true;
	}

	public void GetTableData<TBaseData> (string tableName, Action<string, Dictionary<string, TBaseData>> callback) where TBaseData:IBaseData, new()
	{
		Dictionary<string,TBaseData> resultObjects = new Dictionary<string, TBaseData> ();

		mCommand.CommandText = "SELECT * FROM " + tableName;
		mReader = mCommand.ExecuteReader();

		while (mReader.Read()) {
			TBaseData data = ReadDataItem<TBaseData>(mReader);
			resultObjects.Add(data.objectId, data);
		}

		callback (tableName,resultObjects);
	}

	private TBaseData ReadDataItem<TBaseData> (IDataReader mReader) where TBaseData:IBaseData, new()
	{
		TBaseData resultData = new TBaseData ();
		Type dataType = resultData.GetType();

		PropertyInfo[] props = dataType.GetProperties ();

		string fieldName;
		object fieldValue;
		Type fieldType;
		FieldInfo targetField;
		MethodInfo targetSetter;
		PropertyInfo targetProperty;

		for (int fieldIndex = 0; fieldIndex < mReader.FieldCount; ++ fieldIndex) 
		{
			fieldName = mReader.GetName (fieldIndex);
			fieldValue = mReader.GetValue (fieldIndex); 
			fieldType = mReader.GetFieldType(fieldIndex);

			targetProperty = dataType.GetProperty(fieldName);
			if (targetProperty == null){
				continue;
			}

			targetSetter = targetProperty.GetSetMethod(true);
			if (targetSetter == null){
				continue;
			}

			if (targetProperty.PropertyType != fieldType){
				Debug.LogWarningFormat("Types mismatch (expected '{0}' => get '{1}') on reading data: {2}.{3}", 
				                       targetProperty.PropertyType.Name,
				                       fieldType,
				                       dataType.Name,
				                       targetProperty.Name);
			}
			else {
				targetSetter.Invoke( resultData, new object[] {fieldValue});
			}
			/*targetField = dataType.GetField (fieldName);
			if(targetField == null){
				continue;
			}

			if (targetField.FieldType != fieldType){
				Debug.LogWarningFormat("Types mismatch (expected '{0}' => get '{1}') on reading data: {2}.{3}", 
				                       targetField.FieldType,
				                       fieldType,
				                       dataType.Name,
				                       targetField.Name);
				targetField.SetValue( resultData, Convert.ChangeType( fieldValue, targetField.FieldType));
			}
			else{
				targetField.SetValue( resultData, fieldValue);
			}*/
		}

		return resultData;
	}

	#endregion

	#region WRITE DATABASE

	public void SaveTableData<TBaseData>(string tableName, Dictionary<string, IBaseData> dataDictionary) where TBaseData:IBaseData
	{
		CreateTable<TBaseData>(tableName);
		InsertData<TBaseData> (tableName, dataDictionary);
	}

	private void CreateTable<TBaseData>(string tableName) where TBaseData:IBaseData
	{
		Debug.Log (string.Format ("SQLiter - Dropping old SQLite table if Exists: {0}", tableName));
		
		// insurance policy, drop table
		mCommand.CommandText = "DROP TABLE IF EXISTS " + tableName;
		mCommand.ExecuteNonQuery();
		
		Debug.Log (string.Format ("SQLiter - Creating new SQLite table: {0}", tableName));

		string tableColumns = CreateTableColumnsString<TBaseData>();
		mSQLString = string.Format ("CREATE TABLE IF NOT EXISTS {0} ({1})", tableName, tableColumns);
		mCommand.CommandText = mSQLString;
		mCommand.ExecuteNonQuery();

		Debug.Log (mSQLString);
	}

	private void InsertData<TBaseData>(string tableName, Dictionary<string, IBaseData> dataDictionary) where TBaseData:IBaseData
	{
		string columnNames = string.Empty;
		string values = string.Empty;
		foreach (KeyValuePair<string, IBaseData> pair in dataDictionary) {
			columnNames = GetFieldsNames(pair.Value);
			values = GetDataValues(pair.Value);

			StringBuilder sb = new StringBuilder();
			sb.Append("INSERT OR REPLACE INTO ");
			sb.Append(tableName);
			sb.Append("("); 
			sb.Append(columnNames); 
			sb.Append(") VALUES (");
			sb.Append(values); sb.Append(");");
			mSQLString = sb.ToString();

			Debug.Log (mSQLString);
			mCommand.CommandText = mSQLString;
			mCommand.ExecuteNonQuery ();
		}

	}

	#endregion

	private string CreateTableColumnsString<TBaseData>() where TBaseData:IBaseData
	{
		FieldInfo[] fields = typeof(TBaseData).GetFields ();
		
		StringBuilder sb = new StringBuilder ();
		string fieldType = string.Empty;
		FieldInfo fieldInfo;
		for (int i = 0; i < fields.Length; i++) {
			fieldInfo = fields[i];
			fieldType = SQLiteHelper.GetColumnType( fieldInfo.FieldType.Name );
			if (i==0){
				sb.AppendFormat("{0} {1}", fieldInfo.Name, fieldType);
			}
			else{
				sb.AppendFormat(", {0} {1}", fieldInfo.Name, fieldType);
			}
		}

		sb.Append (", Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP");

		return sb.ToString ();
	}

	private string GetFieldsNames (IBaseData data)
	{
		FieldInfo[] fields = data.GetType().GetFields ();;
		StringBuilder sb = new StringBuilder ();
		for (int i = 0; i < fields.Length; i++) {
			if (i==0){
				sb.Append(fields[i].Name);
			}
			else{
				sb.Append (string.Format (",{0}", fields[i].Name));
			}
		}
		return sb.ToString();
	}

	private string GetDataValues (IBaseData data)
	{
		FieldInfo[] fields = data.GetType().GetFields ();
		string type;
		FieldInfo fieldInfo;
		object fieldValue;
		string valueString = string.Empty;
		StringBuilder sb = new StringBuilder ();
		for (int i = 0; i < fields.Length; i++) {
			fieldInfo = fields[i];
			fieldValue = fieldInfo.GetValue(data);
			type = SQLiteHelper.GetColumnType(fieldInfo.FieldType.Name);
			valueString = type == SQLiteHelper.TEXT ? string.Format("{0}{1}{2}", "'",fieldValue.ToString(),"'") : fieldValue.ToString();
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


public class SQLiteHelper
{
	public const string TEXT = "TEXT";
	public const string TEXT_UNIQUE = "TEXT_UNIQUE";
	public const string REAL = "REAL";
	public const string INTEGER = "INTEGER";

	public static string GetColumnType (string name)
	{
		if (_typeMap.ContainsKey (name)) {
			return _typeMap [name];
		} else {
			Debug.LogException(new Exception(string.Format ("SQLite type mismatch: {0} - not declared in SQLiteHelper", name)));
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

