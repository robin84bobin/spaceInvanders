using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.Scripts.Controllers;
using Assets.Scripts.Data;
using Assets.Scripts.Data.DataFactories.ParseFactories;
using Assets.Scripts.Data.DataSource;
using Assets.Scripts.Data.UserData;
using Parse;
using UnityEngine;

namespace Assets.Scripts.Network
{
    public class ParseProxy: IWebDataProxy
    {
        #region IDataProxy implementation

        public double LastUpdateTime (string tableName_)
        {
            return 0;
        }

        public void GetTableData (string tableName_, Action<string,Dictionary<string, IBaseData>> callback_)	
        {	
            Dictionary<string, IBaseData> resultDict = new Dictionary<string, IBaseData>();
            var query = ParseObject.GetQuery (tableName_);
            query.FindAsync().ContinueWith( t_ => {
                                                     IEnumerable<ParseObject> result = t_.Result;
                                                     foreach(ParseObject po in result){
                                                         IBaseData dataItem = ParseFactory.Instance.Create(tableName_,po);
                                                         resultDict.Add (po.ObjectId, dataItem);
                                                     }
                                                     callback_( tableName_, resultDict );
            });
        }

        public void LogIn (string username_, string password_, Action onSuccess_, Action onFail_)
        {
            ParseUser.LogInAsync (username_, password_).ContinueWith ((Task task_) => 
            {
                if (task_.IsFaulted || task_.IsCanceled){
                    onFail_();
                }
                else{
                    onSuccess_();
                }
            });
        }

        public void SaveScores (string name_, int score_)
        {
            throw new NotImplementedException ();
        }

        public void SignUp (AuthData authData_, Action onSuccess_, Action onFail_)
        {
            ParseUser user = new ParseUser ();
            user.Add("Username", authData_.username);
            user.Add("Email", authData_.email);
            user.Add("Password", authData_.password);
            user.SignUpAsync ().ContinueWith((Task task_) => {
                                                                if (task_.IsCanceled || task_.IsFaulted){
                                                                    onFail_();
                                                                }
                                                                else{
                                                                    onSuccess_();
                                                                }
            });
        }


        public UserData CurrentUser ()
        {
            if (ParseUser.CurrentUser == null) {
                return null;
            }
            return (UserData)ParseFactory.Instance.Create (DataTypes.USER, ParseUser.CurrentUser);
        }

        #endregion
    }

    public class ParseProxyRest : IWebDataProxy
    {
        public const string URL = "https://api.parse.com/1/";

        #region IWebDataProxy implementation

        public double LastUpdateTime (string tableName_)
        {
            return 0;
        }

        public void GetTableData (string tableName_, Action< string, Dictionary<string, IBaseData> > callback_)
        {
            throw new NotImplementedException ();
        }

        public void SaveScores (string name_, int score_)
        {
            throw new NotImplementedException ();
        }

        public void SignUp (AuthData authData_)
        {
            WWWForm form = new WWWForm();
            form.AddField("Username", authData_.username);
            form.AddField("Email", authData_.email);
            form.AddField("Password",authData_.password);
            WWW connect = new WWW (URL + "users", form);
        }

        public void LogIn (string username_, string password_, Action onSuccess_, Action onFail_)
        {
            throw new NotImplementedException ();
        }

        public void SignUp (AuthData authData_, Action onSuccess_, Action onFail_)
        {
            throw new NotImplementedException ();
        }

        public UserData CurrentUser ()
        {
            throw new NotImplementedException ();
        }

        #endregion

    }
}