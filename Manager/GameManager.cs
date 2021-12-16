using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using STORY_ENUM;
using STORY_GAMEDATA;

public class GameManager : MonoBehaviour
{
	IMAGE_PATH _ImagePath;

	Vector3[] _Montr;
	Vector3[] _Pctr;

	static DUNGEON_DATA 				_sDungeonData;
	DUNGEON_STAGE_LIST_DATA 			_StageListData;

	Dictionary<OBJECT_TYPE, int> 		_ObjectCount;
	List< DUNGEON_REWARD_ITEM_DATA > 	_RewardItemList;
	DUNGEON_REWARD_ITEM_LIST_DATA 		_RewardItemData;

	int _nChapter 		= 3;
	int _nCurrentChaper = 1;

    bool _bGameOver 	= false;

	public GameObject[] _Camera;
	GameObject 			_mainCamera;


	bool _bRunCameraVibrate 	= false;
	bool _bRunGlowEffect 		= false;

	public float VibrateValue = 0.3f;
	public float VibrateTime = 0.3f;

	public float _CameraDelayTime = 0;
	public float _CameraMoveTime = 0;

	public static bool IsGlovalSkill = false;

	public static GameManager SingleInstance { get; set; }



	#region SingleTon
	public static GameManager Instance
	{
		get
		{
			if (SingleInstance == null)
			{
				SingleInstance = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
				
				if (SingleInstance == null)
				{
					GameObject Container = new GameObject();
					
					SingleInstance = Container.AddComponent(typeof(GameManager)) as GameManager;
					
					Container.name = SingleInstance.ToString();
				}
			}
			
			return SingleInstance;
		}
	}
	#endregion

	// Use this for initialization
	void Start () {

		//Debug.Log("GameManager Start");

		_ImagePath = new IMAGE_PATH ();
		_nCurrentChaper = 1;
		_ObjectCount = new Dictionary<OBJECT_TYPE, int> ();

		_ObjectCount.Add (OBJECT_TYPE.PLAYER, 0);
		_ObjectCount.Add (OBJECT_TYPE.MONSTER, 0);

		_StageListData = GameDataManagerDungeon.Instance.GatStageListData (_sDungeonData.nStageID);
		//_StageListData = GameDataManagerDungeon.Instance.GatStageListData (100001);
		//Debug.Log ( "nStageID" + _StageListData.DunGeonStageList[0].nMobGroupID.ToString());

		SetRewardItemList ();

		_nChapter = _StageListData.DunGeonStageList.Count;

		_mainCamera = GameObject.Find( "CameraPos1");
		//_mainCamera = Camera.main;
		SetStagePos (_nCurrentChaper);

		Character._bIsNextCahpter = false;

		StartCoroutine(CreatePc ( true ));
		StartCoroutine (CreateMonster ( true ));

		_Camera [1].SetActive (false);
	}


	static public DUNGEON_DATA DungeonData
	{
		get
		{
			return _sDungeonData;
		}
		set 
		{
			_sDungeonData = value;
		}
	}

	public DUNGEON_REWARD_ITEM_LIST_DATA RewardItemData
	{
		get
		{
			return _RewardItemData;
		}
	}

	public List< DUNGEON_REWARD_ITEM_DATA > RewardItemList 
	{
		get
		{
			return _RewardItemList;
		}
	}

	void SetRewardItemList()
	{
		_RewardItemData = new DUNGEON_REWARD_ITEM_LIST_DATA ();


		_RewardItemList = new List< DUNGEON_REWARD_ITEM_DATA > ();


		_RewardItemData = GameDataManagerDungeon.Instance.GetDungeonRewardItemListData (DungeonData.nRewardItemID);

		for (int i_1 = 0; i_1 < _RewardItemData.ItemDataList.Count; ++i_1) 
		{
			//Debug.Log( "Rate : " + RewardList.ItemDataList[i_1].nRate.ToString() );

			if( Random.Range( 0, 10000 ) <= _RewardItemData.ItemDataList[i_1].nRate )
			{
				_RewardItemList.Add( _RewardItemData.ItemDataList[i_1] );
				//Debug.Log( "RewardItem : " + RewardList.ItemDataList[i_1].nID.ToString() );
			}
		}
	}


