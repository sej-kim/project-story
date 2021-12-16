
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using STORY_ENUM;
using STORY_GAMEDATA;
using System.Text;


public class GameDataManagerItem : CSVParser
{
	Dictionary< int, ITEM_INFO_DATA> _ItemInfoDictionary;

	Dictionary< int, ITEM_COMBINE_DATA> _ItemCombineDictionary;
	Dictionary< int, ITEM_DROP_DONGEON> _ItemDropDongeonDictionary;
	Dictionary< int, ITEM_ELEVAT_DATA> _ItemElevatDictionary;


	public static GameDataManagerItem SingleInstance { get; set; }

	#region SingleTon
	public static GameDataManagerItem Instance
	{
		get
		{
			if (SingleInstance == null)
			{
				SingleInstance = GameObject.FindObjectOfType(typeof(GameDataManagerItem)) as GameDataManagerItem;
				
				if (SingleInstance == null)
				{
					GameObject Container = new GameObject();
					
					SingleInstance = Container.AddComponent(typeof(GameDataManagerItem)) as GameDataManagerItem;
					
					Container.name = SingleInstance.ToString();
				}
			}
			
			return SingleInstance;
		}
	}
	#endregion

	void Awake()
	{
		//Debug.Log("GameDataManager Awake");
		GameObject.DontDestroyOnLoad(gameObject);

		Initialization ();
	}

	void Start()
	{
		LoadDataList ();
	}

	void Initialization ()
	{
		_ItemInfoDictionary	= new Dictionary< int, ITEM_INFO_DATA>();

		_ItemCombineDictionary	= new Dictionary< int, ITEM_COMBINE_DATA>();
		_ItemDropDongeonDictionary	= new Dictionary< int, ITEM_DROP_DONGEON>();
		_ItemElevatDictionary	= new Dictionary< int, ITEM_ELEVAT_DATA>();


		_ParsingDeleage = new Dictionary<string, Parsingfun_Deleage>();
		_ParsingDeleage[ePARSE_FUN_NAME.ItemInfoParse.ToString()] = new Parsingfun_Deleage(ItemInfoParse);
		_ParsingDeleage[ePARSE_FUN_NAME.ItemCombineParse.ToString()] = new Parsingfun_Deleage(ItemCombineParse);
		_ParsingDeleage[ePARSE_FUN_NAME.ItemDropDongeonParse.ToString()] = new Parsingfun_Deleage(ItemDropDongeonParse);
		_ParsingDeleage[ePARSE_FUN_NAME.ItemElevatParse.ToString()] = new Parsingfun_Deleage(ItemElevatParse);
	}

	void LoadDataList()
	{
		LoadFile("tb_ITEM_LIST", ePARSE_FUN_NAME.ItemInfoParse);
		LoadFile("tb_ITEM_COMBINE", ePARSE_FUN_NAME.ItemCombineParse);
		LoadFile("tb_ITEM_DROP_INFO", ePARSE_FUN_NAME.ItemDropDongeonParse);
		LoadFile("tb_CHAR_ELEVAT_ITEM", ePARSE_FUN_NAME.ItemElevatParse);
	}
	
	
	#region ItemInfoParse
	public int ItemInfoParse( string[] inputData )
	{
		ITEM_INFO_DATA dat = new ITEM_INFO_DATA();
		
		int count = 0;

		dat.nIndex = Convert.ToInt32(inputData[count]);
		
		if (dat.nIndex <= 0)
		{
			return 0;
		}

		dat.uID 		= Convert.ToInt32(inputData[++count]);
		//dat.strName 	= inputData[++count];
		dat.nSortType	= Convert.ToInt32(inputData[++count]);
		dat.nClass 		= Convert.ToInt32(inputData[++count]);
		dat.ItemType 	= (eITEM_TYPE)Convert.ToInt32(inputData[++count]);
		dat.nMaxAmont	= Convert.ToInt32(inputData[++count]);
		dat.nVested		= Convert.ToInt32(inputData[++count]);
		dat.nTrade		= Convert.ToInt32(inputData[++count]);
		dat.nBuy 		= Convert.ToInt32(inputData[++count]);
		dat.nGuild 		= Convert.ToInt32 (inputData [++count]);
		dat.nConsignmentBuy	= Convert.ToInt32(inputData[++count]);
		dat.nLevel		= Convert.ToInt32(inputData[++count]);
		dat.nElevatLv		= Convert.ToInt32(inputData[++count]);
		dat.nRevolutionLv	= Convert.ToInt32(inputData[++count]);
		dat.nMaxEnchant		= Convert.ToInt32(inputData[++count]);
		dat.strImagePath	= inputData[++count];


		for (int i_1 = 0; i_1 < 6; ++i_1) 
		{
			ITEM_OPTION_DATA opData = new ITEM_OPTION_DATA();

			opData.uID		= Convert.ToInt32(inputData[++count]);
			opData.nValue	= Convert.ToInt32(inputData[++count]);

			dat.OptionList.Add( opData );
		}

		_ItemInfoDictionary.Add (dat.uID, dat);

		//Debug.Log(dat.nIndex.ToString() + " " + dat.strName + " " + dat.uID.ToString() + " " + dat.nClass.ToString() + " " 
		 //         + dat.nMaxAmont.ToString() + " " + dat.nVested.ToString() + " " + dat.nTrade.ToString() + " " + dat.strImagePath );

		return 0;
	}

