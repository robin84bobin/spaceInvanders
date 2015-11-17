using System;
using System.Collections.Generic;
using System.Reflection;

public class LevelStorage: BaseStorage<LevelData>  
{
	public LevelStorage (string table):base(table)
	{}

	public LevelData GetByID(int id)
	{
		LevelData levelData;

		foreach (var item in _objects) {
			levelData = (LevelData)item.Value;
			if(levelData.ID == id){
				return levelData;
			}
		}
		return  null;
	}
}