	void StartObjectCoroutine ( string tag, string funtion )
	{
		GameObject[] Objects = GameObject.FindGameObjectsWithTag( tag );
		
		foreach( GameObject obj in Objects )
		{
			//Debug.Log( obj.name );
			Character charScript = obj.GetComponent< Character >();

			if( charScript.ObjStatus != OBJECT_STATE.DIE )
			{
				charScript.StartCoroutine( funtion );
			}
		}
	}


	void StopObjectCoroutine ( string tag, string funtion )
	{
		GameObject[] Objects = GameObject.FindGameObjectsWithTag( tag );
		
		foreach( GameObject obj in Objects )
		{
			Character charScript = obj.GetComponent< Character >();

			if( charScript.ObjStatus != OBJECT_STATE.DIE )
			{
				charScript.StopCoroutine( funtion );
			}
		}
	}

	/// <summary>
	/// Player Object create
	/// </summary>
	/// <returns><c>true</c>, if pc was created, <c>false</c> otherwise.</returns>
	/// <param name="dat">Dat.</param>
	public IEnumerator CreatePc( bool bStatusMachine = false )
	{
		//yield return CreatePc( bStatusMachine );


		List< OBJECT_DEFAULT_DATA > ObjList = Sqlprocess.Instance.LoadUserCharDataList ( "SELECT* FROM userCharData WHERE BattleDeck > 0" );

		for (int i_1 = 0; i_1 < ObjList.Count; ++i_1) 
		{
			OBJECT_DEFAULT_DATA objData = ObjList[i_1];

//			if( objData == null )
//			{
//				Debug.Log( "null" );
//			}

			//Debug.Log( objData.nPos.ToString() + " : " + nPosIndex.ToString() );
			//Debug.Log( objData.strImage + "_Mecanim" );
			GameObject ObjTemp = Resources.Load( _ImagePath.CHAR + objData.strImage + "_Mecanim", typeof(GameObject)) as GameObject;
			ObjTemp = Instantiate(ObjTemp, _Pctr[objData.nBattleDeck - 1], Quaternion.LookRotation( Vector3.right )) as GameObject;
			//ObjTemp.transform.localPosition = _Pctr[nPosIndex];


			objData.StatData 		= Sqlprocess.Instance.LoadUserCharStatData( objData.nRow );
			objData.ObjectType 		= OBJECT_TYPE.PLAYER;
			objData.TargetObjType 	= OBJECT_TYPE.MONSTER;
			
			ObjectBase baseScript = ObjTemp.GetComponent< ObjectBase >();
			baseScript.initWithObjectData(objData);

			baseScript.CreateSkillUI ( i_1 );

			if (bStatusMachine == true ) 
			{
				baseScript.StartCoroutine ( "StatusMachine" );
			}

		}

		_ObjectCount[OBJECT_TYPE.PLAYER] = ObjList.Count;

		yield return null;
	}

	/// <summary>
	/// /
	/// </summary>
	/// <returns><c>true</c>, if monster was created, <c>false</c> otherwise.</returns>
	public IEnumerator CreateMonster( bool bStatusMachine = false )
	{
		//Debug.Log (_sDungeonData.nID.ToString () + " " + _sDungeonData.nStageID.ToString ());
		//yield return CreateMonster ( bStatusMachine );



		//Debug.Log ("CreateMonster");

		//DUNGEON_MOB_GROUP_LIST_DATA list = GameDataManagerDungeon.Instance.GetDungeonMobGroupList (50000001);
		DUNGEON_MOB_GROUP_LIST_DATA list = GameDataManagerDungeon.Instance.GetDungeonMobGroupList (_StageListData.DunGeonStageList[_nCurrentChaper - 1].nMobGroupID);
		//Debug.Log (_StageListData.DunGeonStageList [_nCurrentChaper - 1].nMobGroupID.ToString ());

		for (int i_1 = 0; i_1 < list.DungeonMobGroupList.Count; ++i_1) 
		{
			DUNGEON_MOB_GROUP_DATA mob = list.DungeonMobGroupList[i_1];

			OBJECT_DEFAULT_DATA obj = GameDataManagerChar.Instance.GetCharInfoData( mob.nCharID );
			obj.StatData 	= GameDataManagerChar.Instance.GetCharStatData( mob.nCharID );

			//Debug.Log( obj.strImage );
			GameObject ObjTemp = Resources.Load( _ImagePath.CHAR + obj.strImage + "_Mecanim", typeof(GameObject)) as GameObject;
			ObjTemp = Instantiate(ObjTemp, _Montr[i_1], Quaternion.LookRotation( Vector3.left ) ) as GameObject;


			obj.ObjectType = OBJECT_TYPE.MONSTER;
			obj.TargetObjType = OBJECT_TYPE.PLAYER;


			ObjectBase baseScript = ObjTemp.GetComponent< ObjectBase >();
			baseScript.initWithObjectData( obj );

			if (bStatusMachine == true ) 
			{
				baseScript.StartCoroutine ( "StatusMachine" );
			}

		}

		_ObjectCount[OBJECT_TYPE.MONSTER] = list.DungeonMobGroupList.Count;

		yield return null;
	}


