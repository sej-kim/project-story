using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using STORY_ENUM;
using STORY_GAMEDATA;

public class Character : ObjectBase
{
	public delegate void StatusMachineFuntion();
	StatusMachineFuntion StatusMachineFun;



	protected DamageModification _DamageModification;

    // 오브젝트 무브 클레스
    protected CharMovement  _Movement;

    private float _fAtOldTime 		= 0.0f;
	private float _fSkillCollTime 	= 3.0f;

    private Vector3         _TargetPos;
    private int             _TargetInstanceID = 0;

	public Dictionary <string, Transform> _EffectPosDictionary;

	static public bool _bIsNextCahpter = false;
	bool _SkillPause = false;

	float _NextMovePosX;

	int _NextSkillIndex = 0;


    void Start ()
    {
		_fSkillCollTime = Random.Range( 5, 10 );
        initialization();
	}

    override protected void initialization()
    {
        // 베이스 클레스에서 메모리 할당을 하기 때문에 먼져 호출 해줘야 한다.
        base.initialization();

        _AniController  	= gameObject.GetComponentInChildren<Animator>();
        _Movement       	= gameObject.AddComponent<CharMovement>();
		_DamageModification = gameObject.AddComponent<DamageModification> ();

        _TargetPos = new Vector3( 0, 0, 0 );

		int nPosSize = (int)eEffectPos.MAX_SIZE;

		_EffectPosDictionary = new Dictionary <string, Transform>();

		for( int i_1 = 0; i_1 < nPosSize; ++i_1 )
		{
			eEffectPos pos = (eEffectPos)i_1;

			Transform[] tempTransforms = gameObject.GetComponentsInChildren<Transform>(); 

			//Debug.Log("child pos : " + pos.ToString()); 

			foreach (Transform child in tempTransforms) 
			{ 
				if (child.name.Contains(pos.ToString())) 
				{ 
					_EffectPosDictionary.Add( pos.ToString(), child ); 
					//Debug.Log("child pos : " + child.name); 
					break; 
				} 
			}
		}
     }

	override public bool initWithObjectData( OBJECT_DEFAULT_DATA data )                                       
	{
		if (data.ObjectType == OBJECT_TYPE.PLAYER) 
		{
			StatusMachineFun = new StatusMachineFuntion( PlayerStatusMachine );
		} 
		else 
		{
			StatusMachineFun = new StatusMachineFuntion( MonsterStatusMachine );
		}

		return base.initWithObjectData(data);
	}

	void Update()
	{
		if (_IsPause) 
		{
			return;
		}

		if( OBJECT_STATE.MOVE == ObjStatus )
		{
			_Movement.Move(_TargetPos, ObjDefaultData.fMoveSpeed, Time.deltaTime);
		}
		
		if( OBJECT_STATE.ATTACK != ObjStatus )
		{
			_fAtOldTime += Time.deltaTime;
		}

		if (ObjDefaultData.ObjectType == OBJECT_TYPE.MONSTER) 
		{
			if (_fSkillCollTime > 0)
			{
				_fSkillCollTime -= Time.deltaTime;
			}
		}
	}
     
    /// <summary>
    /// 타겟팅바라보게 회전
    /// </summary>
    /// <param name="tPos">타겟팅 위치</param>
    void SetTargetLooakRotation(Vector3 tPos)
    {
        Vector3 Dir     = _TargetPos - transform.position;
        Quaternion drot = Quaternion.LookRotation(Dir);
        transform.rotation = drot;
    }


	GameObject GetTargetFind()
	{
//		GameObject TargetObject = GameManager.Instance.GetObject (ObjDefaultData.TargetObjType.ToString (), _TargetInstanceID);
//
//		if (TargetObject == null) 
//		{
//			_TargetInstanceID = 0;
//
//			TargetObject = GameManager.Instance.GetDistTargetObject (ObjDefaultData.TargetObjType.ToString (), transform.position);
//			//SetTargetLooakRotation(_TargetPos);
//		}

		GameObject TargetObject = GameManager.Instance.GetDistTargetObject (ObjDefaultData.TargetObjType.ToString (), transform.position);

		return TargetObject;

	}

