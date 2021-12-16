using UnityEngine;
using System.Collections;

using STORY_GAMEDATA;

public class DungeonTypeSlot : MonoBehaviour {

	public GameObject _SelectImage;
	public GameObject _ChapterNameImage;

	public GameObject _BaseObject;


	DUNGEON_TYPE_DATA _DunGeonTypeData;

	public bool initWithChapterSlot( DUNGEON_TYPE_DATA dat )
	{
		string strPath = "Image/PopUp/StagePopUp/mainmenu_A_0" + dat.nType.ToString ();

		UITexture tex = _ChapterNameImage.GetComponentInChildren<UITexture>();
		tex.mainTexture = Resources.Load(strPath) as Texture;

		_SelectImage.SetActive (false);


		_DunGeonTypeData = new DUNGEON_TYPE_DATA ();
		_DunGeonTypeData = dat;

		return true;
	}

	public DUNGEON_TYPE_DATA DunGeonTypeData
	{
		get
		{
			return _DunGeonTypeData;
		}
	}

	public void SetSelect( bool bSelect )
	{
		_SelectImage.SetActive (bSelect);
	}

	public void Click()
	{
		DungeonTypePopUp DunBase = _BaseObject.GetComponent< DungeonTypePopUp > ();
		DunBase.UpdateDungeonType (gameObject);
	}
}