	/// <summary>
	/// /
	/// </summary>
	/// <param name="nStage">N stage.</param>
	public void SetStagePos( int nStage )
	{
		GameObject stagePos = GameObject.Find ("StagePos" + nStage.ToString());
		
		if (stagePos == null) {
			//Debug.Log( "SetStagePos" );
			return;		
		}
		
		Transform[] trs =  stagePos.GetComponentsInChildren<Transform> ();

		//Debug.Log (trs.Length.ToString ());

		_Pctr 	= new Vector3[5];
		_Montr 	= new Vector3[5];
		
		int nPcIndex 	= 0;
		int nMonIndex 	= 0;
		
		for (int i_1 = 0; i_1 < trs.Length; ++i_1 ) 
		{
			if( trs[i_1].tag.Equals( "P_POSITION" ) )
			{
				_Pctr[nPcIndex] = trs[i_1].position;
				++nPcIndex;
			}
			else if( trs[i_1].tag.Equals( "M_POSITION" ) )
			{
				//_Montr[nMonIndex] = trs[i_1];
				_Montr[nMonIndex] = trs[i_1].position;
				++nMonIndex;
			}
		}
	}


	/// <summary>
	/// /
	/// </summary>
	/// <value><c>true</c> if b game over; otherwise, <c>false</c>.</value>
    public bool bGameOver
    {
        get
        {
            return _bGameOver;
        }
        set
        {
            _bGameOver = value;
        }
    }


	/// <summary>
	/// /
	/// </summary>
	/// <param name="type">Type.</param>
    public void GameOver( OBJECT_TYPE type )
    {
        _bGameOver = false;

        if (type == OBJECT_TYPE.PLAYER)
        {
			Invoke("Defeat", 5);
            //Defeat();
        }
        else
        {
			//NextChaper();
            Victory();
        }
    }

	/// <summary>
	/// Victory this instance.
	/// </summary>
    public void Victory()
    {
		SetSkillSlotVisible (false);
		FactoryManager.Instance.CreatePopUp(POPUP_TYPE.UIvictory);
	}
	
	/// <summary>
	/// /
	/// </summary>
    public void Defeat()
    {
		FactoryManager.Instance.CreatePopUp(POPUP_TYPE.DefeatPopUp);
	}
	

	/// <summary>
	/// /
	/// </summary>
	/// <returns><c>true</c>, if chaper was cleared, <c>false</c> otherwise.</returns>
	public bool ClearChaper()
	{
		++_nCurrentChaper;

		if (_nCurrentChaper > _nChapter) 
		{
			return true;
		}

		return false;
	}

	public IEnumerator SetVictoryMotion( string strTag )
	{
		//yield return new WaitForSeconds( 2.0f );

		//yield return yield return null;

		GameObject[] Objects = GameObject.FindGameObjectsWithTag( strTag );
		
		foreach( GameObject obj in Objects )
		{
			Character charScript = obj.GetComponent< Character >();
			
			if( charScript.ObjStatus == OBJECT_STATE.DIE )
			{
				continue;
			}

			charScript.SetVitoryAnimation();
		}

		yield return null;
	}

	/// <summary>
	/// /
	/// </summary>
	public void NextChaper()
	{
		//Debug.Log ("NextChaper");
		//StopObjectCoroutine ( "PLAYER", "StatusMachine" );

		if (ClearChaper ()) 
		{
			StartCoroutine( "SetVictoryMotion", "PLAYER" );
			Invoke("Victory", 5);
			return;
		}

		Invoke("SettingNextChapter", 3);
	}

