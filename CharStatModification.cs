using UnityEngine;
using System.Collections;

public class CharStatModification
{
    // 캐릭터 현재 스텟 산출  ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
	// CHAR_STAT(힘, 민첩, 지능 ) -----------------------------------------------------------------------------------------------------------------
	
	//	[한글 칼럼명]	            [영문 칼럼 명 (변수명)]	    [테이블] 혹은 데이터 출처
	//	캐릭터 기본 스텟	        CHAR_DEFAULT_STAT	        tb_CHAR_DEFAULT_STAT
	//	캐릭터 기본 레벨	        CHAR_DEFAULT_LEV	        tb_CHAR
	//	캐릭터 힘 비례상수	        CHAR_STR_PROPOCONST	        tb_CHAR_DEFAULT_STAT
	//	STAT 승급상수				STAT_ELEVATCONST			tb_CHAR_ELEVAT_STATCONST
	//	STAT 진화상수				STAT_EVOLVCONST				tb_CHAR_EVOLV_STATCONST

    //  항목 추가▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
    //  캐릭터 기본 승급 레벨       CHAR_DEFAULT_ELEVAT_LEV     tb_CHAR
    //  캐릭터 기본 진화 레벨       CHAR_DEFAULT_EVOLV_LEV      tb_CHAR
    //  ------------------------------------------------------------------

	//<수정전>	캐릭터 STAT = (캐릭터 기본 STAT + 캐릭터 기본 레벨  * 캐릭터 STAT 비례상수 * 0.5) * (1 + STAT 승급상수 + STAT 진화상수)
	//<수정전>	CHAR_STAT = (CHAR_DEFAULT_STAT + CHAR_DEFAULT_LEV  * CHAR_STAT_PROPOCONST * 0.5) * (1 + STAT_ELEVATCONST + STAT_EVOLVCONST)

    //<수정후> (캐릭터 기본 스텟 + 캐릭터 기본 레벨 * 캐릭터 스텟 비례상수 * 0.5) * (1 + 캐릭터 기본 승급 레벨 / 스텟 승급상수 + 캐릭터 기본 진화 레벨 / 스텟 진화상수)
    //<수정후> (CHAR_DEFAULT_STAT + CHAR_DEFAULT_LEV  * CHAR_STAT_PROPOCONST * 0.5) * (1 + CHAR_DEFAULT_ELEVAT_LEV / STAT_ELEVATCONST + CHAR_DEFAULT_EVOLV_LEV / STAT_EVOLVCONST)
    
	static public int CHAR_STAT(float CHAR_DEFAULT_STAT,
	                            float CHAR_DEFAULT_LEV,
	                            float CHAR_STAT_PROPOCONST,
	                            float STAT_ELEVATCONST,
	                            float STAT_EVOLVCONST,
                                int CHAR_DEFAULT_ELEVAT_LEV,
                                int CHAR_DEFAULT_EVOLV_LEV)
    {

		float fa = (float)((float)CHAR_DEFAULT_LEV * CHAR_STAT_PROPOCONST * 0.5);
        float fc = 1 + (CHAR_DEFAULT_ELEVAT_LEV / STAT_ELEVATCONST) + (CHAR_DEFAULT_EVOLV_LEV / STAT_EVOLVCONST);
		float fd = fa + (float)CHAR_DEFAULT_STAT;
		
        int fh = (int)(fd * fc);

        return fh;

    }


    

    // HP	CHAR_HP -----------------------------------------------------------------------------------------------------------------

    //	[한글 칼럼명]	        [영문 칼럼 명 (변수명)]	            [테이블] 혹은 데이터 출처

    //<수정전>	캐릭터 기본 힘	        CHAR_DEFAULT_STR                    CHAR_STAT 에서 계산된 값
    //<수정전>	캐릭터 기본 민첩	    CHAR_DEFAULT_AGI                    CHAR_STAT 에서 계산된 값
    //<수정전>	캐릭터 기본 지능	    CHAR_DEFAULT_INT                    CHAR_STAT 에서 계산된 값
    
    //<수정후>	캐릭터 현재 힘	        CHAR_CURRENT_STR                    CHAR_STAT 에서 계산된 값
    //<수정후>	캐릭터 현재 민첩	    CHAR_CURRENT_AGI                    CHAR_STAT 에서 계산된 값
    //<수정후>	캐릭터 현재 지능	    CHAR_CURRENT_INT                    CHAR_STAT 에서 계산된 값

    //	캐릭터 HP 비례상수	    CHAR_HP_PROPOCONST	                tb_CHAR_DEFAULT_STAT
    //	HP 승급상수	            HP_ELEVATCONST	                    tb_CHAR_ELEVAT_STATCONST
    //	HP 진화상수	            HP_EVOLVCONST	                    tb_CHAR_EVOLV_STATCONST

    //  항목 추가▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
    //  캐릭터 기본 승급 레벨   CHAR_DEFAULT_ELEVAT_LEV             tb_CHAR
    //  캐릭터 기본 진화 레벨   CHAR_DEFAULT_EVOLV_LEV              tb_CHAR
    //  ------------------------------------------------------------------

    //<수정전> = ((캐릭터 기본 힘 * 0.5 + 캐릭터 기본 민첩 * 0.2 + 캐릭터 기본 지능 * 0.3) * 캐릭터 HP 비례상수) * (1 + HP 승급상수 + HP 진화상수)
    //<수정전> = ((CHAR_DEFAULT_STR * 0.5 + CHAR_DEFAULT_AGI * 0.2 + CHAR_DEFAULT_INT * 0.3) * CHAR_HP_PROPOCONST) * (1 + HP_ELEVATCONST + HP_EVOLVCONST)

    //<수정후> = (캐릭터 현재 힘 * 0.5 + 캐릭터 현재 민첩 * 0.2 + 캐릭터 현재 지능 * 0.3) * 캐릭터 HP 비례상수 * (1 + 캐릭터 기본 승급 레벨 / HP 승급상수 + 캐릭터 기본 진화 레벨 / HP 진화상수)
    //<수정후> = (CHAR_CURRENT_STR * 0.5 + CHAR_CURRENT_AGI * 0.2 + CHAR_CURRENT_INT * 0.3) * CHAR_HP_PROPOCONST * (1 + CHAR_DEFAULT_ELEVAT_LEV / HP_ELEVATCONST + CHAR_DEFAULT_EVOLV_LEV / HP_EVOLVCONST)


    static public int CHAR_HP(int CHAR_CURRENT_STR,
                                int CHAR_CURRENT_AGI,
                                int CHAR_CURRENT_INT,
                                float CHAR_HP_PROPOCONST,
                                float HP_ELEVATCONST,
                                float HP_EVOLVCONST,
                                int CHAR_DEFAULT_ELEVAT_LEV,
                                int CHAR_DEFAULT_EVOLV_LEV)

    {

        float fa = (float)((float)CHAR_CURRENT_STR * 0.5);
        float fb = (float)((float)CHAR_CURRENT_AGI * 0.2);
        float fc = (float)((float)CHAR_CURRENT_INT * 0.3);
        float fd = fa + fb + fc;
        float fe = fd * CHAR_HP_PROPOCONST;
        float ff = 1 + (CHAR_DEFAULT_ELEVAT_LEV / HP_ELEVATCONST) + (CHAR_DEFAULT_EVOLV_LEV / HP_EVOLVCONST);
   
        int result = (int)(fe * ff);


		return result;
		
	}


    // MP	CHAR_MP -----------------------------------------------------------------------------------------------------------------

