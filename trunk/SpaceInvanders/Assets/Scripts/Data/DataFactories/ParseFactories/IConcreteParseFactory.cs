using System;
using Parse;
using Data;
namespace Data{
public interface IConcreteParseFactory
{
	IBaseData Create(ParseObject po) ;
}
}

