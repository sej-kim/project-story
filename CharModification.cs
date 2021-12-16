using UnityEngine;
using System.Collections;

public class CharModification
{
    // 물리 데미지 ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒

    
    //-----------------------------------------------------------------------------------------------------------------
    // DEFAULT_P_DMG                기본 물리 데미지 

    //	ATR_P_OP	                공격자 물리 공격력
    //	DFR_P_DFP	                피격자 물리 방어력
    //	ATR_LEV	                    공격자 레벨
    //	DFR_LEV	                    피격자 레벨
    //	DEFAULT_DMG_CONST	        기본 데미지 상수
    //	DEFAULT_DMG_BAL_CONST	    기본 데미지 차감 상수
    //	OP_PROPO_CONST	            공격력 비례 상수
    //	MAX_DEFAULT_DMG_RAND_CONST	최대 기본 데미지 난수 상수
    //	MIN_DEFAULT_DMG_RAND_CONST	최소 기본 데미지 난수 상수
    //	DEFAULT_P_DMG_RAND_CALC 	기본 물리 데미지 난수 계수

    // 기본 물리 데미지 = (공격자 레벨 - 피격자 레벨) * 공격자 레벨 / 피격자 레벨 * 기본 데미지 차감 상수  + 기본 데미지 상수 * 공격자 레벨 - (공격자 물리 공격력 + 10) * √(피격자 물리 방어력 + 50) * 공격력 비례 상수* RANDOM(최소 기본 데미지 난수 상수 ~ 최대 기본 데미지 난수 상수)
    static public int DEFAULT_P_DMG( int ATR_P_OP,
                                int DFR_P_DFP,
                                int ATR_LEV,
	                            int DFR_LEV, 
	                            float MIN_DEFAULT_DMG_RAND_CONST, 
	                            float MAX_DEFAULT_DMG_RAND_CONST, 
	                            float DEFAULT_DMG_BAL_CONST, 
	                            float DEFAULT_DMG_CONST,
	                            float OP_PROPO_CONST )
    {

        //ATR_P_OP                    = ?;	        //	공격자 물리 공격력
        //DFR_P_DFP                   = ?;	        //	피격자 물리 방어력
        //ATR_LEV                     = ?;	        //	공격자 레벨
        //DFR_LEV                     = ?;	        //	피격자 레벨
        
		/*
		Debug.Log ("ATR_P_OP : " + ATR_P_OP.ToString () + "  DFR_P_DFP : " + DFR_P_DFP.ToString () + "  ATR_LEV : " + ATR_LEV.ToString () + "  DFR_LEV : " + DFR_LEV.ToString () 
		           + "  MIN_DEFAULT_DMG_RAND_CONST : " + MIN_DEFAULT_DMG_RAND_CONST.ToString () + "  MAX_DEFAULT_DMG_RAND_CONST : " + MAX_DEFAULT_DMG_RAND_CONST.ToString () + 
		           "  DEFAULT_DMG_BAL_CONST : " + DEFAULT_DMG_BAL_CONST.ToString () + "  DEFAULT_DMG_CONST : " + DEFAULT_DMG_CONST.ToString ()  + "  OP_PROPO_CONST : " + OP_PROPO_CONST.ToString () );
		*/

		int fa = ATR_LEV - DFR_LEV;
		float fb =  ATR_LEV / DFR_LEV * DEFAULT_DMG_BAL_CONST;
		float fc = fa * fb;
		float fd = DEFAULT_DMG_CONST * ATR_LEV;
		float fe = ATR_P_OP + 10;

	float ftemp = DFR_P_DFP + 50;

		float ff = Mathf.Pow(ftemp, 0.5f);

		float fg = Random.Range(MIN_DEFAULT_DMG_RAND_CONST, MAX_DEFAULT_DMG_RAND_CONST);

		float fh = fe * ff * OP_PROPO_CONST * fg;
		
		float fResult = fc + fd - fh;

		return (int)fResult;
    }

	// 마법 데미지 ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
	
	//-----------------------------------------------------------------------------------------------------------------
	
	//  DEFAULT_S_DMG   기본 마법 데미지 
	
