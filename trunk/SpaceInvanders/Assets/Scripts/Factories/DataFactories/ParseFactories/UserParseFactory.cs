using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Data.UserData;
using Parse;

namespace Assets.Scripts.Factories.DataFactories.ParseFactories
{
    public class UserParseFactory : IConcreteParseFactory
    {
        #region IConcreteParseFactory implementation

        public IBaseData Create(ParseObject po_)
        {
            var userData = new UserData();
            var parseUser = (ParseUser) po_;
            userData.username = parseUser.Username;
            userData.email = parseUser.Email;

            return userData;
        }

        #endregion
    }
}