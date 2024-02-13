using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemUseType
{
	Ability = 0,
	Weapon,
	Recovery ,

	Size,
}



[ExcelAsset(AssetPath = PathString.TABLE)]
public class Table_Item : ScriptableObject, IData
{
	public interface IItemInfo
    {
		int GetId();

		string GetName();

		string GetSubscript();

		int GetPrice();

		float GetWeight();
    
	}


	[System.Serializable]
	public class ItemAbilityInfo : IItemInfo
    {
		public int Id;

		public string Name;
		public string Subscript;

		public float Attack;
		public float Defence;
		public float Speed;
		public float Hp;
		public float Shot_Duration;
		public float Shot_LoadSpeed;
		public float Shot_Count;
		public float Shot_Spped;
		public float Weight;
		public int Price;

		public int Duration;
		public string CalculateClass;


		public int GetId()
        {
			return Id;
        }

		public string GetName()
        {
			return Name;
        }

		public string GetSubscript()
        {
			return Subscript;
        }

		public int GetPrice()
        {
			return Price;
        }

		public float GetWeight()
        {
			return Weight;
        }



	}


	[System.Serializable]
	public class ItemWeaponInfo : IItemInfo
	{
		public int Id;
		public string Name;
		public string Subscript;
		public int Price;
		public int Duration;
		public float Weight;
		public float Value;
		public string WeaponActionClass;

		public int GetId()
		{
			return Id;
		}

		public string GetName()
		{
			return Name;
		}

		public string GetSubscript()
		{
			return Subscript;
		}

		public int GetPrice()
		{
			return Price;
		}

		public float GetWeight()
		{
			return Weight;
		}
	}



	[System.Serializable]
	public class ItemTypeInfo
    {
		public int Id;  

		public string Type;    
		
		public float Weight;

	}



	public List<ItemAbilityInfo> Ability; // Replace 'EntityType' to an actual type that is serializable.
	public List<ItemWeaponInfo> Weapons;
	public List<ItemTypeInfo> ItemType; // Replace 'EntityType' to an actual type that is serializable.

	private bool isLoaded = false;


	private Dictionary<int , IItemInfo>[] dic_ItemData;

	private List<KeyValuePair<int, IItemInfo>> listPair_ItemData;


	public void Set()
    {
		if (isLoaded)
		{
			return;
		}

		isLoaded = true;


		SetItemInfos<ItemAbilityInfo>(Ability, ItemUseType.Ability);
		SetItemInfos<ItemWeaponInfo>(Weapons, ItemUseType.Weapon);
	}


	public void Reales()
    {


    }

	public void SetItemInfos<T>(List<T> listData , ItemUseType itemUseType) where T : IItemInfo
    {
		if (dic_ItemData == null)
		{
			dic_ItemData = new Dictionary<int,IItemInfo>[(int)ItemUseType.Size];

			for(int i = 0; i < (int)ItemUseType.Size; ++i)
            {
				dic_ItemData[i] = new Dictionary<int, IItemInfo>();
            }

		}

		if(listPair_ItemData == null)
        {
			listPair_ItemData = new List<KeyValuePair<int, IItemInfo>>();
        }



		for (int i = 0; i < listData.Count; i++)
		{						
			dic_ItemData[(int)itemUseType].Add(listData[i].GetId() , listData[i]);

			listPair_ItemData.Add(new KeyValuePair<int, IItemInfo>((int)itemUseType, listData[i]));
		
		}

	}


	public IItemInfo GetItemInfo(int typeid, int itemid)
    {

		if(typeid - 1 >= 0 && dic_ItemData[typeid - 1].ContainsKey(itemid))
        {
			return dic_ItemData[typeid - 1][itemid];
		}


		return null;
	}


	public IItemInfo GetRandomItemInfo()
    {

		List<KeyValuePair<float, ItemTypeInfo>> itPair = new List<KeyValuePair<float, ItemTypeInfo>>();

		for(int i = 0; i < ItemType.Count; ++i)
        {
			itPair.Add(new KeyValuePair<float, ItemTypeInfo>(ItemType[i].Weight, ItemType[i]));
        }

		


		var t =  RandomManager.CumulativeProbability(itPair);

		int id = t.Id;
		
		var dic =  dic_ItemData[id - 1];



		List<KeyValuePair<float, IItemInfo>> itInfos = new List<KeyValuePair<float, IItemInfo>>();

		foreach (var d in dic)
        {
			var info = d.Value;

			itInfos.Add(new KeyValuePair<float, IItemInfo>(info.GetWeight(), info));

        }


		return  RandomManager.CumulativeProbability(itInfos);
	}


	public ItemUseType GetItemUseTypeById(int id)
    {
		var type =  listPair_ItemData.Find(x => x.Value.GetId() == id).Key;

		return (ItemUseType)type;
    }
	
}
