using UnityEngine;
using System.Collections;
using STORY_GAMEDATA;

public class SkillSlots : MonoBehaviour 
{
	public GameObject _CharTex;
	public GameObject _SkillTex1;
	public GameObject _SkillTex2;

	public GameObject _Slider;

	GameObject _CharObject;
	Character _CharScript;


	SKILL_DATA[] _SkillData;
	float[] _fCoolTime;

	public GameObject[] _CoolTimeObj;
	UISprite[] _CoolTimeSprite;


	void Start()
	{
		_CoolTimeSprite = new UISprite[2];

		_CoolTimeSprite[0] = _CoolTimeObj [0].GetComponent< UISprite > ();
		_CoolTimeSprite[1] = _CoolTimeObj [1].GetComponent< UISprite > ();

		_fCoolTime = new float[2];

		UIButton bu = _SkillTex2.GetComponent< UIButton > ();
		bu.enabled = false;

	}

	// 140
	void Update()
	{
//		if (_SkillData == null) 
//		{
//			return;
//		}
		if (_SkillData[0].fTime > _fCoolTime[0]) 
		{
			_fCoolTime[0] = Mathf.Min( _SkillData[0].fTime, _fCoolTime[0] + Time.deltaTime );
			UpdateGoolTimeGage( 0, false );
		}

		if (_SkillData[1].fTime > _fCoolTime[1]) 
		{
			_fCoolTime[1] = Mathf.Min( _SkillData[1].fTime, _fCoolTime[1] + Time.deltaTime );
			UpdateGoolTimeGage( 1, true );
		}
	}

	public bool initWithSkillSlotsData (GameObject CharObj, SKILL_DATA lData, SKILL_DATA rData )
	{
		_CharObject = CharObj;

		_SkillData = new SKILL_DATA[2];

		_SkillData[0] = lData;
		_SkillData[1] = rData;

		_CharScript = _CharObject.GetComponent< Character > ();

		//Debug.Log (CharScript.ObjDefaultData.strImage);
		UITexture tex = _CharTex.GetComponent< UITexture > ();
		tex.mainTexture = Resources.Load ("Image/Icon/Char/Ch_" + _CharScript.ObjDefaultData.strImage) as Texture;


		tex = _SkillTex1.GetComponent< UITexture > ();
		tex.mainTexture = Resources.Load ("Image/Icon/Skill/" + lData.strImage) as Texture;


		tex = _SkillTex2.GetComponent< UITexture > ();
		tex.mainTexture = Resources.Load ("Image/Icon/Skill/" + rData.strImage) as Texture;

		return true;
	}

	public void ActiveSkillClick( GameObject obj )
	{
		if (_SkillData[0].fTime <= _fCoolTime[0]) 
		{
			Debug.Log( _fCoolTime[0].ToString() );
			_CharScript.SetSkill (1);
		}
	}

	public IEnumerator SetAutoSkill ()
	{
		yield return new WaitForSeconds(0.5f);

		_CharScript.SetSkill (2);
	}

	public void ResetCoolTime( int nIndex )
	{
		_fCoolTime [nIndex - 1] = 0.0f;

		if (nIndex > 1) 
		{
			StopCoroutine( "SetAutoSkill" );
		}
	}

	public void SetDie()
	{
		UIPanel panel = gameObject.GetComponent< UIPanel > ();
		panel.alpha = 0.3f;

		//StopCoroutine( "SetAutoSkill" );
	}

	void UpdateGoolTimeGage( int nIndex, bool bAuto )
	{
		float FillValue = 1.0f - ( _fCoolTime[nIndex] / _SkillData[nIndex].fTime );
		_CoolTimeSprite [nIndex].fillAmount = FillValue;

//		if (bAuto && FillValue <= 0) 
//		{
//			StartCoroutine( "SetAutoSkill" );
//		}
	}
















}
