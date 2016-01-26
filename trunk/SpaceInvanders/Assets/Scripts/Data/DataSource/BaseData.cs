using System;
using Assets.Scripts.Data.Attributes;

namespace Assets.Scripts.Data.DataSource
{
    public interface IBaseData
    {
        string Type { get;}
        string ObjectId { get;}
    }

    public class BaseData : IBaseData
    {
        [DbField]
        public string Type { get; set;}
        [DbField]
        public string ObjectId { get; set;}
    }
}