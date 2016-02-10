using Assets.Scripts.Data.Attributes;

namespace Assets.Scripts.Data.DataSource.Impacts
{
    public class BonusItemImpactData : ImpactData
    {
        [DbField]
        public string ItemId { get; set; }
        [DbField]
        public bool ToInventory { get; set; }
    }
}