using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using STORY_GAMEDATA;

public class DungeonTypePopUp : MonoBehaviour {

	public GameObject _TypeSlotBase;
	public GameObject _DescTextImage;
	public GameObject _TitleTexImage;

	DungeonTypeSlot _SelectSlot;

	//DUNGEON_TYPE_DATA SelectDunGeonTypeData;

	public void Awake()
	{
		//SelectDunGeonTypeData = new DUNGEON_TYPE_DATA ();
		_SelectSlot = new DungeonTypeSlot();

		List<DUNGEON_TYPE_DATA> DunTypeList = GameDataManagerDungeon.Instance.GetDungeonTypeList();

		if (DunTypeList.Count <= 0) 
		{
			return;
		}

		_SelectSlot = CreateDungeonTypeSlot ( DunTypeList[0], 0 );
		_SelectSlot.SetSelect (true);


		UpdateDungeonType (_SelectSlot.gameObject);


		for (int i_1 = 1; i_1 < DunTypeList.Count; ++i_1)
		{
			CreateDungeonTypeSlot( DunTypeList[i_1], i_1 );
		}
	}

	DungeonTypeSlot CreateDungeonTypeSlot( DUNGEON_TYPE_DATA dat, int nIndex )
	{
		GameObject ObjTemp = Resources.Load("Prefabs/PopUp/DungeonTypeSlot", typeof(GameObject)) as GameObject;

		Vector3 localpos = ObjTemp.transform.localPosition;

		ObjTemp = NGUITools.AddChild(_TypeSlotBase, ObjTemp);
		ObjTemp.transform.localPosition = new Vector3(localpos.x, localpos.y - ( nIndex * 77 ), 0);

		DungeonTypeSlot script = ObjTemp.GetComponent< DungeonTypeSlot >();
		script.initWithChapterSlot( dat );

		script._BaseObject = gameObject;

		return script;
	}

	public void UpdateDungeonType( GameObject Obj )
	{
		DungeonTypeSlot script = Obj.GetComponent< DungeonTypeSlot > ();

		if (_SelectSlot.DunGeonTypeData.nType == script.DunGeonTypeData.nType) 
		{
			return;
		}

		_SelectSlot.SetSelect (false);
		script.SetSelect (true);

		string strPath = "Image/PopUp/StagePopUp/mainmenu_A_0" + script.DunGeonTypeData.nType.ToString ();
		
		UITexture tex = _TitleTexImage.GetComponentInChildren<UITexture>();
		tex.mainTexture = Resources.Load(strPath) as Texture;


		strPath = "Image/PopUp/StagePopUp/mainmenu_B_0" + script.DunGeonTypeData.nType.ToString ();
		tex = _DescTextImage.GetComponentInChildren<UITexture>();
		tex.mainTexture = Resources.Load(strPath) as Texture;

		_SelectSlot = script;
	}

	public void CreateDungeonStagePopUp( GameObject obj )
	{
		GameObject PopUp = FactoryManager.Instance.CreatePopUp (STORY_ENUM.POPUP_TYPE.StagePopUp);

		StagePopUp script = PopUp.GetComponent< StagePopUp > ();
		script.OnUpdateStageType (_SelectSlot.DunGeonTypeData);
		                      
	}

	public void ClosePopUp( GameObject obj )
	{
		FactoryManager.Instance.DeletePopUp ();
	}
}

















