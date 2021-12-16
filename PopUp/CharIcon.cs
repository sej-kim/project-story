using UnityEngine;
using System.Collections;
using STORY_GAMEDATA;

public class CharIcon : MonoBehaviour {

	// HartPos -21 -39, -10 -39, 0.7 -39, 11.4 -39, 22.2 -39

	OBJECT_DEFAULT_DATA _CharData;

	public UILabel _LevelLabel;
	public Transform _HartParent;
	public GameObject _ExpSlider;

	public CharIcon _DescIcon;
	public int _nDirIndex;

	void Awake()
	{
		EnableTouch (false);
	}

	public void initWithCharData( OBJECT_DEFAULT_DATA dat )
	{
		//_DescIcon = new CharIcon ();

		_CharData = new OBJECT_DEFAULT_DATA ();
		_CharData = dat;

		_LevelLabel.text = _CharData.nLevel.ToString();

		//Debug.Log (_CharData.strImage);
		UITexture CharTex = GetComponent< UITexture >();
		CharTex.mainTexture = Resources.Load("Image/Icon/Char/Ch_" + _CharData.strImage) as Texture;

		float PosX = -21.5f;


		for (int i_1 = 0; i_1 < dat.nRevolutionLv; ++i_1) 
		{
			GameObject HartObj = Resources.Load( "Prefabs/EmpyObject") as GameObject;
			HartObj = Instantiate(HartObj, Vector2.zero, Quaternion.identity) as GameObject;

			HartObj.transform.parent = _HartParent;
			HartObj.transform.localPosition = new Vector3( PosX, -39.0f, 0 );
			HartObj.transform.localScale =  new Vector3( 1, 1, 1 );

			UITexture tex = HartObj.AddComponent< UITexture >();
			tex.mainTexture = Resources.Load( "Image/Icon/Char/Ch_heart") as Texture;

			tex.depth = 12;
			tex.SetDimensions( 12, 12 );
			PosX += 10.5f;
		}
	}

	public void IconClick()
	{
		BattleDeck DeckSlot = gameObject.GetComponentInParent< BattleDeck > ();
		DeckSlot.SelectCharIcon (this);

		//Debug.Log ("IconClick");
	}

	public void EnableTouch( bool bTouch )
	{
		UIButton button = gameObject.GetComponent< UIButton > ();
		button.enabled = bTouch;
	}

	public void CreateExpGage( float maxExp, float Exp )
	{
		_ExpSlider.SetActive (true);

		UISlider Slider = _ExpSlider.GetComponent< UISlider > ();
		Slider.value = Exp / maxExp;
	}

	public int DirIndex
	{
		get 
		{
			return _nDirIndex;
		}
		set 
		{
			_nDirIndex = value;
		}
	}

	public CharIcon DescIcon
	{
		get 
		{
			return _DescIcon;
		}
		set 
		{
			_DescIcon = value;
		}
	}

	public OBJECT_DEFAULT_DATA CharData
	{
		get 
		{
			return _CharData;
		}
		set 
		{
			_CharData = value;
		}
	}



}