	//	ATR_S_OP	                공격자 마법 공격력
	//	DFR_S_RESIST	            피격자 마법 저항력
	//	ATR_LEV	                    공격자 레벨
	//	DFR_LEV	                    피격자 레벨
	//	DEFAULT_S_DMG_CONST	        기본 마법 데미지 상수
	//	DEFAULT_S_DMG_BAL_CONST	    기본 마법 데미지 차감 상수
	//	S_OP_PROPO_CONST	        마법 공격력 비례 상수
	//	MAX_DEFAULT_S_DMG_CONST	    최대 기본 마법 데미지 상수
	//	MIN_DEFAULT_S_DMG_CONST	    최소 기본 마법 데미지 상수   (ATR_LEV - DFR_LEV) * ATR_LEV / DFR_LEV * DEFAULT_DMG_BAL_CONST  + DEFAULT_DMG_CONST * ATR_LEV - (ATR_P_OP + 10) * Math.Pow(DFR_P_DFP + 50, 0.5) * OP_PROPO_CONST * DEFAULT_P_DMG_RAND_CALC
	//	DEFAULT_S_DMG_RAND	        기본 마법 데미지 난수
	//	DEFAULT_S_DMG	            기본 마법 데미지 = (공격자 레벨 - 피격자 레벨) * 공격자 레벨 / 피격자 레벨 * 기본 마법 데미지 차감 상수  + 기본 마법 데미지 상수 * 공격자 레벨 - (공격자 마법 공격력 + 10) * √(피격자 마법 저항력 + 50) * 마법 공격력 비례 상수* RANDOM(최소 기본 마법 데미지 상수 ~ 최대 기본 마법 데미지 상수)
	
	
	static public int DEFAULT_S_DMG(int ATR_S_OP,
	                                  int DFR_S_RESIST,
	                                  int ATR_LEV,
	                                  int DFR_LEV, 
	                                  float MIN_DEFAULT_S_DMG_CONST,
	                                  float MAX_DEFAULT_S_DMG_CONST, 
	                                  float DEFAULT_S_DMG_BAL_CONST, 
	                                  float DEFAULT_S_DMG_CONST, 
	                                  float S_OP_PROPO_CONST)
	{
		
		//	ATR_S_OP	            =		;	//	공격자 마법 공격력
		//	DFR_S_RESIST	        =		;	//	피격자 마법 저항력
		//	ATR_LEV	                =		;	//	공격자 레벨
		//	DFR_LEV	                =		;	//	피격자 레벨
		float	DEFAULT_S_DMG_RAND	=	Random.Range(MIN_DEFAULT_S_DMG_CONST , MAX_DEFAULT_S_DMG_CONST)	;	//	기본 마법 데미지 난수
		//	DEFAULT_S_DMG	        =	(수식에 의한 결과 값)	;	//	기본 마법 데미지 
		
		
		//float fResult = (ATR_LEV - DFR_LEV) * ATR_LEV / DFR_LEV * DEFAULT_S_DMG_BAL_CONST + DEFAULT_S_DMG_CONST * ATR_LEV - (ATR_S_OP + 10) * Mathf.Pow(DFR_S_RESIST + 50, 0.5) * S_OP_PROPO_CONST * DEFAULT_S_DMG_RAND;
		
		return (int)DEFAULT_S_DMG_RAND;
	}

    //-----------------------------------------------------------------------------------------------------------------


	//  ATT_EVA_SUCC_DECID_RESULT  회피 성공 판단 결과
	//  [물리 회피율]
    //	DFR_DODGER	                	피격자 물리 회피율	            tb_Char_Status_Data + 착용 아이템 옵션 + 버프 + 디버프
    //	ATR_OP	                    공격자 물리 공격력	            tb_Char_Status_Data + 착용 아이템 옵션 + 버프 + 디버프
    //	DFR_LEV	                        피격자 레벨	                    tb_Char_Status_Data
    //	ATR_LEV	                        공격자 레벨	                    tb_Char_Status_Data
	//	ATT_EVA_SUCC_MAGNIF	        물리 공격 회피 성공 배율	    19
	//  -----------------------------------------------------------------------------------------------------------------
	//  -----------------------------------------------------------------------------------------------------------------
	//  [마법 회피율]
	//	DFR_DODGER	        피격자 마법 회피율	
	//	ATR_OP	            공격자 마법 공격력	
	//	DFR_LEV	                피격자 레벨	
	//	ATR_LEV	                공격자 레벨	
	//  ATT_EVA_SUCC_MAGNIF	    마법 회피 판단 난수
	//  -----------------------------------------------------------------------------------------------------------------
	//	DFR_DODGER / ATR_OP * DFR_LEV / ATR_LEV * ATT_EVA_SUCC_MAGNIF  (소수점 아래 버림)

