using UnityEngine;
using System;
using System.Collections;
using STORY_GAMEDATA;

public class ObjectStatValue : MonoBehaviour 
{
	protected OBJECT_STAT_DATA 				_ObjStatData;
	protected OBJECT_VALUE_DATA				_ObjValueData;

	public void initObjectStat( OBJECT_DEFAULT_DATA ObjDefaultData )
	{
		_ObjStatData = new OBJECT_STAT_DATA ();
		_ObjStatData = GameDataManagerChar.Instance.GetCharStatData (ObjDefaultData.uID);
		
		_ObjValueData = new OBJECT_VALUE_DATA ();

		int uID = Convert.ToInt32 (ObjDefaultData.uID.ToString () + ObjDefaultData.nElevatLv.ToString ());
		OBJECT_ELEVAT_DATA ElevatData = GameDataManagerChar.Instance.GetElevatData (uID);
		

//		Debug.Log ( "tb_CHAR_ELEVAT_STATCONST - " + "  승급 레벨 아이디 :" + uID.ToString() + "  캐릭터 승급 레벨 :" + ElevatData.nLevel.ToString () + "  힘 승급상수 :" + ElevatData.fStr.ToString () 
//		           + "  민첩 승급상수 :" + ElevatData.fDex.ToString () + "  지능 승급상수 :" + ElevatData.fInte.ToString () + "  HP 승급상수:" + ElevatData.fHP.ToString () + 
//		           "  MP 승급상수 :" + ElevatData.fMP.ToString () + "  물리 공격력 승급상수 :" + ElevatData.fPhysicsAt.ToString() + "  마법 공격력 승급상수 :" + ElevatData.fMagicAt.ToString() +
//		           "  물리 공격 명중률 승급상수 :" + ElevatData.fPhysicsHit.ToString () + "  마법 공격 명중률 승급상수 :" + ElevatData.fMagicHit.ToString() + "  물리 치명타 승급상수 :"
//		           + ElevatData.fPhysicsCri.ToString() + "  마법 치명타 승급상수 :" + ElevatData.fMagicCri.ToString () + "  물리 방어력 승급상수 :" + ElevatData.fPhysicsDefens.ToString() 
//		           + "  마법 저항력 승급상수 :" + ElevatData.fMagicDefens.ToString() + "  전투력 승급상수 :" + ElevatData.fAttack.ToString () 
//		           + "  물리 회피율 승급상수 :" + ElevatData.fPhysicsMiss.ToString() + "  마법 회피율 승급상수 :" + ElevatData.fMagicMiss.ToString() 
//		           + "  캐릭터 스케일 값 승급상수 :" + ElevatData.fScale.ToString () ); 

		
		uID = Convert.ToInt32( ObjDefaultData.uID.ToString () + ObjDefaultData.nRevolutionLv.ToString () );
		OBJECT_REVOLUTION_DATA RevolData = GameDataManagerChar.Instance.GetRevolutionData (uID);
		
		/*
		Debug.Log ( "tb_CHAR_EVOLV_STATCONST - " + "  승급 레벨 아이디 :" + uID.ToString () +  "  캐릭터 승급 레벨 :" + RevolData.nLevel.ToString () + "  힘 승급상수 :" + RevolData.fStr.ToString () 
		           + "  민첩 승급상수 :" + RevolData.fDex.ToString () + "  지능 승급상수 :" + RevolData.fInte.ToString () + "  HP 승급상수:" + RevolData.fHP.ToString () + 
		           "  MP 승급상수 :" + RevolData.fMP.ToString () + "  물리 공격력 승급상수 :" + RevolData.fPhysicsAt.ToString() + "  마법 공격력 승급상수 :" + RevolData.fMagicAt.ToString() +
		           "  물리 공격 명중률 승급상수 :" + RevolData.fPhysicsHit.ToString () + "  마법 공격 명중률 승급상수 :" + RevolData.fMagicHit.ToString() + "  물리 치명타 승급상수 :"
		           + RevolData.fPhysicsCri.ToString() + "  마법 치명타 승급상수 :" + RevolData.fMagicCri.ToString () + "  물리 방어력 승급상수 :" + RevolData.fPhysicsDefens.ToString() 
		           + "  마법 저항력 승급상수 :" + RevolData.fMagicDefens.ToString() + "  전투력 승급상수 :" + RevolData.fAttack.ToString () 
		           + "  물리 회피율 승급상수 :" + RevolData.fPhysicsMiss.ToString() + "  마법 회피율 승급상수 :" + RevolData.fMagicMiss.ToString() ); 
		*/
		
		//float fValue1 = GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.STAT_ELE
		
		_ObjValueData.nStr = CharStatModification.CHAR_STAT (_ObjStatData.nStr, ObjDefaultData.nLevel, _ObjStatData.fStrValue, ElevatData.fStr, RevolData.fStr, ObjDefaultData.nElevatLv, ObjDefaultData.nRevolutionLv);
		_ObjValueData.nDex = CharStatModification.CHAR_STAT (_ObjStatData.nDex, ObjDefaultData.nLevel, _ObjStatData.fDexValue, ElevatData.fDex, RevolData.fDex, ObjDefaultData.nElevatLv, ObjDefaultData.nRevolutionLv);
		_ObjValueData.nInte = CharStatModification.CHAR_STAT (_ObjStatData.nInt, ObjDefaultData.nLevel, _ObjStatData.fIntValue, ElevatData.fInte, RevolData.fInte , ObjDefaultData.nElevatLv, ObjDefaultData.nRevolutionLv);
		
		
		_ObjValueData.nHP = CharStatModification.CHAR_HP (_ObjValueData.nStr, _ObjValueData.nDex, _ObjValueData.nInte, _ObjStatData.fHPValue, ElevatData.fHP, RevolData.fHP, ObjDefaultData.nElevatLv, ObjDefaultData.nRevolutionLv);
		_ObjValueData.nMP = CharStatModification.CHAR_MP (ObjDefaultData.nLevel);
		
		
		_ObjValueData.nPhysicsAt = CharStatModification.CHAR_OP (_ObjValueData.nStr, _ObjValueData.nDex, _ObjStatData.fPhysicsValue, ElevatData.fPhysicsAt, RevolData.fPhysicsAt, ObjDefaultData.nElevatLv, ObjDefaultData.nRevolutionLv);
		_ObjValueData.nMagicAt = CharStatModification.CHAR_OP (_ObjValueData.nInte, _ObjValueData.nDex, _ObjStatData.fMagicValue, ElevatData.fMagicAt, RevolData.fMagicAt, ObjDefaultData.nElevatLv, ObjDefaultData.nRevolutionLv);
		
		
		_ObjValueData.nPhysicsCri = CharStatModification.CHAR_CAD (_ObjValueData.nStr, _ObjValueData.nDex, _ObjStatData.fPhysicsCriValue, ElevatData.fPhysicsCri, RevolData.fPhysicsCri, ObjDefaultData.nElevatLv, ObjDefaultData.nRevolutionLv);
		_ObjValueData.nMagicCri = CharStatModification.CHAR_CAD (_ObjValueData.nInte, _ObjValueData.nDex, _ObjStatData.fMagicCriValue, ElevatData.fMagicCri, RevolData.fMagicCri, ObjDefaultData.nElevatLv, ObjDefaultData.nRevolutionLv);
		
		
		_ObjValueData.nPhysicsHit = CharStatModification.CHAR_ATT_AR (_ObjValueData.nStr, _ObjValueData.nDex, _ObjStatData.fPhysicsHitValue, ElevatData.fPhysicsHit, RevolData.fPhysicsHit, ObjDefaultData.nElevatLv, ObjDefaultData.nRevolutionLv);
		_ObjValueData.nMagicHit = CharStatModification.CHAR_ATT_AR (_ObjValueData.nInte, _ObjValueData.nDex, _ObjStatData.fMagicHitValue, ElevatData.fMagicHit, RevolData.fMagicHit, ObjDefaultData.nElevatLv, ObjDefaultData.nRevolutionLv);
		
		
		_ObjValueData.nPhysicsDefens = CharStatModification.CHAR_P_DFP (_ObjValueData.nStr, _ObjValueData.nDex, _ObjStatData.fPhysicsDefecsValue, ElevatData.fPhysicsDefens, RevolData.fPhysicsDefens, ObjDefaultData.nElevatLv, ObjDefaultData.nRevolutionLv);
		_ObjValueData.nMagicDefens = CharStatModification.CHAR_S_RESIST (_ObjValueData.nInte, _ObjStatData.fMagicDefecsValue, ElevatData.fMagicDefens, RevolData.fMagicDefens, ObjDefaultData.nElevatLv, ObjDefaultData.nRevolutionLv);
		
		
		_ObjValueData.nAtpr = CharStatModification.CHAR_ATPR (_ObjValueData.nPhysicsAt, _ObjValueData.nMagicAt, _ObjValueData.nPhysicsDefens, _ObjValueData.nMagicDefens, _ObjStatData.fPowerValue, 
		                                                      ElevatData.fAttack, RevolData.fAttack, ObjDefaultData.nElevatLv, ObjDefaultData.nRevolutionLv );
		
		_ObjValueData.nPhysicsMiss = CharStatModification.CHAR_DODGER (_ObjValueData.nStr, _ObjValueData.nDex, _ObjStatData.fPhysicsMissValue, ElevatData.fPhysicsMiss, RevolData.fPhysicsMiss, ObjDefaultData.nElevatLv, ObjDefaultData.nRevolutionLv);
		_ObjValueData.nMagicMiss = CharStatModification.CHAR_DODGER (_ObjValueData.nDex, _ObjValueData.nInte, _ObjStatData.fMagicMissValue, ElevatData.fMagicMiss, RevolData.fMagicMiss, ObjDefaultData.nElevatLv, ObjDefaultData.nRevolutionLv);
		
		
		/*
		Debug.Log ( " 수식으로 계산된스텟- " + "  힘 :" + _ObjValueData.nStr.ToString () +  "  민첩 :" + _ObjValueData.nDex.ToString () + "  지능 :" + _ObjValueData.nInte.ToString ()
		           + "  HP :" + _ObjValueData.nHP.ToString () + "  MP :" + _ObjValueData.nMP.ToString () + "  물리공격력 :" + _ObjValueData.nPhysicsAt.ToString () +
		           "  마법공격력 :" + _ObjValueData.nMagicAt.ToString () + "  물리 치명타 :" + _ObjValueData.nPhysicsCri.ToString() + "  마법치명타 :" + _ObjValueData.nMagicCri.ToString() +
		           "  물리명중률 :" + _ObjValueData.nPhysicsHit.ToString () + "  마법명중율 :" + _ObjValueData.nMagicHit.ToString() + "  물리방어력 :"
		           + _ObjValueData.nPhysicsDefens.ToString() + "  마법저항력 :" + _ObjValueData.nMagicDefens.ToString () + "  전투력 :" + _ObjValueData.nAtpr.ToString()
		           + "  물리회피율 :" + _ObjValueData.nPhysicsMiss.ToString() + "  마법회피율 :" + _ObjValueData.nMagicMiss.ToString()  );
		*/
		
		//_ObjValueData.nMoveSpeed = CharStatModification.CHAR_MOVE_SPEED (_ObjStatData.nDex, _ObjStatData.f, _ObjStatData.fMagicMissValue, ElevatData.fMagicMiss, RevolData.fMagicMiss);
		//_ObjValueData.nAttackSpeed = CharStatModification.CHAR_ATT_SPEED (_ObjStatData.nDex, _ObjStatData.nInt, _ObjStatData.fMagicMissValue, ElevatData.fMagicMiss, RevolData.fMagicMiss);
	}

	public OBJECT_VALUE_DATA ObjValueData
	{
		get
		{
			return _ObjValueData;
		}
	}
	
	public OBJECT_STAT_DATA ObjectStatData
	{
		get
		{
			return _ObjStatData;
		}
	}


}
