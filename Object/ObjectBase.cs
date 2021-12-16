using UnityEngine;
using System.Collections;
using STORY_GAMEDATA;
using STORY_ENUM;

public class ObjectBase : MonoBehaviour {

	protected GameObject 					_ObjectHealth;

	[SerializeField]

	// 오브젝트 전반적인 데이타
	protected OBJECT_DEFAULT_DATA			_ObjDefaultData;

	ObjectStatValue 						_ObjectDamagetStat;

	protected int _SkillCount = 0;

    // 현제 상태
    protected OBJECT_STATE _ObjState = OBJECT_STATE.NONE;

	protected bool _IsPause = false;

	// 애니메이션 컨트롤러
	protected   Animator      _AniController;

	private SkillSlots _SkillSlots;

	int _nDFontType = 9;
    
	// Use this for initialization
    void Start () 
    {
        initialization();
	}
        
    virtual protected void initialization()
    {

    }

    // 하위 래밸의 클레스에서 데이타 셋팅한뒤 맴버 변수에 넣어 준다.
	virtual public bool initWithObjectData( OBJECT_DEFAULT_DATA data )
    {
		_ObjectDamagetStat = gameObject.AddComponent< ObjectStatValue > ();


		_ObjDefaultData = new OBJECT_DEFAULT_DATA();
		_ObjDefaultData = data;


		gameObject.tag = ObjDefaultData.ObjectType.ToString ();


		/*
		Debug.Log ( "tb_CHAR - " + "  uID :" + _ObjDefaultData.uID.ToString () + "  naem :" + _ObjDefaultData.strName + "  Type :" + _ObjDefaultData.nType.ToString () + "  DamageType :" + _ObjDefaultData.DamageType.ToString () 
		           + "  Level :" + _ObjDefaultData.nLevel.ToString () + "  ElevatLv :" + _ObjDefaultData.nElevatLv.ToString () + "  RevolLv :" + _ObjDefaultData.nRevolutionLv.ToString () + "  Pos :" + _ObjDefaultData.nPos.ToString ()
		           + "  ImagePath :" + _ObjDefaultData.strImage + "  AI :" + _ObjDefaultData.nAI.ToString()); 
		*/

		_ObjectDamagetStat.initObjectStat (_ObjDefaultData);

		CreateHeald ();

		if (ObjDefaultData.ObjectType == OBJECT_TYPE.MONSTER) 
		{
			_SkillCount = 2;
		} 
		else 
		{
			_SkillCount = 4;
			_nDFontType = 2;
		}

		//CreateSkillUI ();

        return true;
    }

	virtual public void CreateSkillUI( int nCount )
	{
		SKILL_DATA dat1 = new SKILL_DATA ();
		SKILL_DATA dat2 = new SKILL_DATA ();


		int nIndex = Random.Range (1, 5) + 1;

		dat1.fTime = Random.Range (5.0f, 10.0f);
		dat1.strImage = "Skill_0" + nIndex.ToString();

		nIndex = Random.Range(1, 5) + 1;

		dat2.fTime = Random.Range (30.0f, 60.0f);
		dat2.strImage = "Skill_0" + nIndex.ToString();

		GameObject parent = GameObject.FindWithTag("UI_CAMERA") as GameObject;
		
		GameObject obj = Resources.Load( "Prefabs/UI/SkillSlots", typeof(GameObject)) as GameObject;
		obj = NGUITools.AddChild(parent, obj);
		
		obj.transform.localScale = new Vector3( 1, 1, 1 );
		obj.transform.localPosition = new Vector3( -277.0f + ( 140.0f * nCount ), -198.0f, 0 );

		_SkillSlots = obj.GetComponent< SkillSlots > ();
		_SkillSlots.initWithSkillSlotsData ( gameObject, dat1, dat2);
	}



