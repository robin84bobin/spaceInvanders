using System;

public class LevelData : BaseData
{
	public int ID { get; set;}
	public string levelSceneName { get; set;}
	public int enemyWaveRate { get; set;}
	public int enemyWaveSize { get; set;}
	public int enemyStartSpeed { get; set;}
	public double enemySpeedFactor { get; set;}
	public double enemyMovePeriod { get; set;}
	public string enemyId { get; set;}
	public string heroId { get; set;}

	[NonSerialized]
	private EnemyData _enemy;
	
	public EnemyData enemy {
			get {
				if (_enemy == null) {
					_enemy = Main.inst.Data.EnemyStorage.Get (enemyId);
				}
				return _enemy;
			}
	}
	[NonSerialized]
	private HeroData _hero;

	public HeroData hero {
		get {
			if (_hero == null)	{
				_hero = Main.inst.Data.HeroStorage.Get(heroId);
			}
			return _hero;
		}
	}
}



