using System;
using Parse;

public interface IConcreteParseFactory
{
	IBaseData Create(ParseObject po) ;
}


