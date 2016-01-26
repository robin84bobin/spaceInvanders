using Assets.Scripts.Data.Attributes;

namespace Assets.Scripts.Data.DataSource
{
    public class GameData : BaseData
    {
        [DbField]
        public int Height { get; set;}
        [DbField]
        public int Width  { get; set;}
    }
}

