using UnityEngine;
using System.Collections.Generic;
using STORY_ENUM;

namespace STORY_GAMEDATA
{
    public class USER_DATA
    {
        public int nInit; // 최초 유저 등록 여부
        public int nMoney   = 0;
        public int nCash    = 0;
		public int nLevel   = 0;
		public int nExp 	= 0;
    }
    public class OBJECT_DEFAULT_DATA
    {
		public int nRow 		= 0;
        public int nIndex       = 0;
		public int uID			= 0;
        public string strName   = "";
		public int nType 		= 0;
		public eDAMAGE_TYPE DamageType = eDAMAGE_TYPE.None;
		public int nLevel 		= 1;
		public int nElevatLv = 0;
		public int nRevolutionLv = 1;
		public int nPos 		= 0;
		public int nPosType		= 0;
		public string strImage  = "";
		public int nAI 			= 0;
		public float fAtDist    = 0;
		public float fAtDelay   = 0;
		public float fMoveSpeed = 0;

		public int nBattleDeck 	= 0;
		public int nExp;

		public OBJECT_TYPE ObjectType = OBJECT_TYPE.NONE;
		public OBJECT_TYPE TargetObjType = OBJECT_TYPE.NONE;

		public OBJECT_STAT_DATA StatData = new OBJECT_STAT_DATA();
     }

	public class OBJECT_STAT_DATA
	{
		public int nIndex		= 0;
		public int uID		= 0;
		public float nStr         = 0;
		public float nDex         = 0;
		public float nInt		    = 0;

		public float fStrValue    = 0.0f; // 
		public float fDexValue    = 0.0f;
		public float fIntValue    = 0.0f;
		public float fHPValue    	= 0.0f;
		public float fMPValue    	= 0.0f;
		public float fPowerValue	= 0.0f;
		public float fPhysicsValue    		= 0.0f;
		public float fMagicValue    			= 0.0f;
		public float fPhysicsHitValue 	= 0.0f;
		public float fMagicHitValue 		= 0.0f;
		public float fPhysicsCriValue 		= 0.0f;
		public float fMagicCriValue 			= 0.0f;

		public float fPhysicsDefecsValue 		= 0.0f;
		public float fMagicDefecsValue 		= 0.0f;

		public float fPhysicsMissValue 		= 0.0f;
		public float fMagicMissValue 			= 0.0f;

		public float fPhysicsPenetration = 0.0f;
		public float fMagicPenetration = 0.0f;
	}

	public class OBJECT_VALUE_DATA
	{
		public int nStr 		= 0;
		public int nDex 		= 0;
		public int nInte 		= 0;
		public int nHP 			= 0;
		public int nMP 			= 0;
		public int nPhysicsAt 	= 0;
		public int nMagicAt 	= 0;
		public int nPhysicsCri 	= 0;
		public int nMagicCri 	= 0;
		public int nPhysicsHit 	= 0;
		public int nMagicHit 	= 0;
		public int nPhysicsDefens 	= 0;
		public int nMagicDefens 	= 0;
		public int nAtpr 			= 0;
		public int nPhysicsMiss 	= 0;
		public int nMagicMiss 		= 0;
		public int nMoveSpeed 		= 0;
		public int nAttackSpeed 	= 0;
		public int nTeamExp 		= 0;
		public int nCharExp 		= 0;
		public int CharInnrExp 		= 0;
	}

    public class OBJECT_HPMP_DATA
    {
        public int nMaxHP = 0;
        public int nCurHP = 0;
        public int nMaxMP = 0;
        public int nCurMP = 0;
    }

	public class OBJECT_ELEVAT_DATA
	{
		public int nIndex 	= 0;
		public int uID 		= 0;
		public int nLevel	= 0;
		public float fStr = 0.0f;
		public float fDex = 0.0f;
		public float fInte = 0.0f;
		public float fHP = 0.0f;
		public float fMP = 0.0f;
		public float fPhysicsAt = 0.0f;
		public float fMagicAt = 0.0f;
		public float fPhysicsHit = 0.0f;
		public float fMagicHit = 0.0f;
		public float fPhysicsCri = 0.0f;
		public float fMagicCri = 0.0f;

