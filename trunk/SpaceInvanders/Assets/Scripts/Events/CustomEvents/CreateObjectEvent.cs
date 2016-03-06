using Assets.Scripts.Data.DataSource;
using UnityEngine;

namespace Assets.Scripts.Events.CustomEvents
{
    public sealed class CreateObjectEvent : SiParamEvent<CreateParams>
    {
    }

    public class CreateParams
    {
        public Vector3 rotation = Vector3.zero;
        public Vector3 position = Vector3.zero;
        public Vector3 scale = Vector3.one;
        //public BaseEntityModel model;
        public IBaseData data;
    }
}