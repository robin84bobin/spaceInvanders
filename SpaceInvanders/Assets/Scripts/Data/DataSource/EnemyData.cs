namespace Assets.Scripts.Data.DataSource
{
    public class EnemyData : AbstractWarriorData
    {
        public string[] bonusIds;

        private BonusData[] _bonuses;
        public BonusData[] Bonuses {
            get {
                if (_bonuses == null && bonusIds != null && bonusIds.Length > 0) {
                    _bonuses = new BonusData[bonusIds.Length];
                    for (int i = 0; i < bonusIds.Length; i++) {
                        _bonuses[i] = Main.Inst.Data.Get<BonusData>( bonusIds[i] );
                    }
                }
                return _bonuses;
            }
        }
    }
}



