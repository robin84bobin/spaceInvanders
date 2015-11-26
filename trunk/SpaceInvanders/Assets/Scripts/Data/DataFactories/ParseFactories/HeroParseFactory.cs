using System;
using Parse;

public class HeroParseFactory : IConcreteParseFactory
{
	#region IConcreteParseFactory implementation

	public IBaseData Create (ParseObject po)
	{
		HeroData heroData = new HeroData();
		heroData.Type = po.ClassName;
		heroData.ObjectId = po.ObjectId;
		heroData.weaponId = po.TryGetPointerObjectId (DataTypes.WEAPON);

		return heroData;
	}

	#endregion
}