	void SettingNextChapter()
	{
		//Debug.Log ("SettingNextChapter");

		//DestroyObjectTag ("MONSTER");
		SetStagePos (_nCurrentChaper);

		CameraAction (0);
		
		StartCoroutine( CreateMonster () );

		//CreateMonster ();
		NextChpterMove ();
		
		Character._bIsNextCahpter = true;
	}

	void NextChpterMove ()
	{
		GameObject[] Objects = GameObject.FindGameObjectsWithTag( "PLAYER" );

		int nPosIndex = 0;
		foreach( GameObject obj in Objects )
		{
			Character charScript = obj.GetComponent< Character >();
			
			if( charScript.ObjStatus != OBJECT_STATE.DIE )
			{
				charScript.MoveObject( _Pctr[nPosIndex], 1.0f );
			}

			++nPosIndex;
		}
	}


	public void onMoveComplete()
	{
		Character._bIsNextCahpter = false;
		StartObjectCoroutine ( "MONSTER", "StatusMachine" );
		StartObjectCoroutine ( "PLAYER", "StatusMachine" );
	}

	void CameraAction( int nType )
	{
		string strName = "CameraPos" + _nCurrentChaper.ToString ();

		GameObject tr = GameObject.Find (strName) as GameObject;

		if (tr == null) {
			return;
		}

		Hashtable tweenParam = new Hashtable();
		
		tweenParam.Add ("position", tr.transform.position);
		tweenParam.Add ("time", _CameraMoveTime);
		tweenParam.Add ("delay", _CameraDelayTime);
		tweenParam.Add ("EaseType", iTween.EaseType.easeInSine);

		iTween.MoveTo (_mainCamera.gameObject, tweenParam);

		tweenParam = new Hashtable();
		
		tweenParam.Add ("rotation", tr.transform.rotation.eulerAngles);
		tweenParam.Add ("time", _CameraMoveTime);
		tweenParam.Add ("delay", _CameraDelayTime);
		tweenParam.Add ("EaseType", iTween.EaseType.easeInSine);
		
		iTween.RotateTo (_mainCamera.gameObject, tweenParam);
	}

    // 가까운 위치에 있는 타겟을 얻어 온다.
    public GameObject GetDistTargetObject( string strName, Vector3 position )
    {
        // PLAYER , MONSTER 필드 오브젝트 리스트 
        GameObject[] Objects = GameObject.FindGameObjectsWithTag(strName);

        if (Objects.Length <= 0)
        {
            return null;
        }
        int nSize               = Objects.Length;

        GameObject objResult    = null;

        float fDist = 10000;

        for (int i_1 = 0; i_1 < nSize; ++i_1)
        {
            GameObject ObjTemp = Objects[i_1];

            ObjectBase ObjBase = ObjTemp.GetComponent<ObjectBase>();

            if (ObjBase == null)
            {
                continue;
            }

            if (ObjBase.ObjStatus == OBJECT_STATE.DIE)
            {
                continue;

            }

            float fDistTemp = Vector3.Distance(position, ObjTemp.gameObject.transform.position);

            if ( fDist > fDistTemp )
            {
                objResult = ObjTemp;
                fDist = fDistTemp;
            }
        }

        return objResult;
    }


    public GameObject GetObject( string strName, int nID )
    {
		if (nID == 0) 
		{
			return null;
		}

        GameObject[] Objects = GameObject.FindGameObjectsWithTag(strName);

		foreach (GameObject obj in Objects) 
		{
			if( obj != null )
			{
				ObjectBase ObjBase = obj.GetComponent<ObjectBase>();
				
				//Debug.Log("Name : " + ObjTemp.name + "status : " + ObjBase.ObjStatus.ToString());
				
				if (ObjBase.ObjStatus != OBJECT_STATE.DIE && nID == obj.GetInstanceID())
				{
					return obj;
				}
			}
		}

		return null;
	}
	
	public void SetDieObject( OBJECT_TYPE type )
	{
		--_ObjectCount[type];

		if (_ObjectCount[type] <= 0) 
		{
			if (type == OBJECT_TYPE.PLAYER)
			{
				Invoke("Defeat", 5);
				StartCoroutine( "SetVictoryMotion", "MONSTER" );
			}
			else
			{
				//Debug.Log ("SetDieObject NextChaper");
				NextChaper();
			}	
		}
	}

