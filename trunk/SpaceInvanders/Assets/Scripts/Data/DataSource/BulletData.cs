using System;
using System.Security.Permissions;
using Assets.Scripts.Data.Attributes;
using Assets.Scripts.Data.DataSource.Impacts;

namespace Assets.Scripts.Data.DataSource
{
    public class BulletData : BaseData
    {
        [DbField] public int Damage;// { get; set;}

        public string ImpactType;// { get; set; }
        public string ImpactId;// { get; set;}

        private ImpactData _impactData;
        public ImpactData Impact
        {
            get {
                return _impactData ??
                       (_impactData = Main.Inst.Data.Get<ImpactData>(impact_ => impact_.ImpactType == ImpactType));
            }
        }
    }
}