    //	[한글 칼럼명]	        [영문 칼럼 명 (변수명)]	        [테이블] 혹은 데이터 출처
    //	캐릭터 기본 레벨	    CHAR_DEFAULT_LEV	            tb_CHAR
    
    //	 = 캐릭터 기본 레벨 * 90/99 + 900/99		
    //	 = CHAR_DEFAULT_LEV * 90/99 + 900/99		

    // **** 캐릭터 기본 레벨(CHAR_DEFAULT_LEV) 의 경우 몬스터일 경우만 tb_CHAR 테이블의 데이터를 참조합니다. 
    // ++++ 유저 캐릭터의 경우 계정의 캐릭터가 마지막으로 플레이 했을 때의 저장됐던 레벨 정보를 참조합니다.

    
	static public int CHAR_MP(int CHAR_DEFAULT_LEV)
    {

        float fa = (float)(CHAR_DEFAULT_LEV * 90 / 99);
        float fb = 900 / 99;
        float fd = fa + fb;

		return (int)(fd);
		
	}

	// 물리 Or 마법 공격력	CHAR_OP -----------------------------------------------------------------------------------------------------------------
	
	//	[한글 칼럼명]	        [영문 칼럼 명 (변수명)]	            [테이블] 혹은 데이터 출처
	
	//<수정전> 물리공격력 ----------------------------------------------------------------------------------------------------------------
    //<수정전> 캐릭터 기본 힘	        tb_CHAR_DEFAULT_STAT1	            tb_CHAR_DEFAULT_STAT
    //<수정전> 캐릭터 기본 민첩         tb_CHAR_DEFAULT_STAT2	            tb_CHAR_DEFAULT_STAT
    //<수정전> END 물리공격력 -------------------------------------------------------------------------------------------------------------

    //<수정후> 물리공격력 ----------------------------------------------------------------------------------------------------------------
    //<수정후> 캐릭터 현재 힘	        CHAR_CURRENT_STAT1	                CHAR_STAT 에서 계산된 값
    //<수정후> 캐릭터 현재 민첩         CHAR_CURRENT_STAT2	                CHAR_STAT 에서 계산된 값
    //<수정후> END 물리공격력 -------------------------------------------------------------------------------------------------------------

    //<수정전> 마법공격력 ----------------------------------------------------------------------------------------------------------------
    //<수정전> 캐릭터 기본 지능	        tb_CHAR_DEFAULT_STAT1	            tb_CHAR_DEFAULT_STAT
    //<수정전> 캐릭터 기본 민첩	        tb_CHAR_DEFAULT_STAT2	            tb_CHAR_DEFAULT_STAT
    //<수정전> END 마법공격력 -------------------------------------------------------------------------------------------------------------

    //<수정후> 마법공격력 ----------------------------------------------------------------------------------------------------------------
    //<수정후> 캐릭터 현재 지능	        CHAR_CURRENT_STAT1	                CHAR_STAT 에서 계산된 값
    //<수정후> 캐릭터 현재 민첩	        CHAR_CURRENT_STAT2	                CHAR_STAT 에서 계산된 값
    //<수정후> END 마법공격력 -------------------------------------------------------------------------------------------------------------
    
    //	물리 Or 마법 공격력 비례상수	OP_PROPOCONST	                    tb_CHAR_DEFAULT_STAT
	//	물리 Or 마법 공격력 승급상수	OP_ELEVATCONST	                    tb_CHAR_ELEVAT_STATCONST
	//	물리 Or 마법 공격력 진화상수	OP_EVOLVCONST	                    tb_CHAR_EVOLV_STATCONST

    //  항목 추가▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
    //  캐릭터 기본 승급 레벨   CHAR_DEFAULT_ELEVAT_LEV             tb_CHAR
    //  캐릭터 기본 진화 레벨   CHAR_DEFAULT_EVOLV_LEV              tb_CHAR
    //  --------------------------------------------------------------------

    // 물리 공격력 수식 수정------------------------------------------------------------------------------------------------------------------------------------------------------
	//<수정전>	물리 공격력 = ((캐릭터 기본 힘 + 캐릭터 기본 민첩 * 0.4) * 물리 공격력 비례상수) * (1 + 물리 공격력 승급상수 + 물리 공격력 진화상수)
	//<수정전>	물리 공격력 = ((CHAR_DEFAULT_STR + CHAR_DEFAULT_AGI * 0.4) * P_OP_PROPOCONST) * (1 + P_OP_ELEVATCONST + P_OP_EVOLVCONST)

    //<수정후>  물리 공격력 = (캐릭터 현재 힘 + 캐릭터 현재 민첩 * 0.4) * 물리 공격력 비례상수 * (1 + 캐릭터 기본 승급 레벨 / 물리 공격력 승급상수 + 캐릭터 기본 진화 레벨 / 물리 공격력 진화상수)
    //<수정후>  물리 공격력 = (CHAR_CURRENT_STR + CHAR_CURRENT_AGI * 0.4) * P_OP_PROPOCONST * (1 + CHAR_DEFAULT_ELEVAT_LEV / P_OP_ELEVATCONST + CHAR_DEFAULT_EVOLV_LEV / P_OP_EVOLVCONST)
    // END 물리 공격력 수식 수정------------------------------------------------------------------------------------------------------------------------------------------------------

    // 마법 공격력 수식 수정------------------------------------------------------------------------------------------------------------------------------------------------------
    //<수정전>	마법 공격력 = (캐릭터 기본 지능 + 캐릭터 기본 민첩 * 0.4) * 마법 공격력 비례상수 * (1 + 마법 공격력 승급상수 + 마법 공격력 진화상수)
	//<수정전>	마법 공격력 = (CHAR_DEFAULT_INT + CHAR_DEFAULT_AGI * 0.4) * S_OP_PROPOCONST * (1 + S_OP_ELEVATCONST + S_OP_EVOLVCONST)

    //<수정후>	마법 공격력 = (캐릭터 현재 지능 + 캐릭터 현재 민첩 * 0.4) * 마법 공격력 비례상수 * (1 + 캐릭터 기본 승급 레벨 / 마법 공격력 승급상수 + 캐릭터 기본 진화 레벨 / 마법 공격력 진화상수)
    //<수정후>	마법 공격력 = (CHAR_CURRENT_INT + CHAR_CURRENT_AGI * 0.4) * S_OP_PROPOCONST * (1 + CHAR_DEFAULT_ELEVAT_LEV / S_OP_ELEVATCONST + CHAR_DEFAULT_EVOLV_LEV / S_OP_EVOLVCONST)
    // END 마법 공격력 수식 수정------------------------------------------------------------------------------------------------------------------------------------------------------

    static public int CHAR_OP(int CHAR_CURRENT_STAT1,
                               int CHAR_CURRENT_STAT2,
	                           float OP_PROPOCONST,
                               float OP_ELEVATCONST,
                               float OP_EVOLVCONST,
                               int CHAR_DEFAULT_ELEVAT_LEV,
                               int CHAR_DEFAULT_EVOLV_LEV)
    {

        float fa = (float)((float)CHAR_CURRENT_STAT2 * 0.4);
        float fc = 1 + (CHAR_DEFAULT_ELEVAT_LEV / OP_ELEVATCONST) + (CHAR_DEFAULT_EVOLV_LEV / OP_EVOLVCONST);
        float fd = fa + (float)CHAR_CURRENT_STAT1;

        int fh = (int)(fd * OP_PROPOCONST * fc);

        return fh;

    }

