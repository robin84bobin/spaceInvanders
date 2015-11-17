using System;
using Parse;
using System.Threading.Tasks;
using System.Collections.Generic;


public interface IWebDataProxy
{
	void GetData(string tableName, Action<Dictionary<string, IBaseData>> callback);
	void SaveScores(string name, int score);
}

