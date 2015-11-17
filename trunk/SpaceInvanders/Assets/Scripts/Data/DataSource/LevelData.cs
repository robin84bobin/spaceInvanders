using System;

public class LevelData : BaseData
{
	public int ID;
	public string levelSceneName = "Level";
	public int enemyWaveRate;
	public int enemyWaveSize;
	public int enemyStartSpeed;
	public double enemySpeedFactor;
	public double enemyMovePeriod;
	public string enemyId;
	public string heroId;

	[NonSerialized]
	private EnemyData _enemy;

	public EnemyData enemy {
		get {
			if (_enemy == null)	{
				_enemy = Main.inst.Data.EnemyStorage.Get(enemyId);
			}
			return _enemy;
		}
	}

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