		public float fPhysicsDefens = 0.0f;
		public float fMagicDefens = 0.0f;
		public float fAttack = 0.0f;

		public float fPhysicsMiss = 0.0f; 
		public float fMagicMiss = 0.0f;
		public float fScale = 0.0f;
	}

	public class OBJECT_REVOLUTION_DATA
	{
		public int nIndex 	= 0;
		public int uID 		= 0;
		public int nLevel	= 0;
		public float fStr = 0.0f;
		public float fDex = 0.0f;
		public float fInte = 0.0f;
		public float fHP = 0.0f;
		public float fMP = 0.0f;
		public float fPhysicsAt = 0.0f;
		public float fMagicAt = 0.0f;
		public float fPhysicsHit = 0.0f;
		public float fMagicHit = 0.0f;
		public float fPhysicsCri = 0.0f;
		public float fMagicCri = 0.0f;
		
		public float fPhysicsDefens = 0.0f;
		public float fMagicDefens = 0.0f;
		public float fAttack = 0.0f;
		
		public float fPhysicsMiss = 0.0f; 
		public float fMagicMiss = 0.0f;
	}

	public class OBJECT_EXP_DATA
	{
		public int nIndex = 0;
		public int nLevel = 0;
		public int uID = 0;

		public float []fTeamExt = new float[4];
		public float []fCharExt = new float[4];
		public float []fInExt = new float[4];
	}

    public class STAGE_DATA
    {     
        public int nMapIndex = 0;
		public int []Mons;

    }

    public class BUFF_DATA
    {
		public int nType = 0;
		public int nValue = 0;
    }

    public class DE_BUFF_DATA
    {
		public int nType = 0;
		public int nValue = 0;
    }

    public class SKILL_DATA
    {
		public float fTime;
		public string strImage;
    }

    public class ACTIVE_SKILL_DATA
    { 
    }

    public class PASSIVE_SKILL_DATA
    {
 
    }

    public class USER_INVEN_DATA
    { 
    }

    public class USER_SKILL_DATA
    { 
    }

    // 던전 타잎 정보( 스토리, 모험, 전장, 등등 )
    public class DUNGEON_TYPE_DATA
    {
        public int nIndex   = 0;
        public int nID      = 0; // 던전 유니코드
        public int nType    = 0; // 던전 타입( 스토리, 모험, 전장, 등등 )
        public int nSort    = 0; // 
        public string strName           = ""; // 타입 이름
        public string strTabImageIn     = ""; // 타입 텝 버튼 이미지  
        public string strTabImageOut    = ""; // 타입 텝 버튼 이미지 
        public string strSound          = ""; 
    }

    public class DUNGEON_CHAPTER_DATA
    {
        public int nIndex       = 0;
        public int nID          = 0;
        public int nChapter     = 0;
        public int nType        = 0;
        public string strName   = "";
        public string strImageIn    = "";
        public string strImageOut   = "";
    }

    public class DUNGEON_DATA
    {
        public int nIndex           = 0;
        public int nID              = 0;
        public int nType            = 0;
        public int nChapter         = 0;
        public int nLevel           = 0;
        public int nTitleID         = 0;
        public int nDescID          = 0;
        public int nRewardItemID    = 0;
        public int nStageID      	= 0;
        public int nSoundID         = 0;
		public string strMap 		= "";
		public int nLimitLevel 		= 0;
		public int nLimitCount 		= 0;
		public int nGoItemID 		= 0;
		public int nGoItemAmount 	= 0;
    }

    public class DUNGEON_REWARD_ITEM_DATA
    {
        public int nID      = 0;
        public int nAmount  = 0;
        public int nRate    = 0;
    }

