using Assets.Scripts.Data.DataSource;
using UnityEngine;

namespace Assets.Scripts.Events.CustomEvents
{
    public sealed class CreateEntityEvent : xParamEvent<CreationParams>
    {
    }

    public struct CreationParams
    {
        public Vector3 position;
        public Vector3 direction;
        public IBaseData data;
    }
}