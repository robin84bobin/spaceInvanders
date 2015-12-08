using System;

public interface IBaseData
{
	string type { get;}
	string objectId { get;}
}

public class BaseData : IBaseData
{
	public string type { get; internal set;}
	public string objectId { get; internal set;}
}



