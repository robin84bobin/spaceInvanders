using System;
using Assets.Scripts.Data.Attributes;

namespace Assets.Scripts.Data.DataSource
{
    public class LevelData : BaseData
    {
        [DbField]
        public int Id { get; set;}
        [DbField]
        public string LevelSceneName { get; set;}
        [DbField]
        public int EnemyWaveRate { get; set;}
        [DbField]
        public int EnemyWaveSize { get; set;}
        [DbField]
        public int EnemyStartSpeed { get; set;}
        [DbField]
        public double EnemySpeedFactor { get; set;}
        [DbField]
        public double EnemyMovePeriod { get; set;}
        [DbField]
        public string EnemyId { get; set;}
        [DbField]
        public string HeroId { get; set;}

        private EnemyData _enemy;
	
        public EnemyData Enemy {
            get {
                if (_enemy == null) {
                    _enemy = Main.Inst.Data.Get<EnemyData>(EnemyId);
                }
                return _enemy;
            }
        }

        private HeroData _hero;

        public HeroData Hero {
            get {
                if (_hero == null)	{
                    _hero = Main.Inst.Data.Get<HeroData>(HeroId);
                }
                return _hero;
            }
        }
    }
}



