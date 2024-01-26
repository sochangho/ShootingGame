using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = PathString.TABLE)]
public class Table_Player : ScriptableObject , IData
{

	[System.Serializable]
	public class CharacterPlayerInfo
    {
		public int Id; // Ä³¸¯ÅÍ id 

		public float Attack; 
		public float Defence; 
		public float Speed; 
		public float Hp;
		public float Shot_Duration;
		public float Shot_LoadSpeed;

		public int Shot_Count;
		public int Shot_Id;

		public string CharacterName;

		public float Shot_Spped;
	}



	public List<CharacterPlayerInfo> PlayerCharacters; // Replace 'EntityType' to an actual type that is serializable.

	private Dictionary<int, CharacterPlayerInfo> dic_playerCharacter;

	public void Set()
    {
		dic_playerCharacter = new Dictionary<int, CharacterPlayerInfo>();
	 
		for(int i = 0; i < PlayerCharacters.Count; ++i)
        {
			dic_playerCharacter.Add(PlayerCharacters[i].Id, PlayerCharacters[i]);
        }

    }

	public void Reales()
    {


    }

	public CharacterPlayerInfo GetInfoById(int id)
    {
        if (dic_playerCharacter.ContainsKey(id))
        {
			return dic_playerCharacter[id];
        }

		return null;
    }

}
