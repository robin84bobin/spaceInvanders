using System;
using Assets.Scripts.Data.Attributes;

namespace Assets.Scripts.Data.DataSource
{
    public interface IBaseData
    {
        string Type { get; set; }
        string ObjectId { get;}
    }

    public class BaseData : IBaseData
    {
        [DbField]
        public string type;// { get; set;}
        [DbField]
        public string objectId;// { get; set;}

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string ObjectId
        {
            get { return objectId; } 
        }
    }
}