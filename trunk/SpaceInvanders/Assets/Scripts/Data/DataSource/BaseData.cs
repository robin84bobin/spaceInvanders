using System;

public interface IBaseData
{
	string Type { get; set;}
	string ObjectId { get; set;}
}

public class BaseData : IBaseData
{
	public string type;
	public string Type {
		get { return type;}
		set { type = value;}
	}


	public string objectId;
	public string ObjectId {
		get {
			return objectId;
		}
		set {
			objectId = value;
		}
	}
}



