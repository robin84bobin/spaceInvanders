using System;
using Assets.Scripts.Data.Attributes;

namespace Assets.Scripts.Data.DataSource
{
    [Serializable]
    public class LevelData : BaseData
    {
        [DbField]
        public int id;// { get; set;}

        [DbField]
        public string levelSceneName;//{ get; set;}
        [DbField]
        public int enemyWaveRate;// { get; set;}
        [DbField]
        public int enemyWaveSize;// { get; set;}
        [DbField]
        public int enemyStartSpeed;// { get; set;}
        [DbField]
        public double enemySpeedFactor;// { get; set;}
        [DbField]
        public double enemyMovePeriod;// { get; set;}
        [DbField]
        public string enemyId;// { get; set;}
        [DbField]
        public string heroId;// { get; set;}

        private EnemyData _enemy;
	
        public EnemyData Enemy {
            get {
                if (_enemy == null) {
                    _enemy = Main.Inst.Data.Get<EnemyData>(enemyId);
                }
                return _enemy;
            }
        }

        private HeroData _hero;

        public HeroData Hero {
            get {
                if (_hero == null)	{
                    _hero = Main.Inst.Data.Get<HeroData>(heroId);
                }
                return _hero;
            }
        }
    }
}



