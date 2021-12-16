
using UnityEngine;
using System;
using System.Collections.Generic;
using STORY_ENUM;
using STORY_GAMEDATA;
using System.Text;


public class GameDataManagerChar : CSVParser
{
	Dictionary< int, OBJECT_DEFAULT_DATA> _ObjectDataDictionary;
	Dictionary< int, OBJECT_STAT_DATA > _StatDataDictionary;

	Dictionary< int, OBJECT_ELEVAT_DATA > _ObjectElevatDataDictionary;
	Dictionary< int, OBJECT_REVOLUTION_DATA > _ObjectRevolDataDictionary;
	List< OBJECT_EXP_DATA> _ObjectExpDataList;

	List< EFFECT_DATA > _EffectList;
	float[] _DamageConstList;

	List< int[] > _PcIDList;

	public static GameDataManagerChar SingleInstance { get; set; }

	#region SingleTon
	public static GameDataManagerChar Instance
	{
		get
		{
			if (SingleInstance == null)
			{
				SingleInstance = GameObject.FindObjectOfType(typeof(GameDataManagerChar)) as GameDataManagerChar;
				
				if (SingleInstance == null)
				{
					GameObject Container = new GameObject();
					
					SingleInstance = Container.AddComponent(typeof(GameDataManagerChar)) as GameDataManagerChar;
					
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
		_ObjectDataDictionary	= new Dictionary< int, OBJECT_DEFAULT_DATA>();
		_StatDataDictionary		= new Dictionary< int, OBJECT_STAT_DATA >();

		_ObjectElevatDataDictionary		= new Dictionary< int, OBJECT_ELEVAT_DATA >();
		_ObjectRevolDataDictionary		= new Dictionary< int, OBJECT_REVOLUTION_DATA >();
		_ObjectExpDataList				= new List< OBJECT_EXP_DATA >();

		_DamageConstList = new float[(int)eDAMAGE_CONST.MAX_SIZE];

		_EffectList = new List< EFFECT_DATA > ();

		_PcIDList = new List<int[]> ();

		_ParsingDeleage = new Dictionary<string, Parsingfun_Deleage>();
		_ParsingDeleage[ePARSE_FUN_NAME.CharInfoParse.ToString()] = new Parsingfun_Deleage(CharInfoParse);
		_ParsingDeleage[ePARSE_FUN_NAME.CharStatParse.ToString()] = new Parsingfun_Deleage(CharStatDataParse);
		_ParsingDeleage[ePARSE_FUN_NAME.CharDamageConstParser.ToString()] = new Parsingfun_Deleage(CharDamageConstParser);


		_ParsingDeleage[ePARSE_FUN_NAME.CharElevatConstParser.ToString()] = new Parsingfun_Deleage(CharElevatConstParser);
		_ParsingDeleage[ePARSE_FUN_NAME.CharEvolutionConstParser.ToString()] = new Parsingfun_Deleage(CharEvolutionConstParser);
		_ParsingDeleage[ePARSE_FUN_NAME.CharExpParser.ToString()] = new Parsingfun_Deleage(CharExpParser);

		_ParsingDeleage[ePARSE_FUN_NAME.EffectParse.ToString()] = new Parsingfun_Deleage(EffectParse);


		_ParsingDeleage[ePARSE_FUN_NAME.PcIDListParse.ToString()] = new Parsingfun_Deleage(PcIDListParse);



	}

	void LoadDataList()
	{
		LoadFile("tb_CHAR", ePARSE_FUN_NAME.CharInfoParse);
		LoadFile("tb_CHAR_DEFAULT_STAT", ePARSE_FUN_NAME.CharStatParse);
		LoadFile("Effect_List", ePARSE_FUN_NAME.EffectParse);


		LoadFile("tb_Damage_Const", ePARSE_FUN_NAME.CharDamageConstParser);

		LoadFile("tb_CHAR_ELEVAT_STATCONST", ePARSE_FUN_NAME.CharElevatConstParser);
		LoadFile("tb_CHAR_EVOLV_STATCONST", ePARSE_FUN_NAME.CharEvolutionConstParser);
		LoadFile("tb_CHAR_EXP", ePARSE_FUN_NAME.CharExpParser);

		LoadFile("PC_ID_List", ePARSE_FUN_NAME.PcIDListParse);
	}
	
	
	#region CharInfoParse
	public int CharInfoParse( string[] inputData )
	{
		OBJECT_DEFAULT_DATA dat = new OBJECT_DEFAULT_DATA();
		
		int count = 0;

		//Debug.Log ("asdfasdfasdfasdfasdf");

		dat.nIndex = Convert.ToInt32(inputData[count]);
		
		if (dat.nIndex <= 0)
		{
			return 0;
		}

		dat.uID 		= Convert.ToInt32(inputData[++count]);
		dat.strName 	= inputData[++count];
		dat.nType 		= Convert.ToInt32(inputData[++count]);
		dat.DamageType 	= (eDAMAGE_TYPE)Convert.ToInt32(inputData[++count]);
		dat.nLevel		= Convert.ToInt32(inputData[++count]);
		dat.nElevatLv	= Convert.ToInt32(inputData[++count]);
		dat.nRevolutionLv	= Convert.ToInt32(inputData[++count]);
		dat.nPos 		= Convert.ToInt32(inputData[++count]);
		dat.nPosType	= Convert.ToInt32(inputData[++count]);
		dat.strImage 	= inputData[++count];
		dat.nAI			= Convert.ToInt32(inputData[++count]);

		dat.fAtDist		= Convert.ToSingle(inputData[++count]);
		dat.fAtDelay	= Convert.ToSingle(inputData[++count]);
		dat.fMoveSpeed	= Convert.ToSingle(inputData[++count]);

		//Debug.Log (dat.nIndex.ToString ());
		//Debug.Log (dat.strImage);
		_ObjectDataDictionary.Add (dat.uID, dat);

		//Debug.Log(dat.nIndex.ToString() + " " + dat.uID.ToString() + " " + dat.strName + " " 
		//          + dat.nType.ToString() + " " + dat.DamageType.ToString() + " " + dat.nPos.ToString() + " " + dat.strImage );

		return 0;
	}

	public OBJECT_DEFAULT_DATA GetCharInfoData( int uID )
	{
		if (_ObjectDataDictionary.ContainsKey(uID))
		{
			return _ObjectDataDictionary[uID];
		}

		return null;
	}

	#endregion


	public int CharStatDataParse( string[] inputData )
	{

		OBJECT_STAT_DATA dat = new OBJECT_STAT_DATA();
		
		int count = 0;

		dat.nIndex = Convert.ToInt32(inputData[count]);

		if (dat.nIndex <= 0)
		{
			return 0;
		}

		//return 0;

		dat.uID = Convert.ToInt32(inputData[++count]);

		dat.nStr = Convert.ToSingle(inputData[++count]);
		dat.nDex = Convert.ToSingle(inputData[++count]);
		dat.nInt = Convert.ToSingle(inputData[++count]);
		dat.fStrValue = Convert.ToSingle(inputData[++count]);
		dat.fDexValue = Convert.ToSingle(inputData[++count]);

		dat.fIntValue = Convert.ToSingle(inputData[++count]);
		dat.fHPValue = Convert.ToSingle(inputData[++count]);
		dat.fPowerValue = Convert.ToSingle(inputData[++count]);
		dat.fPhysicsValue = Convert.ToSingle(inputData[++count]);
		dat.fMagicValue = Convert.ToSingle(inputData[++count]);
		dat.fPhysicsHitValue = Convert.ToSingle(inputData[++count]);
		dat.fMagicHitValue = Convert.ToSingle(inputData[++count]);
		dat.fPhysicsCriValue = Convert.ToSingle(inputData[++count]);
		dat.fMagicCriValue = Convert.ToSingle(inputData[++count]);
		dat.fPhysicsDefecsValue = Convert.ToSingle(inputData[++count]);
		dat.fMagicDefecsValue = Convert.ToSingle(inputData[++count]);
		dat.fPhysicsMissValue = Convert.ToSingle(inputData[++count]);
		dat.fMagicMissValue = Convert.ToSingle(inputData[++count]);
		dat.fPhysicsPenetration = Convert.ToSingle(inputData[++count]);
		dat.fMagicPenetration = Convert.ToSingle(inputData[++count]);


		_StatDataDictionary.Add (dat.uID, dat);

		//Debug.Log(dat.nIndex.ToString() + " " + dat.uID.ToString() + " " + dat.nStr.ToString() + " " 
		 //         + dat.nDex.ToString() + " " + dat.nInt.ToString() + " " + dat.fStrValue.ToString() + " " + dat.fDexValue.ToString() );


		return 0;
	}

	public OBJECT_STAT_DATA GetCharStatData( int uID )
	{
		if (_StatDataDictionary.ContainsKey(uID))
		{
			return _StatDataDictionary[uID];
		}
		return null;
	}

	public int CharDamageConstParser( string[] inputData )
	{
		int count = 0;

		int nIndex = Convert.ToInt32(inputData[count]);

		if (nIndex <= 0)
		{
			return 0;
		}


		int nSize = (int)eDAMAGE_CONST.MAX_SIZE;

		/*
		foreach (string buf in inputData) 
		{
			_DamageConstList[count] = Convert.ToDouble(buf);
			++count;
		}
*/

		//Debug.Log ("Size      " + nSize.ToString ());
		//Debug.Log ("Size111   " + inputData.Length.ToString ());
		for ( int i_1 = 0; i_1 < nSize; ++i_1) 
		{
			//double fff = Convert.ToDouble(inputData[++count]);
			_DamageConstList[i_1] = Convert.ToSingle(inputData[++count]);
			//Debug.Log( i_1.ToString() );
		}

		/*
		Debug.Log (_DamageConstList[1].ToString());
		Debug.Log (_DamageConstList[2].ToString());
		Debug.Log (_DamageConstList[3].ToString());
		Debug.Log (_DamageConstList[4].ToString());
		Debug.Log (_DamageConstList[5].ToString());
		Debug.Log (_DamageConstList[6].ToString());
		Debug.Log (_DamageConstList[7].ToString());
		Debug.Log (_DamageConstList[8].ToString());
		Debug.Log (_DamageConstList[9].ToString());
		Debug.Log (_DamageConstList[10].ToString());
		Debug.Log (_DamageConstList[11].ToString());
		Debug.Log (_DamageConstList[12].ToString());
		Debug.Log (_DamageConstList[13].ToString());
		Debug.Log (_DamageConstList[14].ToString());
		Debug.Log (_DamageConstList[15].ToString());
		Debug.Log (_DamageConstList[16].ToString());
		Debug.Log (_DamageConstList[17].ToString());
		Debug.Log (_DamageConstList[18].ToString());
		Debug.Log (_DamageConstList[19].ToString());
		Debug.Log (_DamageConstList[20].ToString());
		Debug.Log (_DamageConstList[21].ToString());
		Debug.Log (_DamageConstList[22].ToString());
		Debug.Log (_DamageConstList[23].ToString());
		Debug.Log (_DamageConstList[24].ToString());
		Debug.Log (_DamageConstList[25].ToString());
		Debug.Log (_DamageConstList[26].ToString());
		Debug.Log (_DamageConstList[27].ToString());
		Debug.Log (_DamageConstList[28].ToString());
		Debug.Log (_DamageConstList[29].ToString());
		Debug.Log (_DamageConstList[30].ToString());
		Debug.Log (_DamageConstList[31].ToString());
		Debug.Log (_DamageConstList[32].ToString());
		Debug.Log (_DamageConstList[33].ToString());
		Debug.Log (_DamageConstList[34].ToString());
		Debug.Log (_DamageConstList[35].ToString());
		Debug.Log (_DamageConstList[36].ToString());
		Debug.Log (_DamageConstList[37].ToString());
		Debug.Log (_DamageConstList[38].ToString());
		Debug.Log (_DamageConstList[39].ToString());
		Debug.Log (_DamageConstList[40].ToString());
		Debug.Log (_DamageConstList[41].ToString());
		Debug.Log (_DamageConstList[42].ToString());
		Debug.Log (_DamageConstList[43].ToString());
*/

		return 0;
	}

	public float GetDamageConstData( eDAMAGE_CONST dConst )
	{
		return _DamageConstList [(int)dConst];
	}

	
	public int CharElevatConstParser(string[] inputData)
	{
		OBJECT_ELEVAT_DATA dat = new OBJECT_ELEVAT_DATA();
		
		int count = 0;
		
		dat.nIndex = Convert.ToInt32(inputData[count]);
		
		if (dat.nIndex <= 0)
		{
			return 0;
		}

		dat.uID = Convert.ToInt32(inputData[++count]);
		dat.nLevel = Convert.ToInt32(inputData[++count]);
		dat.fStr = Convert.ToSingle(inputData[++count]);
		dat.fDex = Convert.ToSingle(inputData[++count]);
		dat.fInte = Convert.ToSingle(inputData[++count]);
		dat.fHP = Convert.ToSingle(inputData[++count]);
		dat.fMP = Convert.ToSingle(inputData[++count]);
		dat.fPhysicsAt = Convert.ToSingle(inputData[++count]);
		dat.fMagicAt = Convert.ToSingle(inputData[++count]);
		dat.fPhysicsHit = Convert.ToSingle(inputData[++count]);
		dat.fMagicHit = Convert.ToSingle(inputData[++count]);
		dat.fPhysicsCri = Convert.ToSingle(inputData[++count]);
		dat.fMagicCri = Convert.ToSingle(inputData[++count]);
		dat.fPhysicsDefens = Convert.ToSingle(inputData[++count]);
		dat.fMagicDefens = Convert.ToSingle(inputData[++count]);
		dat.fAttack = Convert.ToSingle(inputData[++count]);
		dat.fPhysicsMiss = Convert.ToSingle(inputData[++count]);
		dat.fMagicMiss = Convert.ToSingle(inputData[++count]);
		dat.fScale = Convert.ToSingle(inputData[++count]);

		_ObjectElevatDataDictionary.Add (dat.uID, dat);

		/*
		dat = GetElevatData (dat.uID);
		Debug.Log ( "tb_CHAR_ELEVAT_STATCONST - " + "  승급 레벨 아이디 :" + dat.uID.ToString () +  "  캐릭터 승급 레벨 :" + dat.nLevel.ToString () + "  힘 승급상수 :" + dat.fStr.ToString () 
		           + "  민첩 승급상수 :" + dat.fDex.ToString () + "  지능 승급상수 :" + dat.fInte.ToString () + "  HP 승급상수:" + dat.fHP.ToString () + 
		           "  MP 승급상수 :" + dat.fMP.ToString () + "  물리 공격력 승급상수 :" + dat.fPhysicsAt.ToString() + "  마법 공격력 승급상수 :" + dat.fMagicAt.ToString() +
		           "  물리 공격 명중률 승급상수 :" + dat.fPhysicsHit.ToString () + "  마법 공격 명중률 승급상수 :" + dat.fMagicHit.ToString() + "  물리 치명타 승급상수 :"
		           + dat.fPhysicsCri.ToString() + "  마법 치명타 승급상수 :" + dat.fMagicCri.ToString () + "  물리 방어력 승급상수 :" + dat.fPhysicsDefens.ToString() 
		           + "  마법 저항력 승급상수 :" + dat.fMagicDefens.ToString() + "  전투력 승급상수 :" + dat.fAttack.ToString () 
		           + "  물리 회피율 승급상수 :" + dat.fPhysicsMiss.ToString() + "  마법 회피율 승급상수 :" + dat.fMagicMiss.ToString() 
		           + "  캐릭터 스케일 값 승급상수 :" + dat.fScale.ToString () ); 

*/
		return 0;
	}

	public OBJECT_ELEVAT_DATA GetElevatData( int uID )
	{
		if (_ObjectElevatDataDictionary.ContainsKey(uID))
		{
			/*
			OBJECT_ELEVAT_DATA dat = _ObjectElevatDataDictionary[uID];

			Debug.LogFormat( "tb_CHAR_ELEVAT_STATCONST - 승급 레벨 아이디 : {0} 캐릭터 승급 레벨 : {1} 힘 승급상수 : {2} " +
			                "민첩 승급상수 : {3} 지능 승급상수 : {4} HP 승급상수 : {5}" +
			                "MP 승급상수 : {6} 물리 공격력 승급상수 : {7} 마법 공격력 승급상수 : {8} " +
			                "물리 공격 명중률 승급상수 : {9} 마법 공격 명중률 승급상수 : {10} 물리 치명타 승급상수 : {11}" +
			                "마법 치명타 승급상수 : {12} 물리 방어력 승급상수 : {13} 마법 저항력 승급상수 : {14} 전투력 승급상수 : {15} 물리 회피율 승급상수 : {16} 마법 회피율 승급상수 : {17} 캐릭터 스케일 값 승급상수 : {18}", dat.uID,
			                dat.nLevel, dat.fStr, dat.fDex, dat.fInte, dat.fHP, dat.fMP, dat.fPhysicsAt, dat.fMagicAt, dat.fPhysicsHit, dat.fMagicHit, dat.fPhysicsCri, dat.fMagicCri, dat.fPhysicsDefens
			                , dat.fMagicDefens , dat.fAttack , dat.fPhysicsMiss, dat.fMagicMiss, dat.fScale );
			/*
			Debug.Log ( "tb_CHAR_ELEVAT_STATCONST - " + "  승급 레벨 아이디 :" + dat.uID.ToString () +  "  캐릭터 승급 레벨 :" + dat.nLevel.ToString () + "  힘 승급상수 :" + dat.fStr.ToString () 
			           + "  민첩 승급상수 :" + dat.fDex.ToString () + "  지능 승급상수 :" + dat.fInte.ToString () + "  HP 승급상수:" + dat.fHP.ToString () + 
			           "  MP 승급상수 :" + dat.fMP.ToString () + "  물리 공격력 승급상수 :" + dat.fPhysicsAt.ToString() + "  마법 공격력 승급상수 :" + dat.fMagicAt.ToString() +
			           "  물리 공격 명중률 승급상수 :" + dat.fPhysicsHit.ToString () + "  마법 공격 명중률 승급상수 :" + dat.fMagicHit.ToString() + "  물리 치명타 승급상수 :"
			           + dat.fPhysicsCri.ToString() + "  마법 치명타 승급상수 :" + dat.fMagicCri.ToString () + "  물리 방어력 승급상수 :" + dat.fPhysicsDefens.ToString() 
			           + "  마법 저항력 승급상수 :" + dat.fMagicDefens.ToString() + "  전투력 승급상수 :" + dat.fAttack.ToString () 
			           + "  물리 회피율 승급상수 :" + dat.fPhysicsMiss.ToString() + "  마법 회피율 승급상수 :" + dat.fMagicMiss.ToString() 
			           + "  캐릭터 스케일 값 승급상수 :" + dat.fScale.ToString () ); 
			           */
			           
			return _ObjectElevatDataDictionary[uID];
		}

		return null;
	}


	public int CharEvolutionConstParser(string[] inputData)
	{
		OBJECT_REVOLUTION_DATA dat = new OBJECT_REVOLUTION_DATA();
		
		int count = 0;
		
		dat.nIndex = Convert.ToInt32(inputData[count]);
		
		if (dat.nIndex <= 0)
		{
			return 0;
		}
		
		dat.uID = Convert.ToInt32(inputData[++count]);
		dat.nLevel = Convert.ToInt32(inputData[++count]);

		dat.fStr = Convert.ToSingle(inputData[++count]);
		dat.fDex = Convert.ToSingle(inputData[++count]);
		dat.fInte = Convert.ToSingle(inputData[++count]);
		dat.fHP = Convert.ToSingle(inputData[++count]);
		dat.fMP = Convert.ToSingle(inputData[++count]);
		dat.fPhysicsAt = Convert.ToSingle(inputData[++count]);
		dat.fMagicAt = Convert.ToSingle(inputData[++count]);
		dat.fPhysicsHit = Convert.ToSingle(inputData[++count]);
		dat.fMagicHit = Convert.ToSingle(inputData[++count]);
		dat.fPhysicsCri = Convert.ToSingle(inputData[++count]);
		dat.fMagicCri = Convert.ToSingle(inputData[++count]);
		dat.fPhysicsDefens = Convert.ToSingle(inputData[++count]);
		dat.fMagicDefens = Convert.ToSingle(inputData[++count]);
		dat.fAttack = Convert.ToSingle(inputData[++count]);
		dat.fPhysicsMiss = Convert.ToSingle(inputData[++count]);
		dat.fMagicMiss = Convert.ToSingle(inputData[++count]);

		_ObjectRevolDataDictionary.Add (dat.uID, dat);

		/*
		Debug.Log ( "tb_CHAR_EVOLU_STATCONST - " + "  승급 레벨 아이디 :" + dat.uID.ToString () +  "  캐릭터 승급 레벨 :" + dat.nLevel.ToString () + "  힘 승급상수 :" + dat.fStr.ToString () 
		           + "  민첩 승급상수 :" + dat.fDex.ToString () + "  지능 승급상수 :" + dat.fInte.ToString () + "  HP 승급상수:" + dat.fHP.ToString () + 
		           "  MP 승급상수 :" + dat.fMP.ToString () + "  물리 공격력 승급상수 :" + dat.fPhysicsAt.ToString() + "  마법 공격력 승급상수 :" + dat.fMagicAt.ToString() +
		           "  물리 공격 명중률 승급상수 :" + dat.fPhysicsHit.ToString () + "  마법 공격 명중률 승급상수 :" + dat.fMagicHit.ToString() + "  물리 치명타 승급상수 :"
		           + dat.fPhysicsCri.ToString() + "  마법 치명타 승급상수 :" + dat.fMagicCri.ToString () + "  물리 방어력 승급상수 :" + dat.fPhysicsDefens.ToString() 
		           + "  마법 저항력 승급상수 :" + dat.fMagicDefens.ToString() + "  전투력 승급상수 :" + dat.fAttack.ToString () 
		           + "  물리 회피율 승급상수 :" + dat.fPhysicsMiss.ToString() + "  마법 회피율 승급상수 :" + dat.fMagicMiss.ToString() ); 
		           */
		return 0;
	}

	public OBJECT_REVOLUTION_DATA GetRevolutionData( int uID )
	{
		if (_ObjectRevolDataDictionary.ContainsKey(uID))
		{
			return _ObjectRevolDataDictionary[uID];
		}
		return null;
	}


	public int CharExpParser(string[] inputData)
	{
		OBJECT_EXP_DATA dat = new OBJECT_EXP_DATA();
		
		int count = 0;
		
		dat.nIndex = Convert.ToInt32(inputData[count]);
		
		if (dat.nIndex <= 0)
		{
			return 0;
		}
		
		dat.nLevel = Convert.ToInt32(inputData[++count]);
		dat.uID = Convert.ToInt32(inputData[++count]);

		dat.fTeamExt[0] = Convert.ToSingle(inputData[++count]);
		dat.fTeamExt[1] = Convert.ToSingle(inputData[++count]);
		dat.fTeamExt[2] = Convert.ToSingle(inputData[++count]);
		dat.fTeamExt[3] = Convert.ToSingle(inputData[++count]);
		dat.fCharExt[0] = Convert.ToSingle(inputData[++count]);
		dat.fCharExt[1] = Convert.ToSingle(inputData[++count]);
		dat.fCharExt[2] = Convert.ToSingle(inputData[++count]);
		dat.fCharExt[3] = Convert.ToSingle(inputData[++count]);
		dat.fInExt[0] = Convert.ToSingle(inputData[++count]);
		dat.fInExt[1] = Convert.ToSingle(inputData[++count]);
		dat.fInExt[2] = Convert.ToSingle(inputData[++count]);
		dat.fInExt[3] = Convert.ToSingle(inputData[++count]);

		_ObjectExpDataList.Add (dat);

		return 0;
	}

	public OBJECT_EXP_DATA GetExpData( int nLevel )
	{
		if (_ObjectExpDataList.Count < nLevel) 
		{
			return null;
		}

		return _ObjectExpDataList[nLevel - 1];
	}

	public int EffectParse(string[] inputData)
	{
		EFFECT_DATA dat = new EFFECT_DATA();
		
		int count = 0;
		
		dat.nIndex = Convert.ToInt32(inputData[count]);
		
		if (dat.nIndex <= 0)
		{
			return 0;
		}
		
		dat.nScreenFx = Convert.ToInt32(inputData[++count]);
		dat.nPause = Convert.ToInt32(inputData[++count]);
		dat.nType = Convert.ToInt32(inputData[++count]);

		dat.strPath = inputData[++count];
		dat.strPos = inputData[++count];

		_EffectList.Add (dat);

		//Debug.Log(dat.nIndex.ToString() + " " + dat.nScreenFx.ToString() + " " + dat.nPause.ToString() + " " 
		//          + dat.nType.ToString() + " " + dat.strPath + " " + dat.strPos );

		return 0;
	}
	
	public EFFECT_DATA GetEffectData( int nIndex )
	{
		return _EffectList[nIndex - 1];
	}

	public int PcIDListParse( string[] inputData )
	{
		int[] dat = new int[5];
		
		int count = 0;

		int nIndex =  Convert.ToInt32(inputData[count]);
		
		if (nIndex <= 0)
		{
			return 0;
		}

		for (int i_1 = 0; i_1 < 5; ++i_1) 
		{
			dat[i_1] = Convert.ToInt32(inputData[++count]);
		}

		_PcIDList.Add (dat);

		return 0;
	}

	public int[] GetPcIDList()
	{
		int nIndex = UnityEngine.Random.Range( 0, _PcIDList.Count );
		return _PcIDList[nIndex];
	}


















}