	static public bool ATT_EVA_SUCC_DECID_RESULT(int DFR_DODGER,
                                            int ATR_OP,
                                            int DFR_LEV,
                                            int ATR_LEV, 
	                                        float ATT_EVA_SUCC_MAGNIF)
    {


        //	DFR_DODGER	            =	?	;	                //	피격자 회피율
        //	ATR_OP	                =	?	;	                //	공격자 공격력
        //	DFR_LEV	                    =	?	;	                //	피격자 레벨
        //	ATR_LEV	                    =	?	;	                //	공격자 레벨
        //	ATT_EVA_SUCC_CALC	        =	계산 수식 결과	;	    // 공격 회피 성공 계수
        //	ATT_EVA_SUCC_MAGNIF	        공격 회피 성공 배율	    19

        int P_ATT_EVA_SUCC_CALC = (int)(DFR_DODGER / ATR_OP * DFR_LEV / ATR_LEV * ATT_EVA_SUCC_MAGNIF); // 소수점 아래 버림

        // 회피 성공 판단 
        // RANDOM (1 ~ 100) < P_ATT_EVA_SUCC_CALC : 회피 성공 : fResult = 1;
        // RANDOM (1 ~ 100) >= P_ATT_EVA_SUCC_CALC : 회피 실패 : fResult = 0;

		return Random.Range (1, 100) < P_ATT_EVA_SUCC_CALC;
    }

	//-----------------------------------------------------------------------------------------------------------------
	

	//-----------------------------------------------------------------------------------------------------------------
	// CRITI_BEGIN_DECID_RESULT     물리 크리티컬 발동 판단 결과
	//  [물리 크리티컬 발동 여부]
	//	ATR_LEV	                    공격자 레벨
	//	DFR_LEV	                    피격자 레벨
	//	ATR_CAD	                공격자 물리 치명타
	//	ATR_AR	                공격자 물리 명중률
	//	DFR_DFP	                피격자 물리 방어력
	//	CRITI_DECID_LEV_CONST		물리 크리티컬 판단 레벨 상수
	//	CRITI_DECID_STAT_CONST	물리 크리티컬 판단 스텟 상수
	//	공격자 레벨 / 피격자 레벨 * 물리 크리티컬 판단 레벨 상수 + (공격자 물리 명중률 + 공격자 물리 치명타) * 물리 크리티컬 판단 스텟 상수 / 피격자 물리 방어력
	//-----------------------------------------------------------------------------------------------------------------

