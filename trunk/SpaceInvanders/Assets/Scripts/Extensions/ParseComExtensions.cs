using System;
using Parse;

public static class ParseComExtensions
{
	public static T TryGet<T> (this ParseObject po, string name)
	{
		if (po.ContainsKey (name)) {
			return po.Get<T> (name);
		} else {
			UnityEngine.Debug.LogError( string.Format ("Unable to get field '{0}' in ParseObject '{1}'", name, po.ClassName));
			return default(T);
		}
	}
	 
	public static string TryGetPointerObjectId(this ParseObject po, string name)
	{
		ParseObject obj = new ParseObject (name);
		po.TryGetValue<ParseObject> (name, out obj);
		string result = obj.ObjectId;
		if (string.IsNullOrEmpty (result)) {
			UnityEngine.Debug.LogError( string.Format ("Unable to get ObjectID '{0}' in ParseObject '{1}'", name, po.ClassName));
		} 
		return result;
	}
}