	int IsAttack( Vector3 pos )
	{
		if( _fAtOldTime < ObjDefaultData.fAtDelay )
		{
			return 2;
		}


		float fDist = Mathf.Abs(transform.localPosition.x - pos.x);
		return _ObjDefaultData.fAtDist > fDist ? 0 : 1;
	} 

	void MonsterStatusMachine()
	{
		GameObject TargetObject = GetTargetFind();
		
		if( TargetObject != null )
		{
			_TargetPos          = TargetObject.transform.position;
			_TargetInstanceID   = TargetObject.GetInstanceID();
			
			OBJECT_STATE state = ObjStatus;
			
			switch( ObjStatus )
			{
			case OBJECT_STATE.MOVE:
			{
				if( IsAttack( _TargetPos ) == 0 )
				{
					state = OBJECT_STATE.ATTACK;
				}
			}
				break;
			case OBJECT_STATE.NONE:
			{
				int nResult =  IsAttack( _TargetPos );
				
				if( nResult == 0 )
				{
					if( _fSkillCollTime <= 0 && GameManager.IsGlovalSkill == false )
					{
						_fSkillCollTime = Random.Range( 3, 10 );
						state = OBJECT_STATE.SKILL;
					}
					else
					{
						state = OBJECT_STATE.ATTACK;
					}
				}
				else if( nResult == 1 )
				{
					state = OBJECT_STATE.MOVE;
				}
			}
				break;
			}
			
			
			ChangeAnimation(state, Random.Range(1, _SkillCount + 1 ) );
		}
	}

	bool IsSkill()
	{
		return ((_NextSkillIndex > 0) && (GameManager.IsGlovalSkill == false));
	}

	void PlayerStatusMachine()
	{
		GameObject TargetObject = GetTargetFind();
		
		OBJECT_STATE state = OBJECT_STATE.NONE;
		
		if( TargetObject != null )
		{
			_TargetPos          = TargetObject.transform.position;
			_TargetInstanceID   = TargetObject.GetInstanceID();
			
			state = ObjStatus;
			
			switch( ObjStatus )
			{
			case OBJECT_STATE.ATTACK:
			{
				if( _NextSkillIndex > 0 )
				{
					state = OBJECT_STATE.SKILL;
				}
			}
				break;
			case OBJECT_STATE.MOVE:
			{
				if( IsAttack( _TargetPos ) == 0 )
				{
					if( IsSkill() )
					{
						state = OBJECT_STATE.SKILL;
					}
					else
					{
						state = OBJECT_STATE.ATTACK;
					}
				}
			}
				break;
			case OBJECT_STATE.NONE:
			{
				int nResult =  IsAttack( _TargetPos );
				
				if( nResult == 0 )
				{
					if( IsSkill() )
					{
						state = OBJECT_STATE.SKILL;
					}
					else
					{
						state = OBJECT_STATE.ATTACK;
					}
					
				}
				else if( nResult == 1 )
				{
					state = OBJECT_STATE.MOVE;
				}
			}
				break;
			}

			ChangeAnimation(state, _NextSkillIndex );
		}
		

	}
    /// <summary>
    ///  실시간 체크 해야 하는 상태( 이동, 공격 )
    /// </summary>
    /// <returns></returns>
    IEnumerator StatusMachine()
    {
        while (ObjStatus != OBJECT_STATE.DIE)
        {
			yield return new WaitForSeconds(0.05f);

			StatusMachineFun();
        }

		//yield return StartCoroutine ("StatusMachine");
        //yield return null;
    }

	/// <summary>
	///  
	/// </summary>
	/// <returns></returns>
	IEnumerator MoveCoroutine()
	{
		while (_NextMovePosX >= transform.localPosition.x)
		{
			//Debug.Log( "_TargetPos.x : " + _NextMovePosX.ToString() + "  transform.localPosition.x : " + transform.localPosition.x.ToString() );
			yield return new WaitForSeconds(0.1f);			
		}

		GameManager.Instance.onMoveComplete();

		StartCoroutine ("StatusMachine");

		yield return null;
	}



