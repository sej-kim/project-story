namespace STORY_ENUM
{

    public enum OBJECT_STATE
    {
        NONE = 100,
        MOVE,
		ATTACK,
		SKILL,
        STURN,
		SLEEP,
        DIE,
        BEATEN,
    }

    public enum ERROR_MSG
    {
        ERROR_NONE,
        ERRPR_STATE,
    }

    public enum SCENE_TYPE
    {
        EMPTY_SCENE = 100,
        LOGO_SCENE,
        LOADING_SCENE,
        MAINMENU_SCENE,
        BATTLE_SCENE,
    }

    public enum OBJECT_TYPE
    {
        PLAYER = 0,
        MONSTER,
        NPC,
        BUILDING,
        NONE,
    }

    public enum MONSTER_NAME
    {
        MT_BEAR,
        MT_COW,
        MT_HIPPO,
    }

    public enum PLYAER_NAME
    {
        HongGilDong_Mecanim,
        Robin_Mecanim,
        TinkerBell_Mecanim,
    }

    public enum POPUP_TYPE
    {
        VictoryPopUp = 0,
        DefeatPopUp,
        StagePopUp,
        BattleDeck,
		UIvictory,
		ResultPopUp,
		DungeonTypePopUp,
    }


    public enum SKILL_TYPE
    {
        SKILL_PASSIVE,
        SKILL_ACTIVE,
    }

	public enum eSKILL_NAME
	{
		NONE,
	}

    public enum ATTACK_SCOPE
    {
        WIDTH,
        HEIGHT,
        CROSS,
        ALL,
    }

    public enum BUFFE_TYPE
    {
        BUFFE_AE,
        BUFFE_DE,
    }

    public enum BUFFE
    { 
    }

    public enum DE_BUFF
    {
    }

    public enum ATTACK_TYPE
    {
        SHORT,
        LONG,
        SUPPORT,
    }

	public enum eDAMAGE_TYPE
	{
		Physical = 1,
		Magic,
		None,
	}

    public enum ATTRIBUTE_TYPE
    {
    
    }

    public enum ITEM_TYPE
    {
        EQUIPE_ITEM,
        POSION_ITEM,
        QUEST_ITEM,
    }

    public enum EQUIPE_ITEM_TYPE
    {
        EQ_WEAPON,
        EQ_ARMOR,
    }


    public enum  ePARSE_FUN_NAME
    {
        CharInfoParse,			
		CharStatParse,
		CharElevatConstParser,
		CharEvolutionConstParser,
		CharExpParser,
		CharDamageConstParser,  // 
		DungeonTypeDataParse,   // 던전 타잎 파서 함수 이름
        DungeonChapterParse,    // 던전 쳅터 파서 함수 이름
        DungeonDataParse,       // 던전 세브정보 파서 함수 이름
        DungeonRewardItemParse, // 던전 보상 아이템 파서 함수 이름
        DungeonStageGropParse,  // 던전 스테이지 리스트 파서 함수 이름
        DungeonMobListParse,    // 던전 출연 몹리스트 파서 함수 이름
        DungeonStageResourceParse, // 던전 스테이지 파서 리소스 함수 이름
        
		ItemInfoParse, //
		ItemCombineParse,
		ItemDropDongeonParse,
		ItemElevatParse,

		EffectParse,

		PcIDListParse,
		StoryTextDataParse,     // 게임 텍스트 파서 함수 이름


    }


	public enum eDAMAGE_CONST
	{
		DEFAULT_DMG_CONST = 0,
		DEFAULT_DMG_BAL_CONST,
		OP_PROPO_CONST,
		MAX_DEFAULT_DMG_RAND_CONST,
		MIN_DEFAULT_DMG_RAND_CONST,
		P_ATT_EVA_SUCC_MAGNIF,
		P_CRITI_DECID_LEV_CONST,
		P_CRITI_DECID_STAT_CONST,
		P_CRITI_MAX_FRIQ_RAT,
		P_CRITI_HIGH_FRIQ_RAT,
		P_CRITI_NORMAL_FRIQ_RAT,
		P_CRITI_LOW_FRIQ_RAT,
		P_CRITI_MIN_FRIQ_RAT,
		P_CRITI_DMG_STAT_CONST,
		P_CRITI_DMG_LEV_CONST,
		P_CRITI_DMG_MAX_RANGE_CONST,
		P_CRITI_DMG_MIN_RANGE_CONST,
		P_DFP_THRR_PROPO_CONST,
		P_DFP_IGN_INC_CONST,
		P_DFP_THR_MAX_RAT,
		MAX_P_THR_DMG_CONST,
		MIN_P_THR_DMG_CONST,
		DEFAULT_S_DMG_CONST,
		DEFAULT_S_DMG_BAL_CONST,
		S_OP_PROPO_CONST,
		MAX_DEFAULT_S_DMG_CONST,
		MIN_DEFAULT_S_DMG_CONST,
		S_ATT_EVA_SUCC_MAGNIF,
		S_CRITI_DECID_LEV_CONST,
		S_CRITI_DECID_STAT_CONST,
		MAX_FRIQ_RAT,
		HIGH_FRIQ_RAT,
		NORMAL_FRIQ_RAT,
		LOW_FRIQ_RAT,
		MIN_FRIQ_RAT,
		S_CRITI_DMG_STAT_CONST,
		S_CRITI_DMG_LEV_CONST,
		S_CRITI_MAX_RANGE_CONST,
		S_CRITI_MIN_RANGE_CONST,
		S_RESIST_THRR_PROPO_CONST,
		S_RESIST_THRR_INC_CONST,
		S_RESIST_THR_MAX_RAT,
		MAX_S_RESIST_THR_RAND,
		MIN_S_RESIST_THR_RAND,
		MAX_SIZE
	}

	public enum eEFFECT_TYPE
	{
		NONE,
	}


	public enum eITEM_TYPE
	{
		NONE,
		WEAPON,
		EQUIPE,
		POSION,
		MIX,
		OTHERS,
	}


	public enum eEffectPos
	{
		FXDummy_name = 0,
		FXDummy_stun,
		FXDummy_breast,
		FXDummy_hand_R,
		FXDummy_hand_L,
		FXDummy_foot_R,
		FXDummy_foot_L,
		FXDummy_weapon_01,
		FXDummy_weapon_02,
		FXDummy_weapon_03,
		Bip001_Footsteps,
		Root_Dummy,
		MAX_SIZE,
	}
























};

