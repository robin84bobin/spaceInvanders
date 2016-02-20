using Assets.Scripts.ModelComponents.Actors;
using Assets.Scripts.ModelComponents.Impacts;

namespace Assets.Scripts.ModelComponents.Collisions
{
    public class CollisionInfo
    {
        private readonly IImpact[] _impacts;

        public CollisionInfo( IImpact[] impacts_)
        {
            _impacts = impacts_;
        }

        public void Apply(BaseActorModel actor_)
        {
            foreach (var impact in _impacts) {
                impact.Apply(actor_);
            }
        }
    }
}