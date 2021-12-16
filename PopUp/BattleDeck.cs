using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using STORY_ENUM;
using STORY_GAMEDATA;

public class BattleDeck : MonoBehaviour {

	//
	public GameObject[] _BattleDeckSlots;


	List< GameObject > _BattleDeckChars;

	public Transform[] _CharGridTr;
	// Use this for initialization
	void Start () 
	{
		_BattleDeckChars = new List< GameObject > ();

		CreateUserCharSlot (1, _CharGridTr[0]);
		CreateUserCharSlot (2, _CharGridTr[1]);
		CreateUserCharSlot (3, _CharGridTr[2]);
	}

	public void CreateUserCharSlot( int nPosType, Transform tr )
	{
		List< OBJECT_DEFAULT_DATA > CharList = Sqlprocess.Instance.LoadUserCharDataList ( "SELECT* FROM userCharData WHERE PosType =" + nPosType.ToString() );

		//Debug.Log( "Count : " + CharList.Count.ToString() + "  PosType : " + nPosType.ToString() );

		for (int i_1 = 0; i_1 < CharList.Count; i_1 += 2) 
		{
			//Debug.Log( "i_1 " + i_1.ToString() );

			int nIndex = i_1;

			//Debug.Log( "Index " + nIndex.ToString() );
			OBJECT_DEFAULT_DATA ldat = CharList[nIndex];
			OBJECT_DEFAULT_DATA rdat = null;

			++nIndex;
			//Debug.Log( "Index " + nIndex.ToString() );
			if( nIndex < CharList.Count )
			{
				rdat = CharList[nIndex];
			}

			GameObject DeckSlot = Resources.Load( "Prefabs/PopUp/DeckSlot") as GameObject;
			DeckSlot = Instantiate(DeckSlot, Vector3.zero, Quaternion.identity) as GameObject;
			DeckSlot.transform.parent = tr;
			DeckSlot.transform.localPosition = new Vector3( 0, 0, 0 );
			DeckSlot.transform.localScale = new Vector3( 1, 1, 1 );
			
			DeckSlot SlotScript = DeckSlot.GetComponent< DeckSlot >();
			SlotScript.initWithSlotData( ldat, rdat);
		}
	}

	Transform GetEmptyBattleDeckSlot()
	{
		for( int i_1 = 0; i_1 < _BattleDeckSlots.Length; ++i_1 )
		{
			bool bFind = false;

			Transform[] tempTransforms = _BattleDeckSlots[i_1].GetComponentsInChildren<Transform>(); 

			foreach (Transform child in tempTransforms) 
			{ 
				if (child.name.Contains("CharIcon")) 
				{ 
					bFind = true;
				} 
			}

			if( bFind == false )
			{
				return _BattleDeckSlots[i_1].transform;
			}
		}

		return null;
	}

	public bool InsertBattleDeckSlot( OBJECT_DEFAULT_DATA dat, CharIcon DescIcon )
	{
		Transform RootTr = GetEmptyBattleDeckSlot ();

		if (RootTr == null) 
		{
			return false;
		}

		GameObject Icon = Resources.Load( "Prefabs/PopUp/CharIcon") as GameObject;
		Icon = Instantiate(Icon, Vector3.zero, Quaternion.identity) as GameObject;
		Icon.transform.parent = RootTr;
		Icon.transform.localPosition = new Vector3( 0, 0, 0 );
		Icon.transform.localScale = new Vector3( 1, 1, 1 );
					
		CharIcon IconScript = Icon.GetComponent< CharIcon >();
		IconScript.initWithCharData( dat );
		IconScript.DescIcon = DescIcon;
		IconScript.EnableTouch (true);

		_BattleDeckChars.Add (Icon);

		Sqlprocess.Instance.UpdateIntgerData ("UPDATE UserCharData SET BattleDeck = 1 WHERE row = " + dat.nRow.ToString()); 

		return true;
	}

	public bool SelectCharIcon( CharIcon IconScript )
	{
		DeckSlot SlotScript = IconScript.gameObject.GetComponentInParent< DeckSlot > ();

		if (SlotScript != null) 
		{
			if( !SlotScript.SelectUserSlot (IconScript) )
			{
				Sqlprocess.Instance.UpdateIntgerData ("UPDATE UserCharData SET BattleDeck = 0 WHERE row = " + IconScript.CharData.nRow.ToString()); 

				GameObject FindObject = FindDeckSlot( IconScript.CharData.nRow );
				_BattleDeckChars.Remove( FindObject );
				GameObject.DestroyImmediate( FindObject );
			}
		}
		else 
		{
			SlotScript = IconScript.DescIcon.gameObject.GetComponentInParent< DeckSlot >();
			SlotScript.SetCheckVisible( false, IconScript.DescIcon.DirIndex );

			Sqlprocess.Instance.UpdateIntgerData ("UPDATE UserCharData SET BattleDeck = 0 WHERE row = " + IconScript.CharData.nRow.ToString()); 
			_BattleDeckChars.Remove( IconScript.gameObject );
			DestroyImmediate( IconScript.gameObject );
		}

		return true;
	}

	GameObject FindDeckSlot( int row )
	{
		for (int i_1 = 0; i_1 < _BattleDeckChars.Count; ++i_1) 
		{
			GameObject IconObject = _BattleDeckChars[i_1];
			CharIcon script = IconObject.GetComponent< CharIcon >();

			if( script.CharData.nRow == row )
			{

				return IconObject;
			}
		}
		return null;
	}

	public void SetBattlePos()
	{
		int nIndex = 1;
		for (int i_1 = 0; i_1 < _BattleDeckChars.Count; ++i_1, ++nIndex) 
		{
			GameObject Temp = _BattleDeckChars[i_1];

			CharIcon IconScript = Temp.GetComponent< CharIcon >();



			//Debug.Log( "UPDATE UserCharData SET BattleDeck = " + nIndex.ToString() + " WHERE row = " + IconScript.CharData.nRow.ToString() );
			Sqlprocess.Instance.UpdateIntgerData ("UPDATE UserCharData SET BattleDeck = " + nIndex.ToString() + " WHERE row = " + IconScript.CharData.nRow.ToString()); 
		}
	}

	public void ButtonClick( GameObject obj )
	{
		string strName = obj.name;
		
		if( strName.CompareTo( "BackBtn" ) == 0 )
		{
			FactoryManager.Instance.CreatePopUp(POPUP_TYPE.StagePopUp);
		}
		else if ( strName.CompareTo( "PlayBtn" ) == 0 )
		{
			if( _BattleDeckChars.Count > 0 )
			{
				UIButton button = obj.GetComponent< UIButton > ();
				button.enabled = false;

				FactoryManager.Instance.DeletePopUp();
				//Debug.Log( "ButtonClick" );
				SetBattlePos();

				//UnityEngine.Application.LoadLevelAsync("Map_0002-1");

				//UnityEngine.Application.LoadLevel(GameManager.DungeonData.strMap);
				//UnityEngine.Application.LoadLevelAsync(GameManager.DungeonData.strMap);
				GameObject parent = GameObject.FindWithTag("UI_CAMERA") as GameObject;

				GameObject LoadingPage = Resources.Load( "Prefabs/UI/LoadingPage", typeof(GameObject)) as GameObject;
				NGUITools.AddChild(parent, LoadingPage);
				//_OpenPopup = NGUITools.AddChild(parent, obj);
				
				//_OpenPopup.transform.localScale = obj.transform.localScale;
				//_OpenPopup.transform.localPosition = obj.transform.localPosition;

			}
		}
	}
}