	// 치명타	CHAR_CAD -----------------------------------------------------------------------------------------------------------------
	//  [물리치명타]
	//	[한글 칼럼명]	            [영문 칼럼 명 (변수명)]	            [테이블] 혹은 데이터 출처
	//<수정전>	캐릭터 기본 힘	            CHAR_DEFAULT_STAT1	                tb_CHAR_DEFAULT_STAT
	//<수정전>	캐릭터 기본 민첩	        CHAR_DEFAULT_STAT2	                tb_CHAR_DEFAULT_STAT

    //<수정후>	캐릭터 현재 힘	            CHAR_CURRENT_STAT1	                CHAR_STAT 에서 계산된 값
    //<수정후>	캐릭터 현재 민첩	        CHAR_CURRENT_STAT2	                CHAR_STAT 에서 계산된 값


	//	물리 치명타 비례상수	    CAD_PROPOCONST	                tb_CHAR_DEFAULT_STAT
	//	물리 치명타 승급상수	    CAD_ELEVATCONST	                tb_CHAR_ELEVAT_STATCONST
	//	물리 치명타 진화상수	    CAD_EVOLVCONST	                tb_CHAR_EVOLV_STATCONST

    //  항목추가▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
    //  캐릭터 기본 승급 레벨   CHAR_DEFAULT_ELEVAT_LEV             tb_CHAR
    //  캐릭터 기본 진화 레벨   CHAR_DEFAULT_EVOLV_LEV              tb_CHAR


	//<수정전>	물리 치명타 = (캐릭터 기본 힘 * 0.2 + 캐릭터 기본 민첩 * 0.05) * 물리 치명타 비례상수 * (1 + 물리 치명타 승급상수 + 물리 치명타 진화상수)
	//<수정전>	물리 치명타 = (CHAR_DEFAULT_STR * 0.2 + CHAR_DEFAULT_AGI * 0.05) * P_CAD_PROPOCONST * (1 + P_CAD_ELEVATCONST + P_CAD_EVOLVCONST)

    //<수정후>  물리 치명타 = (캐릭터 기본 힘 * 0.2 + 캐릭터 기본 민첩 * 0.05) * 물리 치명타 비례상수 * (1 + 캐릭터 기본 승급 레벨 / 물리 치명타 승급상수 + 캐릭터 기본 진화 레벨 / 물리 치명타 진화상수)
    //<수정후>  물리 치명타 = (CHAR_DEFAULT_STR * 0.2 + CHAR_DEFAULT_AGI * 0.05) * P_CAD_PROPOCONST * (1 + CHAR_DEFAULT_ELEVAT_LEV / P_CAD_ELEVATCONST + CHAR_DEFAULT_EVOLV_LEV / P_CAD_EVOLVCONST)

	// -----------------------------------------------------------------------------------------------------------------
	
	// -----------------------------------------------------------------------------------------------------------------
	//  [마법치명타]
	//	[한글 칼럼명]	            [영문 칼럼 명 (변수명)]	            [테이블] 혹은 데이터 출처
	//<수정전>	캐릭터 기본 지능	CHAR_DEFAULT_STAT1	                tb_CHAR_DEFAULT_STAT
	//<수정전>	캐릭터 기본 민첩	CHAR_DEFAULT_STAT2	                tb_CHAR_DEFAULT_STAT

    //<수정후>	캐릭터 현재 지능	CHAR_CURRENT_STAT1	                CHAR_STAT 에서 계산된 값
    //<수정후>	캐릭터 현재 민첩	CHAR_CURRENT_STAT2	                CHAR_STAT 에서 계산된 값
    
    //	마법 치명타 비례상수	    CAD_PROPOCONST	                tb_CHAR_DEFAULT_STAT
	//	마법 치명타 승급상수	    CAD_ELEVATCONST	                tb_CHAR_ELEVAT_STATCONST
	//	마법 치명타 진화상수	    CAD_EVOLVCONST	                tb_CHAR_EVOLV_STATCONST

    //  항목추가▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
    //  캐릭터 기본 승급 레벨       CHAR_DEFAULT_ELEVAT_LEV             tb_CHAR
    //  캐릭터 기본 진화 레벨       CHAR_DEFAULT_EVOLV_LEV              tb_CHAR

    //<수정전>  마법 치명타 = ((캐릭터 기본 지능 * 0.2 + 캐릭터 기본 민첩 * 0.05) * 마법 치명타 비례상수) * (1 + 마법 치명타 승급상수 + 마법 치명타 진화상수)
    //<수정전>  마법 치명타 = ((CHAR_DEFAULT_INT * 0.2 + CHAR_DEFAULT_AGI * 0.05) * S_CAD_PROPOCONST) * (1 + S_CAD_ELEVATCONST + S_CAD_EVOLVCON

    //<수정후>  마법 치명타 = (캐릭터 현재 힘 * 0.2 + 캐릭터 현재 민첩 * 0.05) * 물리 치명타 비례상수 * (1 + 캐릭터 기본 승급 레벨 / 물리 치명타 승급상수 + 캐릭터 기본 진화 레벨 / 물리 치명타 진화상수)
    //<수정후>  마법 치명타 = (CHAR_CURRENT_STR * 0.2 + CHAR_CURRENT_AGI * 0.05) * P_CAD_PROPOCONST * (1 + CHAR_DEFAULT_ELEVAT_LEV / P_CAD_ELEVATCONST + CHAR_DEFAULT_EVOLV_LEV / P_CAD_EVOLVCONST)

	// -----------------------------------------------------------------------------------------------------------------
    static public int CHAR_CAD(int CHAR_CURRENT_STAT1,
                                int CHAR_CURRENT_STAT2,
								float CAD_PROPOCONST,
								float CAD_ELEVATCONST,
                                float CAD_EVOLVCONST,
                               int CHAR_DEFAULT_ELEVAT_LEV,
                               int CHAR_DEFAULT_EVOLV_LEV)
    {



        float fa = (float)((float)CHAR_CURRENT_STAT1 * 0.2);
        float fb = (float)((float)CHAR_CURRENT_STAT2 * 0.05);
        float fc = fa + fb;
        float ff = 1 + (CHAR_DEFAULT_ELEVAT_LEV / CAD_ELEVATCONST) + (CHAR_DEFAULT_EVOLV_LEV / CAD_EVOLVCONST);

		int fg = (int)(fc * (float)CAD_PROPOCONST * ff);
        return fg;

    }

    // 공격 명중률	CHAR_ATT_AR -----------------------------------------------------------------------------------------------------------------
	//  [물리 명률중]
    //	[한글 칼럼명]	                [영문 칼럼 명 (변수명)]	            [테이블] 혹은 데이터 출처
	//<수정전>	캐릭터 기본 힘	        CHAR_DEFAULT_STAT1	                tb_CHAR_DEFAULT_STAT
	//<수정전>	캐릭터 기본 민첩	    CHAR_DEFAULT_STAT2	                tb_CHAR_DEFAULT_STAT

    //<수정전>	캐릭터 현재 힘	        CHAR_CURRENT_STAT1	                CHAR_STAT 에서 계산된 값
    //<수정전>	캐릭터 현재 민첩	    CHAR_CURRENT_STAT2	                CHAR_STAT 에서 계산된 값

    //	물리 공격 명중률 비례상수	    P_ATT_AR_PROPOCONST	                tb_CHAR_DEFAULT_STAT
    //	물리 공격 명중률 승급상수	    P_ATT_AR_ELEVATCONST	            tb_CHAR_ELEVAT_STATCONST
    //	물리 공격 명중률 진화상수	    P_ATT_AR_EVOLVCONST	                tb_CHAR_EVOLV_STATCONST

    //  항목 추가▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
    //  캐릭터 기본 승급 레벨           CHAR_DEFAULT_ELEVAT_LEV             tb_CHAR
    //  캐릭터 기본 진화 레벨           CHAR_DEFAULT_EVOLV_LEV              tb_CHAR

