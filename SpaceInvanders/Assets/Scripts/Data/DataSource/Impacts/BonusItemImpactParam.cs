using Assets.Scripts.Data.Attributes;

namespace Assets.Scripts.Data.DataSource.Impacts
{
    public class BonusItemImpactData : BaseData
    {
        public string[] itemsIds;
        public bool toInventory;
        public string[] targetTypes;
    }
}