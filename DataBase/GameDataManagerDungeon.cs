
using UnityEngine;
using System;
using System.Collections.Generic;
using STORY_ENUM;
using STORY_GAMEDATA;
using System.Text;

public class GameDataManagerDungeon : CSVParser
{
	List<DUNGEON_TYPE_DATA> _DungeonTypeList; // 던전 타입 리스트( 스토리, 모험, 전장, 등등 )
	
	Dictionary<int, List<DUNGEON_CHAPTER_DATA>> _DungeonChapterDictionary; // 던전 쳅터리스트( 1장, 2장, ... )
	
	Dictionary<int, Dictionary<int, DUNGEON_DATA>> _DungeonDictionary;// 던전 세부 정보 리스트 
	
	Dictionary<int, DUNGEON_REWARD_ITEM_LIST_DATA> _DungeonRewardItemDictionary; // 던전 보상 아이템 리스트 정보
	
	Dictionary<int, DUNGEON_STAGE_LIST_DATA> _DungeonStageListDictionary; // 던전 보상 아이템 리스트 정보
	
	Dictionary<int, DUNGEON_MOB_GROUP_LIST_DATA> _DunGeonMobGropListDictionary; // 던전 몹 그룹 리스트 정보
	
	//Dictionary<int, DUNGEON_STAGE_RESOURCE_DATA> _DungeonStageResourceDictionary; // 던전 스테이지 리소스 정보
	
    Dictionary<int, STORY_TEXT_DATA> _StoryTextDictionary; // 게임 전반적인 텍스트 리스트

	public static GameDataManagerDungeon SingleInstance { get; set; }

