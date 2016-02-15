using System.Collections.Generic;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Data.User;
using Assets.Scripts.Network;

namespace Assets.Scripts.Data.DataStorages
{
    public class UserStorage : BaseStorage<UserData>
    {
        public UserStorage () : base(DataTypes.USER, false)
        {
        }

        public void SaveUserData(int id_)
        {
            DataProxy.Instance.SaveData<UserData>(dataType, id_);
        }


    }
}


