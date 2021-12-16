using UnityEngine;
using System.Collections;
using STORY_ENUM;
using STORY_GAMEDATA;

public class DamageModification : MonoBehaviour {

	// Use this for initialization
	public bool IsAttackMiss ( eDAMAGE_TYPE dType, Character Attacker, Character Defender )
	{
		bool bMiss 					= false;
		
		float ATT_EVA_SUCC_MAGNIF	= 0;
		int nDescMissValue			= 0;
		int nSrcAttack				= 0;
		
		if (dType == eDAMAGE_TYPE.Physical)
		{
			ATT_EVA_SUCC_MAGNIF 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.P_ATT_EVA_SUCC_MAGNIF );
			nDescMissValue 			= Defender.ObjValueData.nPhysicsMiss;
			nSrcAttack 				= Attacker.ObjValueData.nPhysicsAt;
		} 
		else 
		{
			ATT_EVA_SUCC_MAGNIF 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.S_ATT_EVA_SUCC_MAGNIF );
			nDescMissValue 			= Defender.ObjValueData.nMagicMiss;
			nSrcAttack 				= Attacker.ObjValueData.nMagicAt;
		}
		
		bMiss = CharModification.ATT_EVA_SUCC_DECID_RESULT (nDescMissValue, nSrcAttack, Defender.ObjDefaultData.nLevel, Attacker.ObjDefaultData.nLevel, ATT_EVA_SUCC_MAGNIF);
		
		if (bMiss) 
		{
			Debug.Log ( "uID  " + Defender.ObjDefaultData.uID.ToString() + "    MISS" );
		}
		
		return bMiss;
	}

	public bool IsCriDamage( eDAMAGE_TYPE dType, Character Attacker, Character Defender )
	{
		bool bCri 					= false;
		
		float CRITI_DECID_LEV_CONST	 = 0;
		float CRITI_DECID_STAT_CONST = 0;
		
		
		int nScrCri					= 0;
		int nScrHit 				= 0;
		int nDescDefence			= 0;
		
		if (dType == eDAMAGE_TYPE.Physical)
		{
			CRITI_DECID_LEV_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.P_CRITI_DECID_LEV_CONST );
			CRITI_DECID_STAT_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.P_CRITI_DECID_STAT_CONST );
			
			nScrCri 				= Attacker.ObjValueData.nPhysicsCri;
			nScrHit 				= Attacker.ObjValueData.nPhysicsHit;
			nDescDefence 			= Defender.ObjValueData.nPhysicsMiss;
		} 
		else 
		{
			CRITI_DECID_LEV_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.S_CRITI_DECID_LEV_CONST );
			CRITI_DECID_STAT_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.S_CRITI_DECID_STAT_CONST );
			
			nScrCri 				= Attacker.ObjValueData.nMagicCri;
			nScrHit 				= Attacker.ObjValueData.nMagicHit;
			nDescDefence 			= Defender.ObjValueData.nMagicMiss;
		}
		
		
		
		
		bCri = CharModification.CRITI_BEGIN_DECID_RESULT (Attacker.ObjDefaultData.nLevel, Defender.ObjDefaultData.nLevel, nScrCri, nScrHit, nDescDefence, CRITI_DECID_LEV_CONST, CRITI_DECID_STAT_CONST);
		
		return bCri;
	}

	public int GetCriDamage( eDAMAGE_TYPE dType, Character Attacker, Character Defender )
	{
		if (IsCriDamage (dType, Attacker, Defender)) 
		{
			return 0;
		}
		
		
		int nResult 				 = 0;
		
		float CRITI_DMG_STAT_CONST	 		= 0;
		float CRITI_DMG_LEV_CONST 			= 0;
		float CRITI_DMG_MIN_RANGE_CONST	 	= 0;
		float CRITI_DMG_MAX_RANGE_CONST 	= 0;
		
		int nDescDefence			= 0;
		int nScrCri					= 0;
		int nScrHit 				= 0;
		float fDamage					= 0;
		
		
		if (dType == eDAMAGE_TYPE.Physical)
		{
			CRITI_DMG_STAT_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.P_CRITI_DMG_STAT_CONST );
			CRITI_DMG_LEV_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.P_CRITI_DMG_LEV_CONST );
			CRITI_DMG_MIN_RANGE_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.P_CRITI_DMG_MIN_RANGE_CONST );
			CRITI_DMG_MAX_RANGE_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.P_CRITI_DMG_MAX_RANGE_CONST );
			
			nScrCri 				= Attacker.ObjValueData.nPhysicsCri;
			nScrHit 				= Attacker.ObjValueData.nPhysicsHit;
			nDescDefence 			= Defender.ObjValueData.nPhysicsMiss;
			fDamage					= (float)Attacker.ObjValueData.nPhysicsAt;
		} 
		else 
		{
			CRITI_DMG_STAT_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.S_CRITI_DMG_STAT_CONST );
			CRITI_DMG_LEV_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.S_CRITI_DMG_LEV_CONST );
			CRITI_DMG_MIN_RANGE_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.S_CRITI_MIN_RANGE_CONST );
			CRITI_DMG_MAX_RANGE_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.S_CRITI_MAX_RANGE_CONST );
			
			nScrCri 				= Attacker.ObjValueData.nMagicCri;
			nScrHit 				= Attacker.ObjValueData.nMagicHit;
			nDescDefence 			= Defender.ObjValueData.nMagicMiss;
			fDamage					= (float)Attacker.ObjValueData.nMagicAt;
		}
		
		nResult = CharModification.ATT_CRITI_DMG (nDescDefence, nScrCri, nScrHit, Defender.ObjDefaultData.nLevel, Attacker.ObjDefaultData.nLevel, fDamage, 
		                                          CRITI_DMG_STAT_CONST, CRITI_DMG_LEV_CONST, CRITI_DMG_MIN_RANGE_CONST, CRITI_DMG_MAX_RANGE_CONST);
		
		return nResult;
	}


	public bool IsHitDamage( eDAMAGE_TYPE dType, Character Attacker, Character Defender )
	{
		bool bHit 					= false;
		
		float DFP_IGN_INC_CONST	 	= 0;
		float DFP_THRR_PROPO_CONST 	= 0;
		
		int nScrHit 				= 0;
		int nScrCri					= 0;
		
		int nDescHit 				= 0;
		int nDescCri				= 0;
		
		if (dType == eDAMAGE_TYPE.Physical)
		{
			DFP_IGN_INC_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.P_DFP_IGN_INC_CONST );
			DFP_THRR_PROPO_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.P_DFP_THRR_PROPO_CONST );
			
			nScrHit 				= Attacker.ObjValueData.nPhysicsCri;
			nScrCri 				= Attacker.ObjValueData.nPhysicsHit;
			nDescHit 				= Defender.ObjValueData.nPhysicsHit;
			nDescCri 				= Defender.ObjValueData.nPhysicsCri;
		} 
		else 
		{
			DFP_IGN_INC_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.S_RESIST_THRR_INC_CONST );
			DFP_THRR_PROPO_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.S_RESIST_THRR_PROPO_CONST );
			
			nScrHit 				= Attacker.ObjValueData.nMagicHit;
			nScrCri 				= Attacker.ObjValueData.nMagicCri;
			nDescHit 				= Defender.ObjValueData.nMagicHit;
			nDescCri 				= Defender.ObjValueData.nMagicCri;
		}
		
		bHit = CharModification.DFP_THR_SUCC_DECID (nScrHit, nScrCri, nDescHit, nDescCri, 0.01f, 0.01f, DFP_IGN_INC_CONST, DFP_THRR_PROPO_CONST);
		
		return bHit;
	}
	
	public int GetHitDamage( eDAMAGE_TYPE dType, Character Attacker, Character Defender )
	{
		if (IsHitDamage (dType, Attacker, Defender)) 
		{
			return 0;
		}
		
		int nDamage = 0;
		
		float MIN_RESIST_THR_RAND 	= 0;
		float MAX_RESIST_THR_RAND 	= 0;
		float DEFAULT_DMG_CONST 		= 0;
		int nDefence = 0;
		
		if (dType == eDAMAGE_TYPE.Physical) 
		{
			MIN_RESIST_THR_RAND 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.MIN_P_THR_DMG_CONST );
			MAX_RESIST_THR_RAND 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.MAX_P_THR_DMG_CONST );
			DEFAULT_DMG_CONST 		= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.DEFAULT_DMG_CONST );
			
			nDefence = Defender.ObjValueData.nPhysicsDefens;
		} 
		else 
		{
			MIN_RESIST_THR_RAND 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.MIN_S_RESIST_THR_RAND );
			MAX_RESIST_THR_RAND 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.MAX_S_RESIST_THR_RAND );
			DEFAULT_DMG_CONST 		= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.DEFAULT_S_DMG_CONST );
			
			nDefence = Defender.ObjValueData.nMagicDefens;
		}
		
		nDamage = (int)CharModification.DFP_THR_DMG( nDefence, MIN_RESIST_THR_RAND, MAX_RESIST_THR_RAND, DEFAULT_DMG_CONST );
		
		//Debug.Log ( "uID" + this.ObjDefaultData.uID.ToString() +  "  defence   "  + target.ObjValueData.nPhysicsDefens.ToString() + " De : " + DEFAULT_DMG_CONST.ToString ());
		//Debug.Log ( "hitDamage : " + nDamage.ToString());
		
		return nDamage;
	}

	public int GetDefaultDamage( eDAMAGE_TYPE dType, Character Attacker, Character Defender )
	{
		int nDamage = 0;
		
		float MIN_DEFAULT_DMG_RAND_CONST 	= 0;
		float MAX_DEFAULT_DMG_RAND_CONST 	= 0;
		float DEFAULT_DMG_BAL_CONST 		= 0;
		float DEFAULT_DMG_CONST 			= 0;
		float OP_PROPO_CONST 				= 0;
		
		
		if (dType == eDAMAGE_TYPE.Physical) 
		{
			MIN_DEFAULT_DMG_RAND_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.MIN_DEFAULT_DMG_RAND_CONST );
			MAX_DEFAULT_DMG_RAND_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.MAX_DEFAULT_DMG_RAND_CONST );
			DEFAULT_DMG_BAL_CONST 		= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.DEFAULT_DMG_BAL_CONST );
			DEFAULT_DMG_CONST 			= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.DEFAULT_DMG_CONST );
			OP_PROPO_CONST 				= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.OP_PROPO_CONST );
			
			nDamage = (int)CharModification.DEFAULT_P_DMG( Attacker.ObjValueData.nPhysicsAt, Defender.ObjValueData.nPhysicsDefens, 
			                                              Attacker.ObjDefaultData.nLevel, Defender.ObjDefaultData.nLevel, MIN_DEFAULT_DMG_RAND_CONST, 
			                                              MAX_DEFAULT_DMG_RAND_CONST, DEFAULT_DMG_BAL_CONST, DEFAULT_DMG_CONST, OP_PROPO_CONST );
			
			//Debug.Log ( "uID" + this.ObjDefaultData.uID.ToString() +  "Phy  "  + target.ObjValueData.nPhysicsDefens.ToString() + " De : " + DEFAULT_DMG_CONST.ToString ());
		} 
		else 
		{
			MIN_DEFAULT_DMG_RAND_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.MIN_DEFAULT_S_DMG_CONST );
			MAX_DEFAULT_DMG_RAND_CONST 	= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.MAX_DEFAULT_S_DMG_CONST );
			DEFAULT_DMG_BAL_CONST 		= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.DEFAULT_S_DMG_BAL_CONST );
			DEFAULT_DMG_CONST 			= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.DEFAULT_S_DMG_CONST );
			OP_PROPO_CONST 				= GameDataManagerChar.Instance.GetDamageConstData( eDAMAGE_CONST.S_OP_PROPO_CONST );
			
			nDamage = (int)CharModification.DEFAULT_S_DMG( Attacker.ObjValueData.nMagicAt, Defender.ObjValueData.nMagicDefens, 
			                                              Attacker.ObjDefaultData.nLevel, Defender.ObjDefaultData.nLevel, MIN_DEFAULT_DMG_RAND_CONST, 
			                                              MAX_DEFAULT_DMG_RAND_CONST, DEFAULT_DMG_BAL_CONST, DEFAULT_DMG_CONST, OP_PROPO_CONST );
		}
		
		
		
		//Debug.Log( "SrcLv : " + this.ObjDefaultData.nLevel.ToString() + "  DescLv : " + target.ObjDefaultData.nLevel + "srcCri"
		
		
		return nDamage;
	}

}