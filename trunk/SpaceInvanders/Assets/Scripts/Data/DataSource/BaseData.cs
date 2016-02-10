using System;
using Assets.Scripts.Data.Attributes;

namespace Assets.Scripts.Data.DataSource
{
    public interface IBaseData
    {
        string type { get; set; }
        string objectId { get;}
    }

    public class BaseData : IBaseData
    {
        [DbField]
        public string Type;// { get; set;}
        [DbField]
        public string ObjectId;// { get; set;}

        public string type
        {
            get { return Type; }
            set { Type = value; }
        }

        public string objectId
        {
            get { return ObjectId; } 
        }
    }
}