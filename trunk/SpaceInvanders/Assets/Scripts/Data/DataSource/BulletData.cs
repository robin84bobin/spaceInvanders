using Assets.Scripts.Data.Attributes;
using Assets.Scripts.Data.DataSource.Impacts;

namespace Assets.Scripts.Data.DataSource
{
    public class BulletData : BaseData
    {
        private ImpactData _impactData;
        [DbField] public int damage; // { get; set;}
        public string impactId; // { get; set;}

        public string impactType; // { get; set; }

        public ImpactData Impact
        {
            get
            {
                return _impactData ??
                       (_impactData = Main.Inst.Data.Get<ImpactData>(impact_ => impact_.ImpactType == impactType));
            }
        }
    }
}