    //<수정전> 물리 공격 명중률 = ((캐릭터 기본 힘 * 0.25 + 캐릭터 기본 민첩 * 0.16) * 물리 공격 명중률 비례상수) * (1 + 물리 공격 명중률 승급상수 + 물리 공격 명중률 진화상수)		
    //<수정전> 물리 공격 명중률 = ((CHAR_DEFAULT_STR * 0.25 + CHAR_DEFAULT_AGI * 0.16) * P_ATT_AR_PROPOCONST) * (1 + P_ATT_AR_ELEVATCONST + P_ATT_AR_EVOLVCONST)		

    //<수정후> 물리 공격 명중률 = (캐릭터 현재 힘 * 0.25 + 캐릭터 현재 민첩 * 0.16) * 물리 공격 명중률 비례상수 * (1 + 캐릭터 기본 승급 레벨 / 물리 공격 명중률 승급상수 + 캐릭터 기본 진화 레벨 / 물리 공격 명중률 진화상수)
    //<수정후> 물리 공격 명중률 = (CHAR_CURRENT_STR * 0.25 + CHAR_CURRENT_AGI * 0.16) * P_ATT_AR_PROPOCONST * (1 + CHAR_DEFAULT_ELEVAT_LEV / P_ATT_AR_ELEVATCONST + CHAR_DEFAULT_EVOLV_LEV / P_ATT_AR_EVOLVCONST)

	// -----------------------------------------------------------------------------------------------------------------
	//  [미밥 명률중]
	//	[한글 칼럼명]	            [영문 칼럼 명 (변수명)]	        [테이블] 혹은 데이터 출처
	//<수정전>	캐릭터 기본 지능	CHAR_DEFAULT_STAT1	            tb_CHAR_DEFAULT_STAT
	//<수정전>	캐릭터 기본 민첩	CHAR_DEFAULT_STAT2	            tb_CHAR_DEFAULT_STAT

    //<수정후>	캐릭터 현재 지능	CHAR_CURRENT_STAT1	            CHAR_STAT 에서 계산된 값
    //<수정후>	캐릭터 현재 민첩	CHAR_CURRENT_STAT2	            CHAR_STAT 에서 계산된 값
    
    //	마법 공격 명중률 비례상수	S_ATT_AR_PROPOCONST	            tb_CHAR_DEFAULT_STAT
	//	마법 공격 명중률 승급상수	S_ATT_AR_ELEVATCONST	        tb_CHAR_ELEVAT_STATCONST
	//	마법 공격 명중률 진화상수	S_ATT_AR_EVOLVCONST	            tb_CHAR_EVOLV_STATCONST

    //  항목 추가 ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
    //  캐릭터 기본 승급 레벨       CHAR_DEFAULT_ELEVAT_LEV         tb_CHAR
    //  캐릭터 기본 진화 레벨       CHAR_DEFAULT_EVOLV_LEV          tb_CHAR
    
    //<수정전> 마법 공격 명중률 = ((캐릭터 기본 지능 * 0.25 + 캐릭터 기본 민첩 * 0.16) * 마법 공격 명중률 비례상수) * (1 + 마법 공격 명중률 승급상수 + 마법 공격 명중률 진화상수)		
	//<수정전> 마법 공격 명중률 = ((CHAR_DEFAULT_INT * 0.25 + CHAR_DEFAULT_AGI * 0.16) * S_ATT_AR_PROPOCONST) * (1 + S_ATT_AR_ELEVATCONST + S_ATT_AR_EVOLVCONST)

    //<수정후> 마법 공격 명중률 = (캐릭터 현재 지능 * 0.25 + 캐릭터 현재 민첩 * 0.16) * 마법 공격 명중률 비례상수 * (1 + 캐릭터 기본 승급 레벨 / 마법 공격 명중률 승급상수 + 캐릭터 기본 진화 레벨 / 마법 공격 명중률 진화상수)
    //<수정후> 마법 공격 명중률 = (CHAR_CURRENT_INT * 0.25 + CHAR_CURRENT_AGI * 0.16) * S_ATT_AR_PROPOCONST * (1 + CHAR_DEFAULT_ELEVAT_LEV / S_ATT_AR_ELEVATCONST + CHAR_DEFAULT_EVOLV_LEV / S_ATT_AR_EVOLVCONST)

	// -----------------------------------------------------------------------------------------------------------------

    static public int CHAR_ATT_AR(int CHAR_CURRENT_STAT1,
                                int CHAR_CURRENT_STAT2,
                                float ATT_AR_PROPOCONST,
                                float ATT_AR_ELEVATCONST,
                                float ATT_AR_EVOLVCONST,
                               int CHAR_DEFAULT_ELEVAT_LEV,
                               int CHAR_DEFAULT_EVOLV_LEV)
    {



        float fa = (float)((float)CHAR_CURRENT_STAT1 * 0.25);
        float fb = (float)((float)CHAR_CURRENT_STAT2 * 0.16);
        float fc = fa + fb;
        float fd = ATT_AR_PROPOCONST;
        float ff = 1 + (CHAR_DEFAULT_ELEVAT_LEV / ATT_AR_ELEVATCONST) + (CHAR_DEFAULT_EVOLV_LEV / ATT_AR_EVOLVCONST);

        int fg = (int)(fc * fd * ff);
        return fg;

    }

    // 물리 방어력	CHAR_P_DFP -----------------------------------------------------------------------------------------------------------------

    //	[한글 칼럼명]	                [영문 칼럼 명 (변수명)]	            [테이블] 혹은 데이터 출처
    //<수정전>	캐릭터 기본 힘	        CHAR_DEFAULT_STR	                tb_CHAR_DEFAULT_STAT
    //<수정전>	캐릭터 기본 민첩	    CHAR_DEFAULT_AGI	                tb_CHAR_DEFAULT_STAT

    //<수정후>	캐릭터 현재 힘	        CHAR_CURRENT_STR	                CHAR_STAT 에서 계산된 값
    //<수정후>	캐릭터 현재 민첩	    CHAR_CURRENT_AGI	                CHAR_STAT 에서 계산된 값
    
    //	물리 방어력 비례상수	        P_DFP_PROPOCONST	                tb_CHAR_DEFAULT_STAT
    //	물리 방어력 승급상수	        P_DFP_ELEVATCONST	                tb_CHAR_ELEVAT_STATCONST
    //	물리 방어력 진화상수	        P_DFP_EVOLVCONST	                tb_CHAR_EVOLV_STATCONST

    //  항목 추가 ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
    //  캐릭터 기본 승급 레벨   CHAR_DEFAULT_ELEVAT_LEV             tb_CHAR
    //  캐릭터 기본 진화 레벨   CHAR_DEFAULT_EVOLV_LEV              tb_CHAR

    //<수정전>	물리 방어력 = ((캐릭터 기본 힘 / 7 + 캐릭터 기본 민첩 / 14) * 물리 방어력 비례상수) * (1 + 물리 방어력 승급상수 + 물리 방어력 진화상수)		
    //<수정전>	물리 방어력 = ((CHAR_DEFAULT_STR / 7 + CHAR_DEFAULT_AGI / 14) * P_DFP_PROPOCONST) * (1 + P_DFP_ELEVATCONST + P_DFP_EVOLVCONST)		

