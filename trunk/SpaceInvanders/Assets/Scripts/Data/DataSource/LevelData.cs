using System;

namespace Data{

public class LevelData : BaseData
{
	public int ID { get; internal set;}
	public string levelSceneName { get; internal set;}
	public int enemyWaveRate { get; internal set;}
	public int enemyWaveSize { get; internal set;}
	public int enemyStartSpeed { get; internal set;}
	public double enemySpeedFactor { get; internal set;}
	public double enemyMovePeriod { get; internal set;}
	public string enemyId { get; internal set;}
	public string heroId { get; internal set;}

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
}