    public class DUNGEON_REWARD_ITEM_LIST_DATA
    {
        public int nIndex       = 0;
        public int nID          = 0;
        public int nUserExp		= 0;
        public int nCharExp	   	= 0;
		public int nGold 		= 0;

        public List< DUNGEON_REWARD_ITEM_DATA> ItemDataList = new List<DUNGEON_REWARD_ITEM_DATA>();
    }
    
    public class STORY_TEXT_DATA
    {
        public int nIndex       = 0;
        public int nID          = 0;
        public string strText   = "";
    }

    public class DUNGEON_STAGE_DATA
    {
        public int nClearTime   = 0;
        public int nMobGroupID  = 0;
    }

    public class DUNGEON_STAGE_LIST_DATA
    {
        public int nIndex = 0;
        public int nID = 0;

        public List<DUNGEON_STAGE_DATA> DunGeonStageList = new List<DUNGEON_STAGE_DATA>();

    }

    public class DUNGEON_MOB_GROUP_DATA
    {
        public int nCharID = 0;
    }

    public class DUNGEON_MOB_GROUP_LIST_DATA
    {
        public int nIndex = 0;
        public int nID = 0;

        public List<DUNGEON_MOB_GROUP_DATA> DungeonMobGroupList = new List<DUNGEON_MOB_GROUP_DATA>();
        
    }

    public class DUNGEON_STAGE_RESOURCE_DATA
    {
        public int nIndex           = 0;
        public int nID              = 0;
        public string strBgPath     = "";
        public int nSoundID         = 0;
    }

	public class IMAGE_PATH
	{
		public string CHAR = "Prefabs/Chars/";
		public string MAP = "Prefabs/Map/";
		public string POPUP = "Prefabs/PopUp/";
	}


	public class ITEM_OPTION_DATA
	{
		public int uID;
		public int nValue;
	}

	public class ITEM_INFO_DATA
	{
		public int nRow = 0; 
		public int nIndex = 0;
		public int uID = 0;
		public string strName = "";
		public int nClass = 0;
		public int nSortType = 0;
		public eITEM_TYPE ItemType = eITEM_TYPE.NONE;
		public int nMaxAmont = 0;
		public int nVested = 0;
		public int nTrade = 0;
		public int nBuy = 0;
		public int nGuild = 0;
		public int nConsignmentBuy;
		public int nLevel = 0;
		public int nElevatLv = 0;
		public int nRevolutionLv = 0;
		public int nMaxEnchant = 0;
		public string strImagePath = "";

		public List< ITEM_OPTION_DATA > OptionList = new List<ITEM_OPTION_DATA>();

	}

	public class ITEM_COMBINE_INFO_DATA
	{
		public int uID = 0;
		public int nAmount = 0;
	}

	public class ITEM_COMBINE_DATA
	{
		public int nIndex = 0;
		public int uID = 0;
		public int nGold = 0;
		public List< ITEM_COMBINE_INFO_DATA > ItemIDList = new List<ITEM_COMBINE_INFO_DATA>();
	}


	public class ITEM_DROP_DONGEON
	{
		public int nIndex = 0;
		public int uID = 0;

		public List< int > DongeonIDList = new List<int>();
	}

	public class ITEM_ELEVAT_DATA
	{
		public int nIndex = 0;
		public int nCharID = 0;
		
		public List< int > ItemIDList = new List<int>();
	}

	public class BULLET_DATA
	{
		public string strPath = "";
		public float fSpeed = 0;
		public float fDeadTime = 0;
		public Vector3 tPos;
	}


	public class EFFECT_DATA
	{
		public int nIndex = 0;
		public int nScreenFx = 0;
		public int nPause = 0;
		public int nType = 0;
		public string strPath = "";
		public string strPos = "";
	}

	public class RESULT_DATA
	{
		public int nExp = 0;
		public int nGold = 0;

		public List< int > RewardItemList = new List< int >();
	}














};
