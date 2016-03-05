using System.Linq;
using Assets.Scripts.ModelComponents.Actors;

namespace Assets.Scripts.ModelComponents.Impacts
{
    public abstract class Impact
    {
        public void Apply(BaseEntityModel entity_)
        {
            if (CheckTargetTypes(entity_)) {
                Execute(entity_);
            }
        }
        protected abstract void Execute(BaseEntityModel entity_);

        protected string[] targetTypes;

        public bool CheckTargetTypes(BaseEntityModel entity_)
        {
            if (targetTypes == null || targetTypes.Length == 0) {
                return true;
            }
            return targetTypes.Contains(entity_.DataType);
        }
    }
}