    //<수정후>	물리 방어력 = (캐릭터 현재 힘 / 7 + 캐릭터 현재 민첩 / 14) * 물리 방어력 비례상수 * (1 + 캐릭터 기본 승급 레벨 / 물리 방어력 승급상수 + 캐릭터 기본 진화 레벨 / 물리 방어력 진화상수)
    //<수정후>  물리 방어력 = (CHAR_CURRENT_STR / 7 + CHAR_CURRENT_AGI / 14) * P_DFP_PROPOCONST * (1 + CHAR_DEFAULT_ELEVAT_LEV / P_DFP_ELEVATCONST + CHAR_DEFAULT_EVOLV_LEV / P_DFP_EVOLVCONST)


	static public int CHAR_P_DFP(int CHAR_CURRENT_STR,
                                 int CHAR_CURRENT_AGI,
                                 float P_DFP_PROPOCONST,
                                 float P_DFP_ELEVATCONST,
                                 float P_DFP_EVOLVCONST,
                                 int CHAR_DEFAULT_ELEVAT_LEV,
                                 int CHAR_DEFAULT_EVOLV_LEV)
    {


        float fa = (float)(CHAR_CURRENT_STR / 7);
        float fb = (float)(CHAR_CURRENT_AGI / 14);
        float fc = fa + fb;
        float fd = P_DFP_PROPOCONST;
        float ff = 1 + (CHAR_DEFAULT_ELEVAT_LEV / P_DFP_ELEVATCONST) + (CHAR_DEFAULT_EVOLV_LEV / P_DFP_EVOLVCONST);

        int fg = (int)(fc * fd * ff);
        return fg;

    }

    // 마법 저항력	CHAR_S_RESIST -----------------------------------------------------------------------------------------------------------------

    //	[한글 칼럼명]	                [영문 칼럼 명 (변수명)]	                [테이블] 혹은 데이터 출처
    //<수정전>	캐릭터 기본 지능	    CHAR_DEFAULT_INT	                    tb_CHAR_DEFAULT_STAT

    //<수정후>	캐릭터 현재 지능	    CHAR_CURRENT_INT	                    CHAR_STAT 에서 계산된 값
    
    //	마법 저항력 비례상수	        S_RESIST_PROPOCONST	                    tb_CHAR_DEFAULT_STAT
    //	마법 저항력 승급상수	        S_RESIST_ELEVATCONST	                tb_CHAR_ELEVAT_STATCONST
    //	마법 저항력 진화상수	        S_RESIST_EVOLVCONST	                    tb_CHAR_EVOLV_STATCONST

    //  항목 추가 ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
    //  캐릭터 기본 승급 레벨   CHAR_DEFAULT_ELEVAT_LEV             tb_CHAR
    //  캐릭터 기본 진화 레벨   CHAR_DEFAULT_EVOLV_LEV              tb_CHAR

    //<수정전> 마법 저항력 = (캐릭터 기본 지능 / 10 * 마법 저항력 비례상수) * (1 + 마법 저항력 승급상수 + 마법 저항력 진화상수)		
    //<수정전> 마법 저항력 = (CHAR_DEFAULT_INT / 10 * S_RESIST_PROPOCONST) * (1 + S_RESIST_ELEVATCONST + S_RESIST_EVOLVCONST)		

    //<수정후> 마법 저항력 = (캐릭터 현재 지능 / 10 * 마법 저항력 비례상수) * (1 + 캐릭터 기본 승급 레벨 / 마법 저항력 승급상수 + 캐릭터 기본 진화 레벨 / 마법 저항력 진화상수)
    //<수정후> 마법 저항력 = (CHAR_CURRENT_INT / 10 * S_RESIST_PROPOCONST) * (1 + CHAR_DEFAULT_ELEVAT_LEV / S_RESIST_ELEVATCONST + CHAR_DEFAULT_EVOLV_LEV / S_RESIST_EVOLVCONST)

    static public int CHAR_S_RESIST(int CHAR_CURRENT_INT,
                                       float S_RESIST_PROPOCONST,
                                       float S_RESIST_ELEVATCONST,
                                       float S_RESIST_EVOLVCONST,
                                       int CHAR_DEFAULT_ELEVAT_LEV,
                                       int CHAR_DEFAULT_EVOLV_LEV)
    {

        float fa = (float)(CHAR_CURRENT_INT / 10 * S_RESIST_PROPOCONST);
        float fb = (float)(1 + (CHAR_DEFAULT_ELEVAT_LEV / S_RESIST_ELEVATCONST) + (CHAR_DEFAULT_EVOLV_LEV / S_RESIST_EVOLVCONST));
        
        int fd = (int)(fa * fb);
        return fd;

    }

    // 전투력	CHAR_ATPR -----------------------------------------------------------------------------------------------------------------

    //	[한글 칼럼명]	            [영문 칼럼 명 (변수명)]	            [테이블] 혹은 데이터 출처
    //	캐릭터 물리 공격력	        CHAR_P_OP	                        수식 결과 값
    //	캐릭터 마법 공격력	        CHAR_S_OP	                        수식 결과 값
    //	캐릭터 물리 방어력	        CHAR_P_DFP	                        수식 결과 값
    //	캐릭터 마법 저항력	        CHAR_S_RESIST	                    수식 결과 값
    //	캐릭터 전투력 비례상수	    CHAR_ATPR_PROPOCONST	            tb_CHAR_DEFAULT_STAT
    //	전투력 승급상수	            ATPR_ELEVATCONST	                tb_CHAR_ELEVAT_STATCONST
    //	전투력 진화상수	            ATPR_EVOLVCONST	                    tb_CHAR_EVOLV_STATCONST

    //  항목 추가 ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
    //  캐릭터 기본 승급 레벨   CHAR_DEFAULT_ELEVAT_LEV             tb_CHAR
    //  캐릭터 기본 진화 레벨   CHAR_DEFAULT_EVOLV_LEV              tb_CHAR

    //<수정전> 전투력 = (캐릭터 물리 공격력 + 캐릭터 마법 공격력 + 캐릭터 물리 방어력 + 캐릭터 마법 저항력) * 캐릭터 전투력 비례상수) * (1 + 전투력 승급상수 + 전투력 진화상수)		
    //<수정전> 전투력 = (CHAR_P_OP + CHAR_S_OP + CHAR_P_DFP + CHAR_S_RESIST) * CHAR_ATPR_PROPOCONST) * (1 + ATPR_ELEVATCONST + ATPR_EVOLVCONST)		

    //<수정후> 전투력 =  = (캐릭터 물리 공격력 + 캐릭터 마법 공격력 + 캐릭터 물리 방어력 + 캐릭터 마법 저항력) * 캐릭터 전투력 비례상수) * (1 + 캐릭터 기본 승급 레벨 / 전투력 승급상수 + 캐릭터 기본 진화 레벨 / 전투력 진화상수)	
    //<수정후> 전투력 = (CHAR_P_OP + CHAR_S_OP + CHAR_P_DFP + CHAR_S_RESIST) * CHAR_ATPR_PROPOCONST) * (1 + CHAR_DEFAULT_ELEVAT_LEV / ATPR_ELEVATCONST + CHAR_DEFAULT_EVOLV_LEV / ATPR_EVOLVCONST)


	static public int CHAR_ATPR(   int CHAR_P_OP,
                            int CHAR_S_OP,
                            int CHAR_P_DFP,
                            int CHAR_S_RESIST,
                            float CHAR_ATPR_PROPOCONST,
                            float ATPR_ELEVATCONST,
                            float ATPR_EVOLVCONST,
                            int CHAR_DEFAULT_ELEVAT_LEV,
                            int CHAR_DEFAULT_EVOLV_LEV)
    {

        float fa = (float)(CHAR_P_OP + CHAR_S_OP + CHAR_P_DFP + CHAR_S_RESIST);
        float fb = CHAR_ATPR_PROPOCONST;
        float fc = 1 + (CHAR_DEFAULT_ELEVAT_LEV / ATPR_ELEVATCONST) + (CHAR_DEFAULT_EVOLV_LEV / ATPR_EVOLVCONST);

        int fd = (int)(fa * fb * fc);
		return fd;

	}

