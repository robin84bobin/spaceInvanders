using System;
using Parse;
using System.Threading.Tasks;
using System.Collections.Generic;

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


	public void SaveScores (string name, int score)
	{
		throw new NotImplementedException ();
	}

	#endregion
}

public class ParseProxyREST : IWebDataProxy
{
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

	#endregion


}