	virtual public void CreateHeald()
	{
		GameObject prefab = Resources.Load("Prefabs/UI/Hpbar", typeof(GameObject)) as GameObject;
		
		_ObjectHealth = GameObject.Instantiate(prefab) as GameObject;
		_ObjectHealth.transform.localScale = prefab.transform.localScale;
	
		OBJECT_HPMP_DATA healdata = new OBJECT_HPMP_DATA ();
		healdata.nMaxHP = _ObjectDamagetStat.ObjValueData.nHP;
		healdata.nCurHP = _ObjectDamagetStat.ObjValueData.nHP;
		
		ObjectHealth hpth = _ObjectHealth.GetComponent<ObjectHealth> ();
		hpth._target = gameObject;
		hpth.HealthData = healdata;
	}

    virtual public OBJECT_STATE ObjStatus
    {
        get 
        {
            return _ObjState;
        }
        set 
        {
            _ObjState = value;
        }
    }

	virtual public OBJECT_VALUE_DATA ObjValueData
	{
		get
		{
			return _ObjectDamagetStat.ObjValueData;
		}
	}

	virtual public OBJECT_DEFAULT_DATA ObjDefaultData
	{
		get
		{
			return _ObjDefaultData;
		}
	}

	public void SetObjectLayer ( int nLyaer )
	{
//		Transform[] tempTransforms = gameObject.GetComponentsInChildren<Transform>(); 
//		
//		foreach (Transform child in tempTransforms) 
//		{ 
//			child.gameObject.layer = nLyaer;
//		}


//		Shader sha = GetComponentInChildren< Shader >();
//		Debug.Log (sha.ToString ());

		Renderer ren = GetComponentInChildren< Renderer > ();


		if (nLyaer == 12) 
		{
			ren.material.shader = Shader.Find ("Projector/Multiply");
			//ren.material.color = new Color( 0, 0, 0 );
		}
		else 
		{
			ren.material.shader = Shader.Find ("Mobile/Diffuse");

		}

	}

    virtual public void SetSkill( SKILL_DATA dat )
    {
 		//gameObject.

    }

	virtual protected void ChangeAnimation(OBJECT_STATE state, int nValue = 0 )
    {

    }

	virtual public void CreateDamageFont( int nDamage, Vector3 pos )
	{
		return;
		Vector3 pospos = new Vector3 (pos.x + Random.Range( -0.5f, 0.5f ), pos.y, pos.z);
		//타겟의 포지션을 월드좌표에서 ViewPort좌표로 변환하고 다시 ViewPort좌표를 NGUI월드좌표로 변환합니다.
		GameObject prefab = Resources.Load("Prefabs/UI/DamageLabel", typeof(GameObject)) as GameObject;
		GameObject go = GameObject.Instantiate(prefab, pospos, Quaternion.identity) as GameObject;

		FollowText follseText = go.GetComponent<FollowText> ();
		follseText.target = gameObject;
		follseText.tPos = pospos;
		follseText.SetDamageText (_nDFontType, "-" + nDamage.ToString ());
	}

    virtual public int SetDie()  
    {
        if (_ObjState == OBJECT_STATE.DIE)
        {
            return 1;
        }


		if (_SkillSlots != null) {
			_SkillSlots.SetDie ();
		}


		GameManager.Instance.SetDieObject (_ObjDefaultData.ObjectType);

			/*
		if (GameManager.Instance.SetDieObject( _ObjType ) )
        {
			//GameManager.Instance.NextChaper();
        }
        */

         
        ChangeAnimation(OBJECT_STATE.DIE);

        return 0;
    }

	virtual public void ResetSkillCoolTime( int nIndex )
	{
		if (_SkillSlots != null) 
		{
			_SkillSlots.ResetCoolTime (nIndex);
		}
	}

	virtual public void SetVisibleSkillslot( bool bVisible )
	{
		if (_SkillSlots != null) 
		{
			_SkillSlots.gameObject.SetActive( bVisible );
		}
	}

	public void Pause()
	{
		if (_IsPause) {
			return;
		}
		_AniController.enabled = false;
		_IsPause = true;
	}

	public void Resum()
	{
		if (_IsPause == false) {
			return;
		}

		_AniController.enabled = true;
		_IsPause = false;
	}

}
