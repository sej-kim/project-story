using UnityEngine;
using System.Collections;
using STORY_GAMEDATA;

public class DeckSlot : MonoBehaviour {

	public Transform[] _SlotTr;
	public GameObject[] _SelectTex;

	public bool initWithSlotData( OBJECT_DEFAULT_DATA lData, OBJECT_DEFAULT_DATA rData )
	{
		_SelectTex [0].SetActive (false);
		_SelectTex [1].SetActive (false);

		CreateCharIcon (0, lData);
		CreateCharIcon (1, rData);

		return true;
	}

	void CreateCharIcon( int nTrIndex, OBJECT_DEFAULT_DATA data )
	{
		if (data == null) 
		{
			return;
		}

		//Debug.Log (data.strImage);
		GameObject Icon = Resources.Load( "Prefabs/PopUp/CharIcon") as GameObject;
		Icon = Instantiate(Icon, Vector3.zero, Quaternion.identity) as GameObject;
		Icon.transform.parent = _SlotTr[nTrIndex];
		Icon.transform.localPosition = new Vector3( 0, 0, 0 );
		Icon.transform.localScale = new Vector3( 0.7f, 0.7f, 0.7f );
		
		CharIcon IconScript = Icon.GetComponent< CharIcon >();
		IconScript.initWithCharData( data );
		IconScript.EnableTouch (true);
		IconScript.DirIndex = nTrIndex;

		_SelectTex [nTrIndex].SetActive (data.nBattleDeck > 0 ? true : false);

		if (data.nBattleDeck > 0) 
		{
			BattleDeck BattlDeckScript = gameObject.GetComponentInParent< BattleDeck > ();
			BattlDeckScript.InsertBattleDeckSlot( data, IconScript );
		}
	}

	public bool SelectUserSlot( CharIcon Script )
	{
		bool bActivity = !_SelectTex [Script.DirIndex].activeSelf;

		if (bActivity) 
		{
			BattleDeck DeckScript = GetComponentInParent< BattleDeck > ();

			if (DeckScript.InsertBattleDeckSlot (Script.CharData, Script)) 
			{
				_SelectTex [Script.DirIndex].SetActive (bActivity);
			}
		} 
		else 
		{
			_SelectTex [Script.DirIndex].SetActive (bActivity);
		}


		//Debug.Log ("SelectUserSlot");
		return bActivity;
	}


	public void SetCheckVisible( bool bVisigle, int nDir )
	{
		_SelectTex [nDir].SetActive (bVisigle);
	}















}
