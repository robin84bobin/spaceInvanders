using Assets.Scripts.Data.Attributes;

namespace Assets.Scripts.Data.DataSource.Impacts
{
    public class ImpactInfo  : BaseData
    {
        [DbField] public string impactType;
        // objectId of current impact type 
        [DbField] public string impactObjectId; 
   }
}