using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = PathString.TABLE)]
public class Table_Stage : ScriptableObject
{
	
	[System.Serializable]
	public class StageInfo
	{
		public int Stage;
		public int GameObjectType;
		public int Id;
		public int Weight;
		public int Capacity;
	}
	[System.Serializable]
	public class StageCountInfo
    {
		public int StageCount;
	}



	public List<StageInfo> StageInfos; // Replace 'EntityType' to an actual type that is serializable.

    public List<StageCountInfo> StageCounts; // Replace 'EntityType' to an actual type that is serializable.

	private List<StageInfo>[,] data;

	//·£´ý ¿¢¼¼½º·Î Á¢±Ù
	public void Set()
	{

		data = new List<StageInfo>[StageCounts[0].StageCount, (int)GameObjectType.Size];

		for (int i = 0; i < StageInfos.Count; ++i)
		{
			var info = StageInfos[i];
			
			if (data[info.Stage, info.GameObjectType] == null)
			{
				data[info.Stage, info.GameObjectType] = new List<StageInfo>();
			}

			data[info.Stage, info.GameObjectType].Add(info);
		}

		Debug.Log(data);
	}

	public void Reales()
	{


	}


	public List<StageInfo> GetStageInfos(int stage, int type)
	{
		return data[stage, type];
	}
}
