using System;
using Parse;
using Data;
namespace Data{
public class HeroParseFactory : IConcreteParseFactory
{
	#region IConcreteParseFactory implementation

	public IBaseData Create (ParseObject po)
	{
		HeroData heroData = new HeroData();
		heroData.type = po.ClassName;
		heroData.objectId = po.ObjectId;
		heroData.weaponId = po.TryGetPointerObjectId (DataTypes.WEAPON);

		return heroData;
	}

	#endregion
}


}