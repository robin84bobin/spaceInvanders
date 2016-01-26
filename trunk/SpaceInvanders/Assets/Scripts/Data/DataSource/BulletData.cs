using Assets.Scripts.Data.Attributes;

namespace Assets.Scripts.Data.DataSource
{
    public class BulletData : BaseData
    {
        [DbField]
        public int Damage { get; set;}
    }
}