	//  회피율	CHAR_DODGER -----------------------------------------------------------------------------------------------------
	//  [물리 회피율]
	//	[한글 칼럼명]	            [영문 칼럼 명 (변수명)]	            [테이블] 혹은 데이터 출처
	//<수정전>	캐릭터 기본 힘	    CHAR_DEFAULT_STAT1	                tb_CHAR_DEFAULT_STAT
    //<수정전>	캐릭터 기본 민첩	CHAR_DEFAULT_STAT2	                tb_CHAR_DEFAULT_STAT

    //<수정후>	캐릭터 현재 힘	    CHAR_CURRENT_STAT1	                CHAR_STAT 에서 계산된 값
    //<수정후>	캐릭터 현재 민첩	CHAR_CURRENT_STAT2	                CHAR_STAT 에서 계산된 값

    //	물리 회피율 비례상수	    P_DODGER_PROPOCONST	                tb_CHAR_DEFAULT_STAT
	//	물리 회피율 승급상수	    P_DODGER_ELEVATCONST	            tb_CHAR_ELEVAT_STATCONST
	//	물리 회피율 진화상수	    P_DODGER_EVOLVCONST	                tb_CHAR_EVOLV_STATCONST

    //  항목 추가 ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
    //  캐릭터 기본 승급 레벨       CHAR_DEFAULT_ELEVAT_LEV             tb_CHAR
    //  캐릭터 기본 진화 레벨       CHAR_DEFAULT_EVOLV_LEV              tb_CHAR

    //<수정전> 물리 회피율 = ((캐릭터 기본 힘 * 0.3 + 캐릭터 기본 민첩 * 0.2) * 물리 회피율 비례상수) * (1 + 물리 회피율 승급상수 + 물리 회피율 진화상수)
    //<수정전> 물리 회피율 = ((CHAR_DEFAULT_STR * 0.3 + CHAR_DEFAULT_AGI * 0.2) * P_DODGER_PROPOCONST) * (1 + P_DODGER_ELEVATCONST + P_DODGER_EVOLVCONST)

    //<수정후> 물리 회피율 = (캐릭터 현재 힘 * 0.3 + 캐릭터 현재 민첩 * 0.2) * 물리 회피율 비례상수 * (1 + 캐릭터 기본 승급 레벨 / 물리 회피율 승급상수 + 캐릭터 기본 진화 레벨 / 물리 회피율 진화상수)
    //<수정후> 물리 회피율 = (CHAR_CURRENT_STR * 0.3 + CHAR_CURRENT_AGI * 0.2) * P_DODGER_PROPOCONST * (1 + CHAR_DEFAULT_ELEVAT_LEV / P_DODGER_ELEVATCONST + CHAR_DEFAULT_EVOLV_LEV / P_DODGER_EVOLVCONST)

	//  -----------------------------------------------------------------------------------------------------------------

	//  -----------------------------------------------------------------------------------------------------------------
	//	[한글 칼럼명]	                [영문 칼럼 명 (변수명)]	                [테이블] 혹은 데이터 출처
	//  [마법 회피율]

	//<수정전>	캐릭터 기본 민첩	    CHAR_DEFAULT_STAT1	                    tb_CHAR_DEFAULT_STAT
	//<수정전>	캐릭터 기본 지능	    CHAR_DEFAULT_STAT2	                    tb_CHAR_DEFAULT_STAT

    //<수정후>	캐릭터 기본 민첩	    CHAR_CURRENT_STAT1	                    CHAR_STAT 에서 계산된 값
    //<수정후>	캐릭터 기본 지능	    CHAR_CURRENT_STAT2	                    CHAR_STAT 에서 계산된 값
    
    //	마법 회피율 비례상수	        S_DODGER_PROPOCONST	                    tb_CHAR_DEFAULT_STAT
	//	마법 회피율 승급상수	        S_DODGER_ELEVATCONST	                tb_CHAR_ELEVAT_STATCONST
	//	마법 회피율 진화상수	        S_DODGER_EVOLVCONST	                    tb_CHAR_EVOLV_STATCONST

    //  항목 추가 ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒
    //  캐릭터 기본 승급 레벨           CHAR_DEFAULT_ELEVAT_LEV                 tb_CHAR
    //  캐릭터 기본 진화 레벨           CHAR_DEFAULT_EVOLV_LEV                  tb_CHAR

    //<수정전> 마법 회피율 = ((캐릭터 기본 지능 * 0.3 + 캐릭터 기본 민첩 * 0.2) * 마법 회피율 비례상수) * (1 + 마법 회피율 승급상수 + 마법 회피율 진화상수)
    //<수정전> 마법 회피율 = ((CHAR_DEFAULT_INT * 0.3 + CHAR_DEFAULT_AGI * 0.2) * S_DODGER_PROPOCONST) * (1 + S_DODGER_ELEVATCONST + S_DODGER_EVOLVCONST)

    //<수정후> 마법 회피율 = (캐릭터 현재 지능 * 0.3 + 캐릭터 현재 민첩 * 0.2) * 마법 회피율 비례상수 * (1 + 캐릭터 기본 승급 레벨 / 마법 회피율 승급상수 + 캐릭터 기본 진화 레벨 / 마법 회피율 진화상수)
    //<수정후> 마법 회피율 = (CHAR_CURRENT_INT * 0.3 + CHAR_CURRENT_AGI * 0.2) * S_DODGER_PROPOCONST * (1 + CHAR_DEFAULT_ELEVAT_LEV / S_DODGER_ELEVATCONST + CHAR_DEFAULT_EVOLV_LEV / S_DODGER_EVOLVCONST)


	//  -----------------------------------------------------------------------------------------------------------------

    static public int CHAR_DODGER(int CHAR_CURRENT_STAT1,
                                int CHAR_CURRENT_STAT2,
                                float DODGER_PROPOCONST,
                                float DODGER_ELEVATCONST,
                                float DODGER_EVOLVCONST,
                                int CHAR_DEFAULT_ELEVAT_LEV,
                                int CHAR_DEFAULT_EVOLV_LEV)
    {



        float fa = (float)((float)CHAR_CURRENT_STAT1 * 0.3);
        float fb = (float)((float)CHAR_CURRENT_STAT2 * 0.2);
        float fc = fa + fb;
        float fd = DODGER_PROPOCONST;
        float ff = 1 + (CHAR_DEFAULT_ELEVAT_LEV / DODGER_ELEVATCONST) + (CHAR_DEFAULT_EVOLV_LEV / DODGER_EVOLVCONST);

        int fg = (int)(fc * fd * ff);
        return fg;

    }

    // 이동 속도	CHAR_MOVE_SPEED -----------------------------------------------------------------------------------------------------------------

    //	[한글 칼럼명]	                [영문 칼럼 명 (변수명)]	                [테이블] 혹은 데이터 출처
    //<수정전>	캐릭터 기본 민첩	    CHAR_DEFAULT_AGI	                    tb_CHAR_DEFAULT_STAT
    //<수정전>	캐릭터 기본 힘	        CHAR_DEFAULT_STR	                    tb_CHAR_DEFAULT_STAT

    //<수정후>	캐릭터 현재 민첩	    CHAR_CURRENT_AGI	                    CHAR_STAT 에서 계산된 값
    //<수정후>	캐릭터 현재 힘	        CHAR_CURRENT_STR	                    CHAR_STAT 에서 계산된 값

