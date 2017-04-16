using Assets.Scripts.ModelComponents.Entities;
using Assets.Scripts.ModelComponents.Impacts;

namespace Assets.Scripts.ModelComponents.Collisions
{
    public class CollisionInfo
    {
        private readonly Impact[] _impacts;

        public CollisionInfo( Impact[] impacts_)
        {
            _impacts = impacts_;
        }

        public void Apply(BaseEntityModel entity_)
        {
            foreach (var impact in _impacts) {
                 impact.Apply(entity_);
            }
        }
    }
}