	public void MoveObject( Vector3 pos, float time )
	{
		_AniController.SetInteger("AttackSkill", 0 );
		if (_SkillPause) 
		{
			
			EndStopMotion(0);
		}


		ChangeAnimation (OBJECT_STATE.MOVE);


		_TargetInstanceID 	= 0;
		_TargetPos 			= pos;


		_NextMovePosX = pos.x - 0.2f;

		_bIsNextCahpter = true;
		SetTargetLooakRotation(pos);
		gameObject.transform.localScale = new Vector3( 1, 1, 1 );

		StartCoroutine ("MoveCoroutine");
		//_Movement.Move(pos);
	}

	int GetTotalDamage( Character target )
	{
		if( _DamageModification.IsAttackMiss( ObjDefaultData.DamageType, this, target ) )
		{
			return 0;
		}

		int nDefault = _DamageModification.GetDefaultDamage (ObjDefaultData.DamageType, this, target);
		int nCri = _DamageModification.GetCriDamage (ObjDefaultData.DamageType, this, target);
		int nHit = _DamageModification.GetHitDamage (ObjDefaultData.DamageType, this, target);


		//Debug.Log ( "uID : " + this.ObjDefaultData.uID.ToString() +  " De : " + nDefault.ToString () + " Cri : " + nCri.ToString () + " Hit : " + nHit.ToString ());

		int nTotalDamage = nDefault + nCri + nHit;

		return nTotalDamage;
	}



	virtual public void EndStopMotion( int nIndex )
	{
//		if ( GameManager.IsGlovalSkill == false ) 
//		{
//			return;
//		}

		//Debug.Log ("EndStopMotion : " + gameObject.name );

		GameManager.IsGlovalSkill = false;
		_SkillPause = false;

		GameManager.Instance.ObjectPauseResum( gameObject, "Resum" );
		gameObject.transform.localScale = new Vector3 (1, 1, 1);
		
		GameManager.Instance.CameraColorEffect (_SkillPause, gameObject);
	}

	virtual public void StartStopMotion()
	{
		if (GameManager.IsGlovalSkill == true ) 
		{
			return;
		}

		Debug.Log ("StartStopMotion: " + gameObject.name);

		GameManager.IsGlovalSkill = true;
		_SkillPause = true;

		GameManager.Instance.ObjectPauseResum( gameObject, "Pause" );
		gameObject.transform.localScale = new Vector3( 1.4f, 1.4f, 1.4f );

		GameManager.Instance.CameraColorEffect (_SkillPause, gameObject);

		GameManager.Instance.CreatePauseEffect ();
	}

    /// <summary>
    /// 공격시 호출
    /// </summary>
	virtual public IEnumerator HitPoint( int nIndex )
    {
		yield return null;

        GameObject TargetObject = GameManager.Instance.GetObject(_ObjDefaultData.TargetObjType.ToString(), _TargetInstanceID );

        if ( TargetObject != null )
        {
			EFFECT_DATA dat = GameDataManagerChar.Instance.GetEffectData (nIndex);
			
			Character ObjBase = TargetObject.GetComponent<Character>();
			//Debug.Log("Attack :" + _strTargetTag + "_TargetInstanceID : " + _TargetInstanceID.ToString());
			
			if (dat.strPath.Length > 1) 
			{
				
				//Debug.Log("Effect Name :" + dat.strPath);
				GameObject ObjTemp = Resources.Load("Prefabs/Effect/Object/" + dat.strPath, typeof(GameObject)) as GameObject;
				//ObjTemp = Instantiate(ObjTemp, TargetObject.transform.position, gameObject.transform.rotation) as GameObject;
				
				if( nIndex == 42 )
				{
					ObjTemp = Instantiate(ObjTemp, ObjBase._EffectPosDictionary[eEffectPos.Root_Dummy.ToString()].position, ObjBase._EffectPosDictionary[eEffectPos.Root_Dummy.ToString()].rotation ) as GameObject;
					
				}
				else
				{
					ObjTemp = Instantiate(ObjTemp, ObjBase._EffectPosDictionary[eEffectPos.FXDummy_breast.ToString()].position, ObjBase._EffectPosDictionary[eEffectPos.FXDummy_breast.ToString()].rotation ) as GameObject;
					
				}
				
				GameObject.Destroy(ObjTemp,5);
			}
			
			if (dat.nScreenFx == 1) 
			{
				GameManager.Instance.RunCameraVibrate();
			}
			
			int nTotalDamage = GetTotalDamage (ObjBase);
			
			if (nTotalDamage > 0) 
			{
				ObjBase.SetDamage(nTotalDamage, this);
				//Debug.Log( " Name : " + gameObject.name + "TotalDamage : " + nTotalDamage.ToString());
			}
        }

		//Debug.Log( " Name : " + gameObject.name + "TotalDamage : " + nTotalDamage.ToString());
    }


