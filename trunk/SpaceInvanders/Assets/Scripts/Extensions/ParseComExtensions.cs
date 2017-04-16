using Parse;

namespace Assets.Scripts.Extensions
{
    public static class ParseComExtensions
    {
        public static T TryGet<T> (this ParseObject po_, string name_)
        {
            if (po_.ContainsKey (name_)) {
                return po_.Get<T> (name_);
            } else {
                UnityEngine.Debug.LogError( string.Format ("Unable to get field '{0}' in ParseObject '{1}'", name_, po_.ClassName));
                return default(T);
            }
        }
	 
        public static string TryGetPointerObjectId(this ParseObject po_, string name_)
        {
            ParseObject obj = new ParseObject (name_);
            po_.TryGetValue<ParseObject> (name_, out obj);
            string result = obj.ObjectId;
            if (string.IsNullOrEmpty (result)) {
                UnityEngine.Debug.LogError( string.Format ("Unable to get ObjectID '{0}' in ParseObject '{1}'", name_, po_.ClassName));
            } 
            return result;
        }
    }
}


