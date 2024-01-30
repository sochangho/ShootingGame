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

	[System.Serializable]
	public class StageUniqeInfo
	{
		public int Stage;

		public int Time;


	}

	public List<StageInfo> StageInfos; // Replace 'EntityType' to an actual type that is serializable.

	public List<StageCountInfo> StageCounts; // Replace 'EntityType' to an actual type that is serializable.

	public List<StageUniqeInfo> Stage;

	private List<StageInfo>[,] data;

	private List<int> stageNumbers;

	//랜덤 엑세스로 접근
	public void Set()
	{
		stageNumbers = new List<int>();

		data = new List<StageInfo>[StageCounts[0].StageCount, (int)GameObjectType.Size];

		for (int i = 0; i < StageInfos.Count; ++i)
		{
			var info = StageInfos[i];

			if (data[info.Stage, info.GameObjectType] == null)
			{
				data[info.Stage, info.GameObjectType] = new List<StageInfo>();
			}

			data[info.Stage, info.GameObjectType].Add(info);


			if (!stageNumbers.Contains(info.Stage))
			{
				stageNumbers.Add(info.Stage);
			}

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

	public StageUniqeInfo GetStageUniqeInfo(int curStage)
    {
	   return Stage.Find(x => x.Stage == curStage);
    }


	public int CurrentStage(int index)
	{

		if (stageNumbers.Count - 1 < index)
		{
			return -1;  // -1 경우 종료
		}

		return stageNumbers[index];
	}

	

}