	virtual public IEnumerator CreateEffect( int nIndex )
	{
		yield return null;

		EFFECT_DATA dat = GameDataManagerChar.Instance.GetEffectData (nIndex);

		//Debug.Log("Pos Name :" + dat.strPos);
		//Debug.Log("Effect Name :" + dat.strPath);


		GameObject Obj = Resources.Load("Prefabs/Effect/Object/" + dat.strPath, typeof(GameObject)) as GameObject;
		GameObject ObjTemp = Instantiate(Obj, _EffectPosDictionary[dat.strPos].position, Quaternion.identity) as GameObject;

		ObjTemp.transform.parent = _EffectPosDictionary[dat.strPos];
		ObjTemp.transform.localPosition = Obj.transform.localPosition;
		ObjTemp.transform.localRotation = Obj.transform.localRotation;
		//ObjTemp.transform.localScale = gameObject.transform.localScale;


		GameObject.Destroy(ObjTemp,5);

		if (dat.nType == 2) 
		{
			GameObject TargetObject = GameManager.Instance.GetObject(_ObjDefaultData.TargetObjType.ToString(), _TargetInstanceID);

			if( TargetObject )
			{
				Character Defender = TargetObject.GetComponent<Character>();

				Arrow ar = ObjTemp.AddComponent<Arrow>();
				ar.Fire(Defender._EffectPosDictionary[eEffectPos.FXDummy_breast.ToString()].position);
				Defender.SetDamage(  GetTotalDamage( Defender ), Defender );
			}
		} 
	}

    /// <summary>
    /// 데미지 입었을때 호출되는 함수
    /// tode : 모든 함수 리턴 값은 bool 아니면 지정해 놓은  enum 타입이용 
    /// </summary>
    /// <param name="nDamage"> 데미지 </param>
    /// <returns> 데미지 후 상태  </returns>
    virtual public OBJECT_STATE SetDamage(int nDamage, Character Target)
    {
        if (GameManager.Instance.bGameOver)
        {
            return OBJECT_STATE.NONE;
        }

        if (ObjStatus == OBJECT_STATE.DIE)
        {
            return OBJECT_STATE.DIE;
        }

		CreateDamageFont (nDamage, _EffectPosDictionary[eEffectPos.FXDummy_name.ToString()].position);

		ObjectHealth hpth = _ObjectHealth.GetComponent<ObjectHealth> ();


		OBJECT_STATE ResultState = hpth.SetDamage(nDamage);

        //Debug.Log( "Name :" + gameObject.name + "Datage : " + nDamage.ToString() );

        if (ResultState == OBJECT_STATE.DIE)
        {
            SetDie();
        }
        else
        {
			if( ( Random.Range(0, 1000) < 100 ) && _SkillPause == false )
            {
                ChangeAnimation(OBJECT_STATE.BEATEN);
            }
        }

        return ResultState;
    }

