using Assets.Scripts.Data.Attributes;

namespace Assets.Scripts.Data.DataSource
{
    public class GameData : BaseData
    {
        [DbField] public int height;// { get; set;}
        [DbField] public int width;//  { get; set;}
    }
}

