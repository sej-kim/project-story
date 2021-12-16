using UnityEngine;
using System.Collections;
using STORY_ENUM;

public class VictoryEffect : MonoBehaviour {


	public int _Count;
	private Animator _ani;
	public GameObject _FirstAni;
	public GameObject _NextAni;
	public GameObject []_heart;

	void Awake()
	{
		_ani = GetComponent<Animator> ();
		_ani.SetInteger ("HartCount", _Count);
	}
	// Use this for initialization
	void Start () 
	{
		for (int i_1 = 0; i_1 < _Count; ++i_1) 
		{
			_heart [i_1].SetActive ( true );
		}

		float fCallTime = (0.6f * _Count) + 1.0f;
		Invoke ("NextAni", fCallTime);
	}

	public void NextAni()
	{
		_FirstAni.SetActive (false);
		_NextAni.SetActive (true);

		Invoke ("CreateResultPopUp", 3);
	}

	public void CreateResultPopUp()
	{
		FactoryManager.Instance.CreatePopUp (POPUP_TYPE.ResultPopUp);
	}
}
