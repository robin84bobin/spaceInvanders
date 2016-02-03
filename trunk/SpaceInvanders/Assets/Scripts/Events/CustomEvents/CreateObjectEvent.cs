using Assets.Scripts.Data.DataSource;
using Assets.Scripts.ModelComponents.Actors;
using UnityEngine;

namespace Assets.Scripts.Events.CustomEvents
{
    public sealed class CreateObjectEvent : SiParamEvent<CreateObjectParams>
    {
    }

    public class CreateObjectParams
    {
        public Vector3 rotation;
        public Vector3 position;
        public Vector3 scale;
        public BaseActorModel model;
    }
}