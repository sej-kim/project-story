using UnityEngine;
using System.Collections;
using STORY_GAMEDATA;
using STORY_ENUM;

public class ObjectHealth : MonoBehaviour {

	public GameObject _target;

	private OBJECT_HPMP_DATA _HearthData;
	//private Vector3 healthScale;   
	private UISlider healthBar; 
	private float _fhealth;

	Camera worldCam;
	Camera guiCam;

	// Use this for initialization
	void Start () 
    {
		healthBar = GetComponent< UISlider> ();

		//월드좌표의 카메라객체입니다.
		worldCam = NGUITools.FindCameraForLayer(_target.layer);
		//GUI객체의 카메라 객체입니다.
		guiCam = NGUITools.FindCameraForLayer(gameObject.layer);
		//healthScale = healthBar.transform.localScale;
	}

	void Update () 
	{
		if (_target == null) 
		{
			return;
		}
		//gameObject.transform.localScale= new Vector3( 0.6f, 0.5f );
		
		//GUIText text;
		//text.text
		//transform.localPosition = target.transform.localPosition;
		

		
		//타겟의 포지션을 월드좌표에서 ViewPort좌표로 변환하고 다시 ViewPort좌표를 NGUI월드좌표로 변환합니다.
		Vector3 pos = guiCam.ViewportToWorldPoint(worldCam.WorldToViewportPoint(_target.transform.position));
		
		//Z는 0으로...
		//pos.z = 0f;

		pos.y += 0.42f;
		pos.x -= 0.11f;
		//Vector3 pos = _target.transform.position;

		//pos.y += 1.2f;

		//Label의 좌표를 설정합니다.
		transform.position = pos;
	}

    public OBJECT_HPMP_DATA HealthData
    {
        get
        {
			return _HearthData;
        }

        set
        {
			_HearthData = new OBJECT_HPMP_DATA();
			_HearthData = value;
        }
    }

    public OBJECT_STATE SetDamage( int nDamage )
    {
        OBJECT_STATE ResultState = OBJECT_STATE.NONE;

		_HearthData.nCurHP = _HearthData.nCurHP - nDamage;

		if (_HearthData.nCurHP <= 0)
        {
			_HearthData.nCurHP = 0;
            ResultState = OBJECT_STATE.DIE;
        }

		UpdateHealthBar ();

        return ResultState;
    }

	public void UpdateHealthBar ()
	{
		_fhealth = (float)((float)_HearthData.nCurHP / (float)_HearthData.nMaxHP);

		//Debug.Log (_fhealth.ToString ());
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		//healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - _fhealth);

		healthBar.value = _fhealth;
		// Set the scale of the health bar to be proportional to the player's health.
		//healthBar.transform.localScale = new Vector3(healthScale.x * _fhealth, healthBar.transform.localScale.y, transform.localScale.z);
	}
}
