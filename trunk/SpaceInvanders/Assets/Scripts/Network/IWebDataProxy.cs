using System;
using System.Collections.Generic;
using Assets.Scripts.Controllers;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Data.User;

namespace Assets.Scripts.Network
{
    public interface IWebDataProxy
    {
        double LastUpdateTime (string tableName_);
        void GetTableData(string tableName_, Action< string, Dictionary<string, IBaseData>> callback_);
        void LogIn (string username_, string password_, Action onSuccess_, Action onFail_);
        void SignUp (AuthData authData_, Action onSuccess_, Action onFail_);

        UserData CurrentUser ();

        void SaveScores(string name_, int score_);
       // void GetTableData<TData>(string tableName_, Action<string, Dictionary<string, TData>> p) where TData : IBaseData, new();
    }
}