    //	이동속도 민첩상수	            MSPEED_AGILCONST	                    tb_CHAR_DEFAULT_STAT
    //	이동속도 힘상수	                MSPEED_STRCONST	                        tb_CHAR_DEFAULT_STAT
    //	캐릭터 기본 레벨	            CHAR_DEFAULT_LEV	                    tb_CHAR
    //	이동속도 레벨상수	            MSPEED_LEVELCONST	                    tb_CHAR_DEFAULT_STAT
    //	이동속도 기울기상수	            MSPEED_GRADECONST	                    tb_CHAR_DEFAULT_STAT

    // **** 캐릭터 기본 레벨(CHAR_DEFAULT_LEV) 의 경우 몬스터일 경우만 tb_CHAR 테이블의 데이터를 참조합니다. 
    // ++++ 유저 캐릭터의 경우 계정의 캐릭터가 마지막으로 플레이 했을 때 저장됐던 레벨 정보를 참조합니다.

    //<수정전> 이동속도 = (캐릭터 기본 민첩 * 이동속도 민첩상수 + 캐릭터 기본 힘 * 이동속도 힘상수 + 캐릭터 기본 레벨 * 이동속도 레벨상수) / 320 * 이동속도 기울기상수
    //<수정전> 이동속도 = (CHAR_DEFAULT_AGI * MSPEED_AGILCONST + CHAR_DEFAULT_STR * MSPEED_STRCONST + CHAR_DEFAULT_LEV * MSPEED_LEVELCONST) / 320 * MSPEED_GRADECONST

    //<수정후> 이동속도 =(캐릭터 현재 민첩 * 이동속도 민첩상수 + 캐릭터 현재 힘 * 이동속도 힘상수 + 캐릭터 기본 레벨 * 이동속도 레벨상수) / 320 * 이동속도 기울기상수
    //<수정후> 이동속도 =(CHAR_CURRENT_AGI * MSPEED_AGILCONST + CHAR_CURRENT_STR * MSPEED_STRCONST + CHAR_DEFAULT_LEV * MSPEED_LEVELCONST) / 320 * MSPEED_GRADECONST

    static public int CHAR_MOVE_SPEED(int CHAR_CURRENT_AGI,
                                float MSPEED_AGILCONST,
                                int CHAR_CURRENT_STR,
                                float MSPEED_STRCONST,
                                int CHAR_DEFAULT_LEV,
                                float MSPEED_LEVELCONST,
                                float MSPEED_GRADECONST)
    {



        float fa = (float)((float)CHAR_CURRENT_AGI * MSPEED_AGILCONST);
        float fb = (float)((float)CHAR_CURRENT_STR * MSPEED_STRCONST);
		float fc = (float)((float)CHAR_DEFAULT_LEV * MSPEED_LEVELCONST);
        float fd = fa + fb + fc;
        int ff = (int)(fd / 320 * MSPEED_GRADECONST);

        return ff;

    }

    // 공격 속도    CHAR_ATT_SPEED -----------------------------------------------------------------------------------------------------------------

    //	[한글 칼럼명]	            [영문 칼럼 명 (변수명)]	            [테이블] 혹은 데이터 출처
    //	캐릭터 기본 레벨	        CHAR_DEFAULT_LEV	                tb_CHAR
    
    //<수정전>	캐릭터 기본 힘	    CHAR_DEFAULT_STR	                tb_CHAR_DEFAULT_STAT
    //<수정전>	캐릭터 기본 민첩	CHAR_DEFAULT_AGI	                tb_CHAR_DEFAULT_STAT

    //<수정후>	캐릭터 현재 힘	    CHAR_CURRENT_STR	                CHAR_STAT 에서 계산된 값
    //<수정후>	캐릭터 현재 민첩	CHAR_CURRENT_AGI	                CHAR_STAT 에서 계산된 값
    
    //	공격속도 기울기상수	        ATTSPEED_GRADECONST	                tb_CHAR_DEFAULT_STAT
    //	공격속도 레벨상수	        ATTSPEED_LEVELCONST	                tb_CHAR_DEFAULT_STAT
    //	공격속도 힘상수	            ATTSPEED_STRCONST	                tb_CHAR_DEFAULT_STAT
    //	공격속도 민첩상수	        ATTSPEED_AGILCONST	                tb_CHAR_DEFAULT_STAT

    // **** 캐릭터 기본 레벨(CHAR_DEFAULT_LEV) 의 경우 몬스터일 경우만 tb_CHAR 테이블의 데이터를 참조합니다. 
    // ++++ 유저 캐릭터의 경우 계정의 캐릭터가 마지막으로 플레이 했을 때의 저장됐던 레벨 정보를 참조합니다.

    //<수정전> 공격속도 = 1+ (민첩스 * 민첩 공속상수 + 힘스 * 힘 공속상수 + 레벨스 * 레벨 공속상수) / 320 * 공속 기울기상수		
    //<수정전> 공격속도	= 1+ (ATTSPEED_STRCONST * ATTSPEED_AGILCONST + CHAR_DEFAULT_AGI * ATTSPEED_GRADECONST + CHAR_DEFAULT_LEV * CHAR_DEFAULT_STR) / 320 * ATTSPEED_LEVELCONST		

    //<수정후> 공격속도 = (캐릭터 현재 민첩 * 공격속도 민첩상수 + 캐릭터 현재 힘 * 공격속도 힘상수 + 캐릭터 기본 레벨 * 공격속도 레벨상수) / 320 * 공격속도 기울기상수
    //<수정후> 공격속도 = (CHAR_CURRENT_AGI * ATTSPEED_AGILCONST + CHAR_CURRENT_STR * ATTSPEED_STRCONST + CHAR_DEFAULT_LEV * ATTSPEED_LEVELCONST) / 320 * ATTSPEED_GRADECONST

	static public int CHAR_ATT_SPEED(  int CHAR_DEFAULT_LEV,
                                        int CHAR_CURRENT_STR,
                                        int CHAR_CURRENT_AGI,
                                        float ATTSPEED_GRADECONST,
                                        float ATTSPEED_LEVELCONST,
                                        float ATTSPEED_STRCONST,
                                        float ATTSPEED_AGILCONST)
    {


        float fa = (float)((float)CHAR_CURRENT_AGI * ATTSPEED_AGILCONST);
        float fb = (float)((float)CHAR_CURRENT_STR * ATTSPEED_STRCONST);
        float fc = (float)(CHAR_DEFAULT_LEV * ATTSPEED_LEVELCONST);
        float fd = fa + fb + fc;
        int ff = (int)(fd / 320 * ATTSPEED_LEVELCONST);

        return ff;

    }

    // 팀 경험치	TEAM_EXP -----------------------------------------------------------------------------------------------------------------
    // 레벨당 습득해야 할 경험치 양

    // tb_CHAR_EXP 테이블의 경험치 관련 상수들을 참조하기 위한 [경험치 구간 아이디] 구하는 수식 = (int)(캐릭터 현재 레벨 / 3 + 0.2)
    // <예제> 40레벨 캐릭터의 tb_CHAR_EXP 테이블의 [경험치 구간 아이디] = (int)(40 / 3 + 0.2) = 14

    //	[한글 칼럼명]	            [영문 칼럼 명 (변수명)]	            [테이블] 혹은 데이터 출처
    //	캐릭터 기본 레벨	        CHAR_DEFAULT_LEV	                tb_CHAR
    //	팀 경험치상수 1	            TEAM_EXPCONST_1	                    tb_CHAR_EXP
    //	팀 경험치상수 2	            TEAM_EXPCONST_2	                    tb_CHAR_EXP
    //	팀 경험치상수 3	            TEAM_EXPCONST_3	                    tb_CHAR_EXP
    //	팀 경험치상수 4	            TEAM_EXPCONST_4	                    tb_CHAR_EXP

