using System;

public interface IBaseData
{
	string type { get;}
	string objectId { get;}
}

public class BaseData : IBaseData
{
	public string type { get; set;}
	public string objectId { get; set;}
}



