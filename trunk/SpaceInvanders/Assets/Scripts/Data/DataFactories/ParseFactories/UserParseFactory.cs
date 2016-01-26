using Assets.Scripts.Data.DataSource;
using Parse;

namespace Assets.Scripts.Data.DataFactories.ParseFactories
{
    public class UserParseFactory : IConcreteParseFactory
    {
        #region IConcreteParseFactory implementation

        public IBaseData Create (ParseObject po_)
        {
            UserData.UserData userData = new UserData.UserData ();
            ParseUser parseUser = (ParseUser)po_;
            userData.username = parseUser.Username;
            userData.email = parseUser.Email;

            return userData;
        }

        #endregion
    }
}



