using System;
using Parse;
using System.Threading.Tasks;
using System.Collections.Generic;
using Data;


public interface IWebDataProxy
{
	double lastUpdateTime (string tableName);
	void GetTableData(string tableName, Action< string, Dictionary<string, IBaseData> > callback);
	void LogIn (string username, string password, Action onSuccess, Action onFail);
	void SignUp (AuthData _authData, Action onSuccess, Action onFail);

	UserData CurrentUser ();

	void SaveScores(string name, int score);
}

