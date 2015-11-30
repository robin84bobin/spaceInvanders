using System;
using Parse;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class ParseProxy: IWebDataProxy
{
	#region IDataProxy implementation

	public double lastUpdateTime (string tableName)
	{
		return 0;
	}

	public void GetTableData (string dataType, Action<string,Dictionary<string, IBaseData>> callback)	
	{	
		Dictionary<string, IBaseData> resultDict = new Dictionary<string, IBaseData>();
		var query = ParseObject.GetQuery (dataType);
		query.FindAsync().ContinueWith( t => {
			IEnumerable<ParseObject> result = t.Result;
			foreach(ParseObject po in result){
				IBaseData dataItem = ParseFactory.Instance.Create(dataType,po);
				resultDict.Add (po.ObjectId, dataItem);
			}
			callback( dataType, resultDict );
		});
	}

	public void LogIn (string username, string password, Action onSuccess, Action onFail)
	{
		ParseUser.LogInAsync (username, password).ContinueWith ((Task task) => 
		{
			if (task.IsFaulted || task.IsCanceled){
				onFail();
			}
			else{
				onSuccess();
			}
		});
	}

	public void SaveScores (string name, int score)
	{
		throw new NotImplementedException ();
	}

	public void SignUp (AuthData _authData, Action OnSuccess, Action OnFail)
	{
		ParseUser user = new ParseUser ();
		user.Add("Username", _authData.username);
		user.Add("Email", _authData.email);
		user.Add("Password", _authData.password);
		user.SignUpAsync ().ContinueWith((Task task) => {
			if (task.IsCanceled || task.IsFaulted){
				OnFail();
			}
			else{
				OnSuccess();
			}
		});
	}


	public UserData CurrentUser ()
	{
		if (ParseUser.CurrentUser == null) {
			return null;
		}
		return (UserData)ParseFactory.Instance.Create (DataTypes.USER, ParseUser.CurrentUser);
	}

	#endregion
}

public class ParseProxyREST : IWebDataProxy
{
	public const string url = "https://api.parse.com/1/";

	#region IWebDataProxy implementation

	public double lastUpdateTime (string tableName)
	{
		return 0;
	}

	public void GetTableData (string tableName, Action< string, Dictionary<string, IBaseData> > callback)
	{
		throw new NotImplementedException ();
	}

	public void SaveScores (string name, int score)
	{
		throw new NotImplementedException ();
	}

	public void SignUp (AuthData _authData)
	{
		WWWForm form = new WWWForm();
		form.AddField("Username", _authData.username);
		form.AddField("Email", _authData.email);
		form.AddField("Password",_authData.password);
		WWW connect = new WWW (url + "users", form);
	}

	public void LogIn (string username, string password, Action onSuccess, Action onFail)
	{
		throw new NotImplementedException ();
	}

	public void SignUp (AuthData _authData, Action onSuccess, Action onFail)
	{
		throw new NotImplementedException ();
	}

	public UserData CurrentUser ()
	{
		throw new NotImplementedException ();
	}

	#endregion

}