	//-----------------------------------------------------------------------------------------------------------------
	//  [마법 크리티컬 발동 여부]
	//	ATR_LEV	                    공격자 레벨
	//	DFR_LEV	                    피격자 레벨
	//	ATR_CAD	                공격자 마법 치명타
	//	ATR_AR	                공격자 마법 명중률
	//	DFR_DFP	                피격자 마법 방어력
	//	CRITI_DECID_LEV_CONST		마법 크리티컬 판단 레벨 상수
	//	CRITI_DECID_STAT_CONST	마법 크리티컬 판단 스텟 상수
	//	공격자 레벨 / 피격자 레벨 * 마법 크리티컬 판단 레벨 상수 + (공격자 마법 명중률 + 공격자 마법 치명타) * 마법 크리티컬 판단 스텟 상수 / 피격자 마법 저항력
	//-----------------------------------------------------------------------------------------------------------------
	static public bool CRITI_BEGIN_DECID_RESULT(  int ATR_LEV,
                                            int DFR_LEV,
                                            int ATR_CAD,
                                            int ATR_AR,
	                                        int DFR_DFP, 
	                                        float CRITI_DECID_LEV_CONST, 
	                                        float CRITI_DECID_STAT_CONST )
    {

        //	ATR_LEV	                            =	?	;	        //	공격자 레벨
        //	DFR_LEV	                            =	?	;	        //	피격자 레벨
        //	ATR_P_CAD	                        =	?	;	        //	공격자 물리 치명타
        //	ATR_P_AR	                        =	?	;	        //	공격자 물리 명중률
        //	DFR_P_DFP	                        =	?	;	        //	피격자 물리 방어력
        //	P_CRITI_BEGIN_DECID_CALC	        =	수식에 의한 계산 결과 값	    ;	//	물리 크리티컬 발동 판단 계수
        //	P_CRITI_BEGIN_DECID_RESULT	        =	조건 수식에 의한 최종 결과 값	;	//	물리 크리티컬 발동 판단 결과


        // P_CRITI_BEGIN_DECID_CALC	물리 크리티컬 발동 판단 계수
        int P_CRITI_BEGIN_DECID_CALC = (int)(ATR_LEV / DFR_LEV * CRITI_DECID_LEV_CONST + (ATR_AR + ATR_CAD) * CRITI_DECID_STAT_CONST / DFR_DFP);

        // if P_ATT_EVA_SUCC_DECID_RESULT =	회피 실패 then // 아래 조건식을 통해서 CRITI_BEGIN_DECID_CALC 값을 결정
        // if CRITI_BEGIN_DECID_CALC > 2 then	        CRITI_BEGIN_DECID_CALC = P_CRITI_MAX_FRIQ_RAT
        // if 1 =< CRITI_BEGIN_DECID_CALC < 2 then      CRITI_BEGIN_DECID_CALC = P_CRITI_HIGH_FRIQ_RAT
        // if 0.7 =< CRITI_BEGIN_DECID_CALC < 1 then	CRITI_BEGIN_DECID_CALC = P_CRITI_NORMAL_FRIQ_RAT
        // if 0.3=< CRITI_BEGIN_DECID_CALC < 0.7 then	CRITI_BEGIN_DECID_CALC = P_CRITI_LOW_FRIQ_RAT
        // if CRITI_BEGIN_DECID_CALC< 0.3 then	        CRITI_BEGIN_DECID_CALC = P_CRITI_MIN_FRIQ_RAT

        // Random(1 ~ 100) 의 결과 값이 CRITI_BEGIN_DECID_CALC 값 보다 작다면 
        //              P_CRITI_BEGIN_DECID_RESULT (물리 크리티컬 발동 판단 결과) 는 크리티컬 데미지 발생
        // Random(1 ~ 100) 의 결과 값이 CRITI_BEGIN_DECID_CALC 값 보다 크거나 같다면 
        //              P_CRITI_BEGIN_DECID_RESULT (물리 크리티컬 발동 판단 결과) 는 크리티컬 데미지 발생 안함

		//bool b

		return Random.Range( 1, 100 ) < P_CRITI_BEGIN_DECID_CALC;
    }

	//-----------------------------------------------------------------------------------------------------------------
	


    //-----------------------------------------------------------------------------------------------------------------
    // ATT_CRITI_DMG                크리티컬 데미지 

    //	CRITI_DMG_STAT_CONST	    물리 크리티컬 데미지 스텟 상수	0.5
    //	CRITI_DMG_LEV_CONST	    물리 크리티컬 데미지 레벨 상수	0.3
    //	CRITI_DMG_MAX_RANGE_CONST	물리 크리티컬 데미지 최대 범위 상수	0.5
    //  CRITI_DMG_MIN_RANGE_CONST	물리 크리티컬 데미지 최소 범위 상수	0.2
    //	DEFAULT_DMG	            기본 물리 데미지	
    //	DFR_DFP	                피격자 물리 방어력	
    //	ATR_CAD	                공격자 물리 치명타	
    //	ATR_AR	                공격자 물리 명중률	
    //	DFR_LEV	                    피격자 레벨	
    //	ATR_LEV	                    공격자 레벨	
    //	CRITI_DMG_DECR_CALC	    물리 크리티컬 데미지 감소 계수	 = 피격자 물리 방어력 /  (공격자 물리 치명타 + 공격자 물리 명중률) * 물리 크리티컬 데미지 스텟 상수 + (피격자 레벨 / 공격자 레벨) * 물리 크리티컬 데미지 레벨 상수
    //	ATT_CRITI_DMG	            물리 공격 크리티컬 데미지	 = (기본 물리 데미지 - (기본 물리 데미지 * 물리 크리티컬 데미지 감소 계수)) * RANDOM(물리 크리티컬 데미지 최소 범위 상수 ~ 물리 크리티컬 데미지 최대 범위 상수)
	//-----------------------------------------------------------------------------------------------------------------

