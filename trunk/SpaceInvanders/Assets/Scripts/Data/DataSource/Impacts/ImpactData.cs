using Assets.Scripts.Data.Attributes;

namespace Assets.Scripts.Data.DataSource.Impacts
{
    public class ImpactData  : BaseData
    {
        [DbField]
        public string ImpactType { get; set; }
        [DbField]
        public double Period { get; set; }
        [DbField]
        public double TimeActive { get; set; }
        [DbField]
        public double Delayed { get; set; }
        [DbField]
        private bool IgnoreTimer { get; set; }

        public bool ApplyImmediately
        {
            get
            {
                return IgnoreTimer || (Delayed <= 0 && TimeActive >= 0 && Period >= 0);
            }
        }

    }
}