	public ITEM_INFO_DATA GetItemInfoData( int uID )
	{
		if (_ItemInfoDictionary.ContainsKey(uID))
		{
			return _ItemInfoDictionary[uID];
		}

		return null;
	}

	#endregion


	public int ItemCombineParse( string[] inputData )
	{
		ITEM_COMBINE_DATA dat = new ITEM_COMBINE_DATA();
		
		int count = 0;
		
		dat.nIndex = Convert.ToInt32(inputData[count]);
		
		if (dat.nIndex <= 0)
		{
			return 0;
		}
		
		dat.uID 		= Convert.ToInt32(inputData[++count]);

		
		for (int i_1 = 0; i_1 < 5; ++i_1) 
		{
			ITEM_COMBINE_INFO_DATA opData = new ITEM_COMBINE_INFO_DATA();
			
			opData.uID		= Convert.ToInt32(inputData[++count]);
			opData.nAmount	= Convert.ToInt32(inputData[++count]);
			
			dat.ItemIDList.Add( opData );
		}


		dat.nGold = Convert.ToInt32(inputData[++count]);
		_ItemCombineDictionary.Add (dat.uID, dat);

		return 0;
	}

	public ITEM_COMBINE_DATA GetItemCombineData( int uID )
	{
		if (_ItemCombineDictionary.ContainsKey(uID))
		{
			return _ItemCombineDictionary[uID];
		}
		
		return null;
	}


	public int ItemDropDongeonParse( string[] inputData )
	{

		ITEM_DROP_DONGEON dat = new ITEM_DROP_DONGEON();
		
		int count = 0;
		
		dat.nIndex = Convert.ToInt32(inputData[count]);
		
		if (dat.nIndex <= 0)
		{
			return 0;
		}
		
		dat.uID 		= Convert.ToInt32(inputData[++count]);
		

		int nDongeonID = Convert.ToInt32(inputData[++count]);

		while (nDongeonID < 0)
		{
			dat.DongeonIDList.Add( nDongeonID );

			nDongeonID = Convert.ToInt32(inputData[++count]);
		}

		_ItemDropDongeonDictionary.Add (dat.uID, dat);

		return 0;

	}

	public ITEM_DROP_DONGEON GetItemDropDongeonData( int uID )
	{
		if (_ItemDropDongeonDictionary.ContainsKey(uID))
		{
			return _ItemDropDongeonDictionary[uID];
		}
		
		return null;
	}


	public int ItemElevatParse( string[] inputData )
	{
		ITEM_ELEVAT_DATA dat = new ITEM_ELEVAT_DATA();
		
		int count = 0;
		
		dat.nIndex = Convert.ToInt32(inputData[count]);
		
		if (dat.nIndex <= 0)
		{
			return 0;
		}
		
		dat.nCharID 		= Convert.ToInt32(inputData[++count]);
		
		
		int nItemID = Convert.ToInt32(inputData[++count]);
		
		while (nItemID < 0)
		{
			dat.ItemIDList.Add( nItemID );
			
			nItemID = Convert.ToInt32(inputData[++count]);
		}
		
		_ItemElevatDictionary.Add (dat.nCharID, dat);

		return 0;
	}

	public ITEM_ELEVAT_DATA GetItemElevatData( int uID )
	{
		if (_ItemElevatDictionary.ContainsKey(uID))
		{
			return _ItemElevatDictionary[uID];
		}
		
		return null;
	}























}