	//-----------------------------------------------------------------------------------------------------------------
	//	CRITI_DMG_STAT_CONST	마법 크리티컬 데미지 스텟 상수	
	//	CRITI_DMG_LEV_CONST	마법 크리티컬 데미지 레벨 상수	
	//	CRITI_MAX_RANGE_CONST	마법 크리티컬 최대 범위 상수	
	//	CRITI_MIN_RANGE_CONST	마법 크리티컬 최소 범위 상수	
	//	DEFAULT_S_DMG	        기본 마법 데미지	
	//	DFR_DFP		        피격자 마법 저항력	
	//	ATR_CAD	            공격자 마법 치명타	
	//	ATR_AR	            공격자 마법 명중률	
	//	DFR_LEV	                피격자 레벨	
	//	ATR_LEV	                공격자 레벨	
	//	CRITI_DMG_RAND	    마법 크리티컬 데미지 난수	Random( 마법 크리티컬 최소 범위 상수 ~ 마법 크리티컬 최대 범위 상수 )
	//	CRITI_DECR_CALC	    마법 크리티컬 감소 계수	 = 피격자 마법 저항력 /  (공격자 마법 치명타 + 공격자 마법 명중률) * 마법 크리티컬 데미지 스텟 상수 + (피격자 레벨 / 공격자 레벨) * 마법 크리티컬 데미지 레벨 상수
	//	CRITI_DMG	            마법 크리티컬 데미지	 = (기본 마법 데미지 - (기본 마법 데미지 * 마법 크리티컬 감소 계수)) * RANDOM(마법 크리티컬 최소 범위 상수 ~ 마법 크리티컬 최대 범위 상수)
	//-----------------------------------------------------------------------------------------------------------------
	static public int ATT_CRITI_DMG(   int DFR_DFP,
                                    int ATR_CAD,
                                    int ATR_AR,
                                    int DFR_LEV,
                                    int ATR_LEV, 
	                             float DEFAULT_P_DMG,
	                             float CRITI_DMG_STAT_CONST, 
	                             float CRITI_DMG_LEV_CONST,
	                             float CRITI_DMG_MIN_RANGE_CONST, 
	                             float CRITI_DMG_MAX_RANGE_CONST )
    {

        
        //	DEFAULT_P_DMG	                =	기본 물리 데미지 산출 펑션 결과 값	;	//	기본 물리 데미지
        //	DFR_P_DFP	                    =	?	;	                            //	피격자 물리 방어력
        //	ATR_P_CAD	                    =	?	;	                            //	공격자 물리 치명타
        //	ATR_P_AR	                    =	?	;	                            //	공격자 물리 명중률
        //	DFR_LEV	                        =	?	;	                            //	피격자 레벨
        //	ATR_LEV	                        =	?	;	                            //	공격자 레벨
        //	P_CRITI_DMG_DECR_CALC	        =	수식에 의한 계산 결과 값	;	    //	물리 크리티컬 데미지 감소 계수
        float P_CRITI_DMG_DECR_CALC = DFR_DFP /  (ATR_CAD + ATR_AR) * CRITI_DMG_STAT_CONST + (DFR_LEV / ATR_LEV) * CRITI_DMG_LEV_CONST;

        //	P_ATT_CRITI_DMG	                =	수식에 의한 계산 결과 값	;	    //	물리 공격 크리티컬 데미지
        float P_ATT_CRITI_DMG = (DEFAULT_P_DMG - (DEFAULT_P_DMG * P_CRITI_DMG_DECR_CALC)) * Random.Range(CRITI_DMG_MIN_RANGE_CONST , CRITI_DMG_MAX_RANGE_CONST); // 소수점 아래 반올림

		return (int)P_ATT_CRITI_DMG;
    }