    #region SingleTon
	public static GameDataManagerDungeon Instance
    {
        get
        {
            if (SingleInstance == null)
            {
				SingleInstance = GameObject.FindObjectOfType(typeof(GameDataManagerDungeon)) as GameDataManagerDungeon;

                if (SingleInstance == null)
                {
                    GameObject Container = new GameObject();

					SingleInstance = Container.AddComponent(typeof(GameDataManagerDungeon)) as GameDataManagerDungeon;
					SingleInstance.Initialization();

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
        GameObject.DontDestroyOnLoad(this);
        
		//Initialization ();
    }

    void Start()
    {
		LoadDataList ();
    }

	void Initialization()
	{
		_DungeonTypeList                = new List<DUNGEON_TYPE_DATA>();
		_DungeonChapterDictionary       = new Dictionary<int, List<DUNGEON_CHAPTER_DATA>>();
		_DungeonDictionary              = new Dictionary<int, Dictionary<int, DUNGEON_DATA>>();
		_DungeonRewardItemDictionary    = new Dictionary<int, DUNGEON_REWARD_ITEM_LIST_DATA>();
		_DungeonStageListDictionary     = new Dictionary<int, DUNGEON_STAGE_LIST_DATA>();
		_DunGeonMobGropListDictionary   = new Dictionary<int, DUNGEON_MOB_GROUP_LIST_DATA>();
		//_DungeonStageResourceDictionary = new Dictionary<int, DUNGEON_STAGE_RESOURCE_DATA>();


		_StoryTextDictionary         = new Dictionary<int, STORY_TEXT_DATA>();


		_ParsingDeleage = new Dictionary<string, Parsingfun_Deleage>();
		_ParsingDeleage[ePARSE_FUN_NAME.DungeonTypeDataParse.ToString()] 		= new Parsingfun_Deleage(DungeonTypeParse);
		_ParsingDeleage[ePARSE_FUN_NAME.DungeonChapterParse.ToString()] 		= new Parsingfun_Deleage(DungeonChapterParse);
		_ParsingDeleage[ePARSE_FUN_NAME.DungeonDataParse.ToString()] 			= new Parsingfun_Deleage(DungeonDataParse);
		_ParsingDeleage[ePARSE_FUN_NAME.DungeonRewardItemParse.ToString()] 		= new Parsingfun_Deleage(DungeonRewardItemParse);
		_ParsingDeleage[ePARSE_FUN_NAME.DungeonStageGropParse.ToString()] 		= new Parsingfun_Deleage(DungeonStageGropParse);
		_ParsingDeleage[ePARSE_FUN_NAME.DungeonMobListParse.ToString()] 		= new Parsingfun_Deleage(DungeonMobListParse);
		//_ParsingDeleage[ePARSE_FUN_NAME.DungeonStageResourceParse.ToString()] 	= new Parsingfun_Deleage(DungeonStageResourceParse);

		_ParsingDeleage[ePARSE_FUN_NAME.StoryTextDataParse.ToString()] 			= new Parsingfun_Deleage(StoryTextDataParse);
		
	}
	
	void LoadDataList()
	{
		LoadFile("tb_dungeon_type", ePARSE_FUN_NAME.DungeonTypeDataParse);
		LoadFile("tb_dungeon_chapter", ePARSE_FUN_NAME.DungeonChapterParse);
		LoadFile("tb_dungeon_list", ePARSE_FUN_NAME.DungeonDataParse);
		LoadFile("tb_dungeon_text", ePARSE_FUN_NAME.StoryTextDataParse);
		LoadFile("tb_Reward", ePARSE_FUN_NAME.DungeonRewardItemParse);
		LoadFile("tb_StageGroup", ePARSE_FUN_NAME.DungeonStageGropParse);
		LoadFile("tb_MobGroup", ePARSE_FUN_NAME.DungeonMobListParse);
		//LoadFile("tb_StageResoruce", ePARSE_FUN_NAME.DungeonStageResourceParse);
	}
	
	#region DungeonTypeParse
	/// <summary>
	/// 
	/// </summary>
	/// <param name="inputData"></param>
	/// <returns></returns>
	public int DungeonTypeParse(string[] inputData)
	{
		DUNGEON_TYPE_DATA dat = new DUNGEON_TYPE_DATA();
		int count = 0;
		
		//Debug.Log(inputData[count]);
		
		dat.nIndex = Convert.ToInt32(inputData[count]);
		
		if (dat.nIndex <= 0)
		{
			return 0;
		}
		
		
		dat.nID = Convert.ToInt32(inputData[++count]);
		dat.nType = Convert.ToInt32(inputData[++count]);
		dat.nSort = Convert.ToInt32(inputData[++count]);
		dat.strName = inputData[++count];
		dat.strTabImageIn = inputData[++count];
		dat.strTabImageOut = inputData[++count];
		dat.strSound = inputData[++count];
		
		
		_DungeonTypeList.Add(dat);
		
		//Debug.Log(dat.nIndex.ToString() + " " + dat.nID.ToString() + " " + dat.nType.ToString() + " " 
		//     + dat.nSort.ToString() + " " + dat.strName + " " + dat.strTabImageIn + " " + dat.strTabImageOut + " " + dat.strSound + " ");
		
		return 0;
	}
	#endregion
	
	
	#region GetDungeonTypeData
	/// <summary>
	/// 
	/// </summary>
	/// <param name="nIndex"></param>
	/// <returns></returns>
	public  DUNGEON_TYPE_DATA GetDungeonTypeData(int nIndex)
	{
		if (_DungeonTypeList.Count <= 0)
		{
			return null;
		}
		
		return _DungeonTypeList[nIndex];
	}
	
	public List<DUNGEON_TYPE_DATA> GetDungeonTypeList()
	{
		return _DungeonTypeList;
	}
	
	#endregion 
	
	
	#region DungeonChapterParse
	public int DungeonChapterParse(string[] inputData) 
	{ 
		DUNGEON_CHAPTER_DATA dat = new DUNGEON_CHAPTER_DATA();
		
		int count = 0;
		
		dat.nIndex = Convert.ToInt32(inputData[count]);
		
		if (dat.nIndex <= 0)
		{
			return 0;
		}
		
		dat.nID             = Convert.ToInt32(inputData[++count]);
		dat.nChapter        = Convert.ToInt32(inputData[++count]);
		dat.nType           = Convert.ToInt32(inputData[++count]);
		dat.strName         = inputData[++count];
		dat.strImageOut     = inputData[++count];
		dat.strImageIn      = inputData[++count];
		
		
		
		if (_DungeonChapterDictionary.ContainsKey(dat.nChapter) == false)
		{
			_DungeonChapterDictionary.Add(dat.nChapter, new List<DUNGEON_CHAPTER_DATA>());
		}
		
		
		_DungeonChapterDictionary[dat.nChapter].Add(dat);
		
		//Debug.Log(dat.nIndex.ToString() + " " + dat.nID.ToString() + " " + dat.nChapter.ToString() + " " +
		//          dat.nType.ToString() + " " + dat.strName + " " + dat.strImageIn + " " +
		//          dat.strImageOut );
		
		return 0; 
	}
	#endregion
	
	#region GetDungeonChapterData
	public List<DUNGEON_CHAPTER_DATA> GetDungeonChapterData( int nType )
	{
		if (_DungeonChapterDictionary.ContainsKey(nType))
		{
			return _DungeonChapterDictionary[nType];
		}
		return null;
	}
	#endregion
	
	
	#region DungeonDataParse
	public int DungeonDataParse(string[] inputData)
	{
		DUNGEON_DATA dat = new DUNGEON_DATA();
		
		int count = 0;
		
		dat.nIndex = Convert.ToInt32(inputData[count]);
		
		//Debug.Log(inputData.Length.ToString());
		
		if (dat.nIndex <= 0)
		{
			return 0;
		}
		
		
		dat.nID             = Convert.ToInt32(inputData[++count]);
		dat.nType           = Convert.ToInt32(inputData[++count]);
		dat.nChapter        = Convert.ToInt32(inputData[++count]);
		dat.nLevel          = Convert.ToInt32(inputData[++count]);
		dat.nTitleID        = Convert.ToInt32(inputData[++count]);
		dat.nDescID         = Convert.ToInt32(inputData[++count]);
		dat.nRewardItemID   = Convert.ToInt32(inputData[++count]);
		dat.nStageID     	= Convert.ToInt32(inputData[++count]);
		dat.nSoundID        = Convert.ToInt32(inputData[++count]);
		dat.strMap			= inputData[++count];
		dat.nLimitLevel		= Convert.ToInt32(inputData[++count]);
		dat.nLimitCount   	= Convert.ToInt32(inputData[++count]);
		dat.nGoItemID     	= Convert.ToInt32(inputData[++count]);
		dat.nGoItemAmount	= Convert.ToInt32(inputData[++count]);

		//Debug.Log(dat.strMap);
		
		if (_DungeonDictionary.ContainsKey(dat.nType) == false)
		{
			_DungeonDictionary.Add( dat.nType, new Dictionary<int,DUNGEON_DATA>() );
		}
		
		_DungeonDictionary[dat.nType].Add(dat.nID, dat);

		//Debug.Log (dat.nStageID.ToString ());
//		 Debug.Log(dat.nIndex.ToString() + " " + dat.nID.ToString() + " " + dat.nChapter.ToString() + " " + 
//		           dat.nLevel.ToString() + " " + dat.nTitleID.ToString() + " " + dat.nDescID.ToString() + " " +
//		          dat.nRewardItemID.ToString() );
		
		//(_DungeonDictionary[dat.nType])[dat.nID] = dat;
		return 0;
		
	}
	#endregion
	
	#region GetDungeonData
	public List<DUNGEON_DATA> GetDungeonData(int nType, int nChapter )
	{
		List<DUNGEON_DATA> resultList = new List<DUNGEON_DATA>();
		
		Dictionary<int, DUNGEON_DATA> DunDicTemp = _DungeonDictionary[nType];
		
		foreach (KeyValuePair<int, DUNGEON_DATA> kv in DunDicTemp)
		{
			if (kv.Value.nChapter == nChapter)
			{
				resultList.Add(kv.Value);
				//Debug.Log("GetDungeonData" + kv.Value.nChapter.ToString());
			}
		}
		
		return resultList;
	}
	#endregion
	
	#region DungeonRewardItemParse
	public int DungeonRewardItemParse(string[] inputData)
	{
		DUNGEON_REWARD_ITEM_LIST_DATA dat = new DUNGEON_REWARD_ITEM_LIST_DATA();
		
		int count = 0;
		dat.nIndex = Convert.ToInt32(inputData[count]);
		
		if (dat.nIndex <= 0)
		{
			return 0;
		}
		
		dat.nID         = Convert.ToInt32(inputData[++count]);
		dat.nUserExp	= Convert.ToInt32(inputData[++count]);
		dat.nCharExp  	= Convert.ToInt32(inputData[++count]);
		dat.nGold	  	= Convert.ToInt32(inputData[++count]);
		//Debug.Log( "DUNGEON_REWARD_ITEM_LIST_DATA " +  dat.nID.ToString() + " " + dat.nClass.ToString() + " " + dat.nAttribute.ToString());
		/////
		while (true)
		{
			int nDropItemID = Convert.ToInt32(inputData[++count]);
			
			if (nDropItemID <= 0)
			{
				break;
			}
			
			DUNGEON_REWARD_ITEM_DATA ItemData = new DUNGEON_REWARD_ITEM_DATA();
			ItemData.nID        = nDropItemID;
			ItemData.nAmount    = Convert.ToInt32(inputData[++count]);
			ItemData.nRate      = Convert.ToInt32(inputData[++count]);
			dat.ItemDataList.Add(ItemData);
			
			//Debug.Log("DUNGEON_REWARD_ITEM " +  ItemData.nID.ToString() + " " + ItemData.nAmount.ToString() + " " + ItemData.nRate.ToString());
		}
		
		_DungeonRewardItemDictionary[dat.nID] = dat;
		
		return 0;
	}
	#endregion
	
	#region GetDungeonRewardItemListData
	public DUNGEON_REWARD_ITEM_LIST_DATA GetDungeonRewardItemListData( int nID )
	{
		if (_DungeonRewardItemDictionary.ContainsKey(nID))
		{
			return _DungeonRewardItemDictionary[nID];
		}
		
		return null;
	}
	#endregion
	
	#region DungeonStageGropParse
	public int DungeonStageGropParse(string[] inputData)
	{
		DUNGEON_STAGE_LIST_DATA dat = new DUNGEON_STAGE_LIST_DATA();
		
		int count = 0;
		dat.nIndex = Convert.ToInt32(inputData[count]);
		
		if (dat.nIndex <= 0)
		{
			return 0;
		}
		
		dat.nID = Convert.ToInt32(inputData[++count]);
		
		//Debug.Log("DUNGEON_STAGE_LIST_DATA " + dat.nID.ToString());
		/////
		while (true)
		{
			int nMobGroupID = Convert.ToInt32(inputData[++count]);
			
			if (nMobGroupID <= 0)
			{
				break;
			}

			DUNGEON_STAGE_DATA StageData = new DUNGEON_STAGE_DATA();

			StageData.nMobGroupID           = nMobGroupID;
			StageData.nClearTime            = Convert.ToInt32(inputData[++count]);

			dat.DunGeonStageList.Add(StageData);
			
			//Debug.Log("DUNGEON_STAGE_DATA " + " " + StageData.nClearTime.ToString() + " " + StageData.nMobGroupID.ToString());
		}
		
		_DungeonStageListDictionary[dat.nID] = dat;
		
		return 0;
	}
	#endregion
	
	#region GatStageListData
	public DUNGEON_STAGE_LIST_DATA GatStageListData(int nID)
	{
		if (_DungeonStageListDictionary.ContainsKey(nID))
		{
			return _DungeonStageListDictionary[nID];
		}
		
		return null;
	}
	#endregion
	
	#region DungeonMobListParse
	public int DungeonMobListParse(string[] inputData)
	{
		DUNGEON_MOB_GROUP_LIST_DATA dat = new DUNGEON_MOB_GROUP_LIST_DATA();
		
		int count = 0;
		dat.nIndex = Convert.ToInt32(inputData[count]);
		
		if (dat.nIndex <= 0)
		{
			return 0;
		}
		
		dat.nID = Convert.ToInt32(inputData[++count]);
		
		//Debug.Log("DUNGEON_MOB_GROUP_LIST_DATA " + dat.nID.ToString());
		/////
		while (true)
		{
			//int nCharID = Convert.ToInt32(inputData[++count]);;

			//Int32.TryParse( inputData[++count], nCharID );

			int nCharID = 0;
			if (!int.TryParse(inputData[++count], out nCharID)) 
			{
				nCharID = 0;
				//Debug.Log("DUNGEON_MOB_GROUP_LIST_DATA " + dat.nIndex.ToString());
			}



			if (nCharID <= 0)
			{
				break;
			}
			
			DUNGEON_MOB_GROUP_DATA MobGropData = new DUNGEON_MOB_GROUP_DATA();

			MobGropData.nCharID = nCharID;
			dat.DungeonMobGroupList.Add(MobGropData);
			
			//Debug.Log("DUNGEON_MOB_GROUP_DATA " + MobGropData.nCharID.ToString() );
		}
		
		_DunGeonMobGropListDictionary[dat.nID] = dat;
		
		return 0;
	}
	#endregion
	
	#region GetDungeonMobGroupList
	public DUNGEON_MOB_GROUP_LIST_DATA GetDungeonMobGroupList(int nID)
	{
		if (_DunGeonMobGropListDictionary.ContainsKey(nID))
		{
			return _DunGeonMobGropListDictionary[nID];
		}
		
		return null;
	}
	#endregion
	
	#region DungeonStageResourceParse
	public int DungeonStageResourceParse(string[] inputData)
	{
		DUNGEON_STAGE_RESOURCE_DATA dat = new DUNGEON_STAGE_RESOURCE_DATA();
		
		int count = 0;
		dat.nIndex = Convert.ToInt32(inputData[count]);
		
		if (dat.nIndex <= 0)
		{
			return 0;
		}
		
		dat.nID             = Convert.ToInt32(inputData[++count]);
		dat.strBgPath       = inputData[++count];
		
		++count;
		
		dat.nSoundID        = Convert.ToInt32(inputData[++count]);
		
		
		//_DungeonStageResourceDictionary[dat.nID] = dat;
		
		//Debug.Log("DUNGEON_STAGE_RESOURCE_DATA " + dat.nID.ToString() + " " + dat.strBgPath + " " + dat.nSoundID.ToString());
		
		return 0;
	}
	#endregion

	/*
	#region GetDungeonStageResource
	public DUNGEON_STAGE_RESOURCE_DATA GetDungeonStageResource(int nID)
	{
		if (_DunGeonMobGropListDictionary.ContainsKey(nID))
		{
			return _DungeonStageResourceDictionary[nID];
		}
		
		return null;
	}
	#endregion
	*/
	
	#region StoryTextDataParse
	public int StoryTextDataParse(string[] inputData)
	{
		STORY_TEXT_DATA dat = new STORY_TEXT_DATA();
		
		int count = 0;
		
		
		dat.nIndex = Convert.ToInt32(inputData[count]);
		
		
		if (dat.nIndex <= 0)
		{
			return 0;
		}
		
		dat.nID = Convert.ToInt32(inputData[++count]);
		dat.strText = inputData[++count];
		
		//Debug.Log(dat.nIndex.ToString() + " " + dat.nID.ToString() + " " + dat.strText);
		
		
		_StoryTextDictionary[dat.nID] = dat;
		return 0;
	}
	#endregion
	
	#region GetStoryTextData
	public STORY_TEXT_DATA GetStoryTextData(int nID)
	{
		if (_StoryTextDictionary.ContainsKey(nID))
		{
			return _StoryTextDictionary[nID];
		}
		
		return null;
	}
	#endregion

}