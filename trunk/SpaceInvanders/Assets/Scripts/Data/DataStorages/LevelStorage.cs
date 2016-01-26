using Assets.Scripts.Data.DataSource;

namespace Assets.Scripts.Data.DataStorages
{
    public class LevelStorage: BaseStorage<LevelData>  
    {
        public LevelStorage (string table_):base(table_)
        {}

        public LevelData GetById(int id_)
        {
            LevelData levelData;

            foreach (var item in objects) {
                levelData = (LevelData)item.Value;
                if(levelData.Id == id_){
                    return levelData;
                }
            }
            return  null;
        }
    }
}