    //-----------------------------------------------------------------------------------------------------------------


	//-----------------------------------------------------------------------------------------------------------------
    // DFP_THR_SUCC_DECID     방어력 관통 성공 판단

    //	ATR_AR	                공격자 물리 명중률	
    //	ATR_CAD	                공격자 물리 치명타	
    //	DFR_AR	                방어자 물리 명중률	
    //	DFR_CAD	                방어자 물리 치명타	
    //	ATR_DFP_THRR	            공격자 물리 방어력 관통률	
    //	DFR_DFP_THRR	            방어자 물리 방어력 관통률	
    //	DFP_THRR_PROPO_CONST	    물리 방어력 관통률 비례 상수	
    //	DFP_IGN_INC_CONST	        물리 방어력 무시 증가 상수	
    //  DFP_THR_MAX_RAT	        물리 방어력 관통 최대 확율	
    //	DFP_THR_CONDI_CALC	    물리 방어력 관통 조건 계수	= 공격자 물리 명중률 / 방어자 물리 명중률 * 공격자 물리 치명타 / 방어자 물리 치명타 * 물리 방어력 무시 증가 상수 + 공격자 물리 방어력 관통률 / 방어자 물리 방어력 관통률 * 물리 방어력 관통률 비례 상수
    //	DFP_THR_RAND	            물리 방어력 관통 난수	    = Random(1 ~ 100)
    //	DFP_THR_SUCC_DECID	    물리 방어력 관통 성공 판단	
	//-----------------------------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------------------------
	//	ATR_AR	                공격자 마법 명중률
	//	ATR_CAD	                공격자 마법 치명타
	//	DFR_AR	                방어자 마법 명중률
	//	DFR_CAD	                방어자 마법 치명타
	//	ATR_DFP_THRR	            공격자 마법 방어력 관통률
	//	DFR_DFP_THRR	            방어자 마법 방어력 관통률
	//	DFP_THRR_PROPO_CONST	    마법 방어력 관통률 비례 상수
	//	DFP_IGN_INC_CONST	        마법 방어력 무시 증가 상수
	//  DFP_THR_MAX_RAT	        마법 방어력 관통 최대 확율
	//	DFP_THR_CONDI_CALC	    마법 방어력 관통 조건 계수	= 공격자 마법 명중률 / 방어자 마법 명중률 * 공격자 마법 치명타 / 방어자 마법 치명타 * 마법 방어력 무시 증가 상수 + 공격자 마법 방어력 관통률 / 방어자 마법 방어력 관통률 * 마법 방어력 관통률 비례 상수
	//	DFP_THR_RAND	            마법 방어력 관통 난수	    = Random(1 ~ 100)
	//	DFP_THR_SUCC_DECID	    마법 방어력 관통 성공 판단
	//-----------------------------------------------------------------------------------------------------------------
	static public bool DFP_THR_SUCC_DECID(int ATR_AR,
                                        int ATR_CAD,
                                        int DFR_AR,
                                        int DFR_CAD,
                                        float ATR_DFP_THRR,
                                        float DFR_DFP_THRR,
	                                float DFP_IGN_INC_CONST, 
	                                float DFP_THRR_PROPO_CONST )

