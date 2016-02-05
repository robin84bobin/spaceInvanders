namespace Assets.Scripts.ModelComponents.Collisions
{
    public class CollisionInfo
    {
        public ImpactInfo[] Impact { get; private set; }

        public CollisionInfo(params ImpactInfo[] impact_)
        {
            Impact = impact_;
        }
    }
}
