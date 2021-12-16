using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using STORY_GAMEDATA;


public class ResultPopUp : MonoBehaviour {

	//RESULT_DATA _ResultData = new RESULT_DATA ();

	public UILabel _TeamLevelLabel;
	public UILabel _ExpLabel;
	public UILabel _GoldLabel;
	public Transform[] _CharIconTr; 

	GameObject _DataView = null;

	// Use this for initialization
	void Start () 
	{
		initResultData ();
	}

	public void initResultData()
	{
		_TeamLevelLabel.text 	= GameManager.Instance.RewardItemData.nUserExp.ToString();
		_ExpLabel.text 			= GameManager.Instance.RewardItemData.nCharExp.ToString();
		_GoldLabel.text 		= GameManager.Instance.RewardItemData.nGold.ToString();

		UpdateUserExp (GameManager.Instance.RewardItemData.nUserExp);


		GameObject[] Objects = GameObject.FindGameObjectsWithTag( "PLAYER" );

		int nCharExp = GameManager.Instance.RewardItemData.nCharExp / Objects.Length;


		//CharStatModification.CHAR_EXP ();

		for (int i_1 = 0; i_1 < Objects.Length; ++i_1) 
		{
			Character BaseScript = Objects[i_1].GetComponent< Character >();

			GameObject Icon = Resources.Load( "Prefabs/PopUp/CharIcon") as GameObject;
			Icon = Instantiate(Icon, Vector3.zero, Quaternion.identity) as GameObject;
			Icon.transform.parent = _CharIconTr[i_1];
			Icon.transform.localPosition = Vector3.zero;
			Icon.transform.localScale = new Vector3( 1, 1, 1 );

			OBJECT_EXP_DATA ExpData = GameDataManagerChar.Instance.GetExpData (BaseScript.ObjDefaultData.nLevel);
			int nMaxExp = CharStatModification.CHAR_EXP (BaseScript.ObjDefaultData.nLevel, ExpData.fCharExt [0], ExpData.fCharExt [1], ExpData.fCharExt [2], ExpData.fCharExt [3]); 


			OBJECT_DEFAULT_DATA ObjDataTemp = UpdateCharExp( BaseScript.ObjDefaultData, nCharExp, nMaxExp );

			CharIcon IconScript = Icon.GetComponent< CharIcon >();
			IconScript.initWithCharData( ObjDataTemp );
			IconScript.CreateExpGage(nMaxExp, (float)ObjDataTemp.nExp);
		}
	}

	OBJECT_DEFAULT_DATA UpdateCharExp(OBJECT_DEFAULT_DATA CharData, int nAddExp, int nMaxExp )
	{
		CharData.nExp  += nAddExp;

		OBJECT_EXP_DATA ExpData = GameDataManagerChar.Instance.GetExpData (CharData.nLevel);

		while (nMaxExp < CharData.nExp) 
		{
			++CharData.nLevel; 
			CharData.nExp -= nMaxExp;


			nMaxExp = CharStatModification.CHAR_EXP (CharData.nLevel, ExpData.fCharExt [0], ExpData.fCharExt [1], ExpData.fCharExt [2], ExpData.fCharExt [3]); 
		}

		//Debug.Log ("MaxExp : " + nMaxExp.ToString ());
		//Debug.Log ("name : " + CharData.strName + " CharData.nExp : " + CharData.nExp.ToString ());

		Sqlprocess.Instance.UpdateIntgerData( "UPDATE userCharData SET Exp = " + CharData.nExp.ToString() + " WHERE Row = " + CharData.nRow.ToString() );
		Sqlprocess.Instance.UpdateIntgerData( "UPDATE userCharData SET Lv = " + CharData.nLevel.ToString() + " WHERE Row = " + CharData.nRow.ToString() );

		return CharData;

	}

	void UpdateUserExp( int nAddExp )
	{
		USER_DATA UserDat 	= Sqlprocess.Instance.LoadUserData ();

		int TotalExp  		= UserDat.nExp + nAddExp;
		
		OBJECT_EXP_DATA ExpData = GameDataManagerChar.Instance.GetExpData (UserDat.nLevel);
		
		int nMaxExp = CharStatModification.TEAM_EXP (UserDat.nLevel, ExpData.fTeamExt [0], ExpData.fTeamExt [1], ExpData.fTeamExt [2], ExpData.fTeamExt [3]); 
		
		int nLevel = UserDat.nLevel;
		
		while (nMaxExp < TotalExp) 
		{
			++nLevel; 
			nMaxExp = CharStatModification.CHAR_EXP (nLevel, ExpData.fTeamExt [0], ExpData.fTeamExt [1], ExpData.fTeamExt [2], ExpData.fTeamExt [3]); 
			
			TotalExp -= TotalExp;
		}
		
		Debug.Log ("MaxExp : " + nMaxExp.ToString ());
		
		Sqlprocess.Instance.UpdateIntgerData( "UPDATE userData SET exp = " + TotalExp.ToString()  );
		Sqlprocess.Instance.UpdateIntgerData( "UPDATE userData SET level = " + nLevel.ToString() );
	}

	public void ButtonClick( GameObject obj )
	{
		string strName = obj.name;
		
		if( strName.CompareTo( "DataBtn" ) == 0 )
		{
			if( _DataView == null )
			{
				GameObject parent = GameObject.FindWithTag("UI_CAMERA") as GameObject;
				
				GameObject View = Resources.Load("Prefabs/PopUp/ResultDataView", typeof(GameObject)) as GameObject;
				_DataView = NGUITools.AddChild(parent, View);

			}

			_DataView.SetActive( true );
		}
		else if( strName.CompareTo( "MainBtn" ) == 0 )
		{
			UnityEngine.Application.LoadLevel("MainMenuScene");
		}
		else if( strName.CompareTo( "RetryBtn" ) == 0 )
		{
			UnityEngine.Application.LoadLevel(GameManager.DungeonData.strMap);
		}
		else if( strName.CompareTo( "NextBtn" ) == 0 )
		{
			
		}
	}
}