    {

        //	ATR_P_AR	                =		;	                        //	공격자 물리 명중률	
        //	ATR_P_CAD	                =		;	                        //	공격자 물리 치명타	
        //	DFR_P_AR	                =		;	                        //	방어자 물리 명중률	
        //	DFR_P_CAD	                =		;	                        //	방어자 물리 치명타	
        //	ATR_P_DFP_THRR	            =		;	                        //	공격자 물리 방어력 관통률	
        //	DFR_P_DFP_THRR	            =		;	                        //	방어자 물리 방어력 관통률	
        //	P_DFP_THR_CONDI_CALC	    =	수식에 의한 계산 결과 값	;	//	물리 방어력 관통 조건 계수= 공격자 물리 명중률 / 방어자 물리 명중률 * 공격자 물리 치명타 / 방어자 물리 치명타 * 물리 방어력 무시 증가 상수 + 공격자 물리 방어력 관통률 / 방어자 물리 방어력 관통률 * 물리 방어력 관통률 비례 상수 
        //	P_DFP_THR_SUCC_DECID	    =	비교 연산에 의한 결과 값	;	//	물리 방어력 관통 성공 판단	
		//float P_DFP_THR_RAND = Random.Range (1, 100);
        int P_DFP_THR_CONDI_CALC = (int)(ATR_AR / DFR_AR * ATR_CAD / DFR_CAD * DFP_IGN_INC_CONST + ATR_DFP_THRR / DFR_DFP_THRR * DFP_THRR_PROPO_CONST);
        
            //  물리 방어력 관통 성공 판단	
            //  if P_DFP_THR_CONDI_CALC < P_DFP_THR_MAX_RAT;   
            //       if P_DFP_THR_RAND < P_DFP_THR_CONDI_CALC;  크리티컬 데미지 발동
            //       if P_DFP_THR_RAND >= P_DFP_THR_CONDI_CALC;  크리티컬 데미지 미발동
            //  if P_DFP_THR_CONDI_CALC >= P_DFP_THR_MAX_RAT;   
            //       if P_DFP_THR_RAND < P_DFP_THR_MAX_RAT;  크리티컬 데미지 발동
            //       if P_DFP_THR_RAND >= P_DFP_THR_MAX_RAT;  크리티컬 데미지 미발동
		return Random.Range( 1, 100 ) < P_DFP_THR_CONDI_CALC;
    }

	//-----------------------------------------------------------------------------------------------------------------
    //P_DFP_THR_DMG      방어력 관통 데미지 

    //	DFR_DFP	            피격자 물리 방어력	
    //	DEFAULT_DMG_CONST	    기본 데미지 상수	
    //	MAX_THR_DMG_CONST	    최대 물리 관통 데미지 상수	
    //	MIN_THR_DMG_CONST	    최소 물리 관통 데미지 상수	
    //	DFP_THR_DMG	        물리 방어력 관통 데미지	 = 피격자 물리 방어력 * 기본 데미지 상수 * RANDOM(최소 물리 관통 데미지 상수 ~ 최대 물리 관통 데미지 상수)
	//-----------------------------------------------------------------------------------------------------------------
	//-----------------------------------------------------------------------------------------------------------------

	//	DFR_DFP	피격자 마법 저항력	
	//	DEFAULT_DMG_CONST	기본 마법 데미지 상수	
	//	MAX_THR_DMG_CONST	최대 마법 저항 관통 난수	
	//	MIN_THR_DMG_CONST	최소 마법 저항 관통 난수	
	//	DFP_THR_DMG	마법 저항력 관통 데미지	마법 저항력 관통 데미지 = 피격자 마법 저항력 * 기본 마법 데미지 상수 * RANDOM(최소 마법 저항 관통 난수 ~ 최대 마법 저항 관통 난수)
	//	RESIST_THR_DMG_RAND	마법 저항력 관통 데미지 난수	 = RANDOM ( 최소 마법 저항 관통 난수 ~ 최대 마법 저항 관통 난수)
	//-----------------------------------------------------------------------------------------------------------------
	static public int DFP_THR_DMG(int DFR_DFP, float MIN_RESIST_THR_RAND, float MAX_RESIST_THR_RAND, float DEFAULT_DMG_CONST)

    {

        //	DFR_P_DFP	            =		    ;	//	피격자 물리 방어력	
        //	P_DFP_THR_DMG	        =		    ;	//	물리 방어력 관통 데미지 = 피격자 물리 방어력 * 기본 데미지 상수 * RANDOM(최소 물리 관통 데미지 상수 ~ 최대 물리 관통 데미지 상수)
        
		int nResult = (int)(DFR_DFP * DEFAULT_DMG_CONST * Random.Range(MIN_RESIST_THR_RAND , MAX_RESIST_THR_RAND)); // 소수점 아래 반올림
		return nResult;
    }
}