	public void SetSkillSlotVisible( bool bVisible )
	{
		GameObject[] Objects = GameObject.FindGameObjectsWithTag("PLAYER");
		
		foreach (GameObject obj in Objects) 
		{
			if( obj != null )
			{
				ObjectBase ObjBase = obj.GetComponent<ObjectBase>();
				ObjBase.SetVisibleSkillslot( bVisible );
			}
		}

	}
	
	public void DestroyObjectTag( string strTag )
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag (strTag);

		foreach (GameObject obj in objects) 
		{
			GameObject.Destroy( obj, 2.0f );
		}
	}


    public void LoadMainMenuScene()
    {
        Application.LoadLevel( "MainMenuScene" );
    }

	public void CreatePauseEffect()
	{
		GameObject ObjTemp = Resources.Load("Prefabs/Effect/Object/fx_skill_camfx_01", typeof(GameObject)) as GameObject;
		
		GameObject Effect = Instantiate(ObjTemp, Vector3.zero, Quaternion.identity ) as GameObject;
		
		Effect.transform.parent = _Camera[1].gameObject.transform;
		Effect.transform.localPosition = ObjTemp.transform.localPosition;
		Effect.transform.localRotation = ObjTemp.transform.localRotation;

		GameObject.Destroy (Effect, 1);
	}

	public void RunCameraVibrate()
	{
		if (_bRunCameraVibrate) {
			return;
		}

		iTween.ShakePosition(_Camera[0].gameObject,iTween.Hash("x",VibrateValue,"time",VibrateTime, "oncompletetarget", gameObject, "oncomplete", "EndCameraVibrate" ));
		_bRunCameraVibrate = true;
	}

	public void EndCameraVibrate()
	{
		_bRunCameraVibrate = false;
	}

	public void CameraGlowEffect()
	{
		if (_bRunGlowEffect) {
			return;
		}

		_bRunGlowEffect = true;

//		GlowEffect effect = _mainCamera.GetComponent< GlowEffect > ();
//		effect. = _bRunGlowEffect;

		Invoke ( "EndClowEffect", 0.1f);
	}

	public void EndClowEffect()
	{
		_bRunGlowEffect = false;

		GlowEffect effect = _mainCamera.GetComponent< GlowEffect > ();
		effect.enabled = _bRunGlowEffect;
	}


	public void ObjectPauseResum( GameObject obj, string fun )
	{
		GameObject[] Objects = GameObject.FindGameObjectsWithTag( "PLAYER" );
		
		if ( Objects != null )
		{
			foreach( GameObject objtemp in Objects )
			{
				if( objtemp == obj )
				{
					continue;
				}

				objtemp.SendMessage( fun );
			}
		}


		Objects = GameObject.FindGameObjectsWithTag( "MONSTER" );
		
		if ( Objects != null )
		{
			foreach( GameObject objtemp in Objects )
			{
				if( objtemp == obj )
				{
					continue;
				}

				objtemp.SendMessage( fun );
			}
		}
	}

	public void CameraColorEffect( bool bEnable, GameObject obj )
	{
		int nLayerNo = 12;
		if (bEnable) 
		{
			_Camera [0].SetActive (false);
			_Camera [1].SetActive (true);
		}
		else 
		{
			nLayerNo = 11;
			_Camera [0].SetActive (true);
			_Camera [1].SetActive (false);
		}

		GameObject[] Objects = GameObject.FindGameObjectsWithTag( "PLAYER" );
		
		if ( Objects != null )
		{
			foreach( GameObject objtemp in Objects )
			{
				if( objtemp == obj )
				{
					continue;
				}
				
				objtemp.SendMessage( "SetObjectLayer", nLayerNo);
			}
		}
		
		
		Objects = GameObject.FindGameObjectsWithTag( "MONSTER" );
		
		if ( Objects != null )
		{
			foreach( GameObject objtemp in Objects )
			{
				if( objtemp == obj )
				{
					continue;
				}
				
				objtemp.SendMessage( "SetObjectLayer", nLayerNo);

			}
		}
	}















}