    // **** 캐릭터 기본 레벨(CHAR_DEFAULT_LEV) 의 경우 몬스터일 경우만 tb_CHAR 테이블의 데이터를 참조합니다. 
    // ++++ 유저 캐릭터의 경우 계정의 캐릭터가 마지막으로 플레이 했을 때의 저장됐던 레벨 정보를 참조합니다.

    //	 = ((캐릭터 기본 레벨 + 팀 경험치상수 1) * (캐릭터 기본 레벨 + 팀 경험치상수 2) * 팀 경험치상수 3) * 캐릭터 기본 레벨 * 팀 경험치상수 4		
    //	 = ((CHAR_DEFAULT_LEV + TEAM_EXPCONST_1) * (CHAR_DEFAULT_LEV + TEAM_EXPCONST_2) * TEAM_EXPCONST_3) * CHAR_DEFAULT_LEV * TEAM_EXPCONST_4		


	static public int TEAM_EXP(int CHAR_DEFAULT_LEV,
                        float TEAM_EXPCONST_1,
                        float TEAM_EXPCONST_2,
                        float TEAM_EXPCONST_3,
                        float TEAM_EXPCONST_4)
    {



        float fa = (float)((float)CHAR_DEFAULT_LEV + TEAM_EXPCONST_1);
        float fb = (float)((float)CHAR_DEFAULT_LEV + TEAM_EXPCONST_2);
        float fc = TEAM_EXPCONST_3;
        float fd = fa * fb * fc;
        float ff = fd * CHAR_DEFAULT_LEV * TEAM_EXPCONST_4;

		return (int)(ff);
		
	}

    // 캐릭터 경험치	CHAR_EXP -----------------------------------------------------------------------------------------------------------------

    // tb_CHAR_EXP 테이블의 경험치 관련 상수들을 참조하기 위한 [경험치 구간 아이디] 구하는 수식 = (int)(캐릭터 현재 레벨 / 3 + 0.2)
    // <예제> 40레벨 캐릭터의 tb_CHAR_EXP 테이블의 [경험치 구간 아이디] = (int)(40 / 3 + 0.2) = 14

    //	[한글 칼럼명]	            [영문 칼럼 명 (변수명)]	    [테이블] 혹은 데이터 출처
    //	캐릭터 기본 레벨	        CHAR_DEFAULT_LEV	        tb_CHAR
    //	캐릭터 경험치상수 1	        CHAR_EXPCONST_1	            tb_CHAR_EXP
    //	캐릭터 경험치상수 2	        CHAR_EXPCONST_2	            tb_CHAR_EXP
    //	캐릭터 경험치상수 3	        CHAR_EXPCONST_3	            tb_CHAR_EXP
    //	캐릭터 경험치상수 4	        CHAR_EXPCONST_4	            tb_CHAR_EXP

    // **** 캐릭터 기본 레벨(CHAR_DEFAULT_LEV) 의 경우 몬스터일 경우만 tb_CHAR 테이블의 데이터를 참조합니다. 
    // ++++ 유저 캐릭터의 경우 계정의 캐릭터가 마지막으로 플레이 했을 때의 저장됐던 레벨 정보를 참조합니다.

    //	 = ((캐릭터 기본 레벨 + 캐릭터 경험치상수 1) * (캐릭터 기본 레벨 + 캐릭터 경험치상수 2) * 캐릭터 경험치상수 3) * 캐릭터 기본 레벨 * 캐릭터 경험치상수 4		
    //	 = ((CHAR_DEFAULT_LEV + CHAR_EXPCONST_1) * (CHAR_DEFAULT_LEV + CHAR_EXPCONST_2) * CHAR_EXPCONST_3) * CHAR_DEFAULT_LEV * CHAR_EXPCONST_4		


	static public int CHAR_EXP(int CHAR_DEFAULT_LEV,
                        float CHAR_EXPCONST_1,
                        float CHAR_EXPCONST_2,
                        float CHAR_EXPCONST_3,
                        float CHAR_EXPCONST_4)
    {



        float fa = (float)((float)CHAR_DEFAULT_LEV + CHAR_EXPCONST_1);
        float fb = (float)((float)CHAR_DEFAULT_LEV + CHAR_EXPCONST_2);
        float fc = CHAR_EXPCONST_3;
        float fd = fa * fb * fc;
        float ff = fd * CHAR_DEFAULT_LEV * CHAR_EXPCONST_4;

		return (int)(ff);
		
	}

    // 캐릭터 내장 경험치	CHAR_INNR_EXP -----------------------------------------------------------------------------------------------------------------

    // tb_CHAR_EXP 테이블의 경험치 관련 상수들을 참조하기 위한 [경험치 구간 아이디] 구하는 수식 = (int)(캐릭터 현재 레벨 / 3 + 0.2)
    // <예제> 40레벨 캐릭터의 tb_CHAR_EXP 테이블의 [경험치 구간 아이디] = (int)(40 / 3 + 0.2) = 14
    
    //	[한글 칼럼명]	            [영문 칼럼 명 (변수명)]	    [테이블] 혹은 데이터 출처
    //	캐릭터 기본 레벨	        CHAR_DEFAULT_LEV	        tb_CHAR
    //	캐릭터 내장 경험치상수 1	CHAR_INN_EXPCONST_1	        tb_CHAR_EXP
    //	캐릭터 내장 경험치상수 2	CHAR_INN_EXPCONST_2	        tb_CHAR_EXP
    //	캐릭터 내장 경험치상수 3	CHAR_INN_EXPCONST_3	        tb_CHAR_EXP
    //	캐릭터 내장 경험치상수 4	CHAR_INN_EXPCONST_4	        tb_CHAR_EXP

    // **** 캐릭터 기본 레벨(CHAR_DEFAULT_LEV) 의 경우 몬스터일 경우만 tb_CHAR 테이블의 데이터를 참조합니다. 
    // ++++ 유저 캐릭터의 경우 계정의 캐릭터가 마지막으로 플레이 했을 때의 저장됐던 레벨 정보를 참조합니다.

    //	 = ((캐릭터 기본 레벨 + 캐릭터 내장 경험치상수 1) * (캐릭터 기본 레벨 + 캐릭터 내장 경험치상수 2) * 캐릭터 내장 경험치상수 3) * 캐릭터 기본 레벨 * 캐릭터 내장 경험치상수 4		
    //	 = ((CHAR_DEFAULT_LEV + CHAR_INN_EXPCONST_1) * (CHAR_DEFAULT_LEV + CHAR_INN_EXPCONST_2) * CHAR_INN_EXPCONST_3) * CHAR_DEFAULT_LEV * CHAR_INN_EXPCONST_4		


	static public int CHAR_INNR_EXP(   int CHAR_DEFAULT_LEV,
                                float CHAR_INN_EXPCONST_1,
                                float CHAR_INN_EXPCONST_2,
                                float CHAR_INN_EXPCONST_3,
                                float CHAR_INN_EXPCONST_4)
    {



        float fa = (float)((float)CHAR_DEFAULT_LEV + CHAR_INN_EXPCONST_1);
        float fb = (float)((float)CHAR_DEFAULT_LEV + CHAR_INN_EXPCONST_2);
        float fc = CHAR_INN_EXPCONST_3;
        float fd = fa * fb * fc;
        float ff = fd * CHAR_DEFAULT_LEV * CHAR_INN_EXPCONST_4;

        return (int)ff;

    }

    //  -----------------------------------------------------------------------------------------------------------------



}