    /// <summary>
    /// 애니메이션 모션 끝난후 호출
    /// </summary>
    /// <param name="state"> 끝난 모션 </param>
	virtual public void EndAnimation( string OldMotion )
    {
		if (_SkillPause == true ) 
		{
			EndStopMotion(0);
		}

		if (ObjStatus == OBJECT_STATE.SKILL ) 
		{
			_AniController.SetInteger("AttackSkill", 0 );
		}

		if( OldMotion.Equals( "die" ) )
		{
			GameObject target = GameManager.Instance.GetDistTargetObject (ObjDefaultData.TargetObjType.ToString (), transform.position);

			if( target )
			{
				GameObject ObjTemp = Resources.Load("Prefabs/Effect/Object/fx_charcter_die_bullet", typeof(GameObject)) as GameObject;
				ObjTemp = Instantiate(ObjTemp, this.transform.position, Quaternion.identity) as GameObject;
				
				DieSoulEffect ef = ObjTemp.GetComponent<DieSoulEffect>();
				ef.target = target;

				GameObject Obj = Resources.Load("Prefabs/Effect/Object/fx_charcter_die_a", typeof(GameObject)) as GameObject;
				ObjTemp = Instantiate(Obj, transform.position, Quaternion.identity) as GameObject;


				ObjTemp.transform.localRotation = Obj.transform.localRotation;

				//gameObject.SetActive( false );
				return;
			}
		}

		if (_bIsNextCahpter == false) 
		{
			ChangeAnimation(OBJECT_STATE.NONE);
		}
    }

	public void SetVitoryAnimation()
	{
		StopCoroutine ("StatusMachine");
		_AniController.SetInteger("AttackSkill", 0 );


		if (_SkillPause) 
		{
			EndStopMotion(0);
		}


		gameObject.transform.rotation = Quaternion.LookRotation( Vector3.back );
		_AniController.SetBool("IsFinish", true);
	}

	public bool SetSkill( int nIndex )
	{
		if ( ( _NextSkillIndex > 0 ) || ( GameManager.IsGlovalSkill == true ) ) 
		{
			return false;
		}


		if (GetTargetFind () == null) {
			return false;
		}

		_NextSkillIndex = nIndex;

		return true;
	}

    /// <summary>
    /// 상태에 따른 모션 변화
    /// </summary>
    /// <param name="state"> 변화 모션 </param>
	override protected void ChangeAnimation( OBJECT_STATE state, int nValue = 0 )
    {
        if (ObjStatus == state || ObjStatus == OBJECT_STATE.DIE)
        {
            return;
        }

		//Debug.Log( "  name :" + gameObject.name + "  status :" + state.ToString() + "  Value :" + nValue.ToString());

		ObjStatus = state;

        switch (state)
        {
            case OBJECT_STATE.NONE:
                {
                    _AniController.SetBool( "IsRun", false );
                    _AniController.SetBool("IsAttack", false );
					_AniController.SetBool("IsDamage", false);
                }
                break;
            case OBJECT_STATE.BEATEN:
                {
                    _AniController.SetBool("IsDamage", true);
                }
                break;
            case OBJECT_STATE.MOVE:
                {
                    _AniController.SetBool("IsRun", true);
                    _AniController.SetBool("IsAttack", false );
                }
                break;
            case OBJECT_STATE.ATTACK:
                {

					SetTargetLooakRotation(_TargetPos);
                    _AniController.SetBool("IsRun", false);
                    _AniController.SetBool("IsAttack", true);

					_fAtOldTime 	= 0;
                }
                break;
			case OBJECT_STATE.SKILL:
			{
				ResetSkillCoolTime( _NextSkillIndex );
				_NextSkillIndex = 0;

				_AniController.SetBool( "IsRun", false );
				_AniController.SetBool("IsAttack", false );
				_AniController.SetBool("IsDamage", false);
				_AniController.SetInteger("AttackSkill", nValue );

				//Debug.Log( "SkillIndex : " + _NextSkillIndex.ToString());

				//Debug.Log(state.ToString());
			}
			break;
            case OBJECT_STATE.DIE:
                {
                    //_Movement.Stop();
					//gameObject.transform.localScale = new Vector3( 1, 1, 1 );
                    _AniController.SetBool("IsDie", true);
                    _AniController.SetBool("IsRun", false);
                    _AniController.SetBool("IsAttack", false);
                    //Debug.Log(state.ToString());
                }
                break;

        }
    }
}
