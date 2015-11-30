using System;
using Parse;

public class UserParseFactory : IConcreteParseFactory
{
	#region IConcreteParseFactory implementation

	public IBaseData Create (ParseObject po)
	{
		UserData userData = new UserData ();
		ParseUser parseUser = (ParseUser)po;
		userData.username = parseUser.Username;
		userData.email = parseUser.Email;

		return userData;
	}

	#endregion
}


