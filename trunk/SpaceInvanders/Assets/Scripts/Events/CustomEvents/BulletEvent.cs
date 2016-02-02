using Assets.Scripts.Data.DataSource;
using UnityEngine;

namespace Assets.Scripts.Events.CustomEvents
{
    public sealed class BulletEvent : SiParamEvent<BulletParams>
    {
    }

    public struct BulletParams
    {
        public Vector3 position;
        public Vector3 direction;
        public BulletData data;
    }
}