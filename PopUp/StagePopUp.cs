using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using STORY_ENUM;
using STORY_GAMEDATA;

public class StagePopUp : MonoBehaviour {

    public GameObject[] _MonsIcon;
    public GameObject[] _ItemIcon;

    public GameObject _InfoImage;
    public GameObject _StoryDescLabel;
    public GameObject _StoryTitlelabel;
    public GameObject _parent;
    public GameObject _PageLabel;

    public GameObject[] _StoryName      = null;
    public GameObject[] _StoryChapter   = null;
    public GameObject _InfoView;

	public GameObject _TitleTexImage;

	public Transform _SlotGridTr;

	DUNGEON_DATA _SelectDungeonData;

    int _CurDungeonType     = 0;
    int _CurDongeonChapter  = 0;

    void Start()
    {

		_SelectDungeonData = new DUNGEON_DATA ();

//		DUNGEON_TYPE_DATA dat = GameDataManagerDungeon.Instance.GetDungeonTypeData( 0 );
//
//        if (dat != null)
//        {
//            initDungeonTypeList(dat.nID);
//        }
    }

    void Update()
    {
        //마우스가 내려갔는지?
        if (true == Input.GetMouseButtonDown(0))
        {
            DesctoryInfoView();
        }
    }

    private EventDelegate.Parameter MakeParameter(Object _value, System.Type _type)
    {
        EventDelegate.Parameter param = new EventDelegate.Parameter();  // 이벤트 parameter 생성.
        param.obj = _value;                                             // 이벤트 함수에 전달하고 싶은 값.
        param.expectedType = _type;                                     // 값의 타입.

        return param;
    }

    GameObject CreateTabButton( string strPath, string strCallFuntion, Vector2 StartPos, int nGroup, bool bStarting)
    {
        GameObject ObjTemp = Resources.Load(strPath, typeof(GameObject)) as GameObject;
        GameObject TabObj = NGUITools.AddChild(_parent, ObjTemp);

        TabObj.transform.localPosition  = StartPos;
        TabObj.transform.localScale     = ObjTemp.transform.localScale;

        UIToggle toggle = TabObj.GetComponent<UIToggle>();

        toggle.group = nGroup;
        if (bStarting)
        {
            toggle.startsActive = true;
            toggle.value = true;
        }

        EventDelegate eventBtn = new EventDelegate(this, strCallFuntion);
        eventBtn.parameters[0].obj = TabObj;
        toggle.onChange.Add(eventBtn);

        return TabObj;
    }

//    void initDungeonTypeList( int nTypeID )
//    {
//		List<DUNGEON_TYPE_DATA> DunTypeList = GameDataManagerDungeon.Instance.GetDungeonTypeList();
//
//        float fMovePos = 0;
//
//        for (int i_1 = 0; i_1 < DunTypeList.Count; ++i_1)
//        {
//            GameObject TabBnt = CreateTabButton( "Prefabs/PopUp/DunTypeTab", "OnUpdateStageType", new Vector2(-217.0f + fMovePos, 195.0f), 1, (i_1 == 0));
//            TabButtonCnt TabCnt = TabBnt.GetComponent<TabButtonCnt>();
//            TabCnt.initWithTabButtonData(DunTypeList[i_1]);
//            fMovePos += 95;
//        }
//    }

    void UpdateChapterList( int nType )
    {
		List<DUNGEON_CHAPTER_DATA> DunChapterList = GameDataManagerDungeon.Instance.GetDungeonChapterData(nType);

        if (DunChapterList == null)
        {
            Debug.Log( "Error ChaterList Load" );
            return;
        }

        DestroyChapterList();


        float fMovePos = 0;

        _StoryChapter = new GameObject[DunChapterList.Count];

		//Debug.Log ("DunChapterList.Count : " + DunChapterList.Count.ToString ());
        
        for (int i_1 = 0; i_1 < DunChapterList.Count; ++i_1)
        {
            _StoryChapter[i_1] = CreateTabButton("Prefabs/PopUp/DunChapterTab", "OnUpdateStageLevel", new Vector2(365.0f, 145.0f - fMovePos), 2, (i_1 == 0));
            TabButtonCnt TabCnt = _StoryChapter[i_1].GetComponent<TabButtonCnt>();
            TabCnt.initWithTabButtonData(DunChapterList[i_1]);
            fMovePos += 55;
        }
    }

	void UpdatePageText( int nID )
	{

	}

//    void UpdatePageText( string strText )
//    {
//        UILabel label = _PageLabel.GetComponent<UILabel>();
//        label.text = strText;
//    }
//	string strPath = "Image/PopUp/StagePopUp/mainmenu_A_0" + script.DunGeonTypeData.nType.ToString ();
//	
//	UITexture tex = _TitleTexImage.GetComponentInChildren<UITexture>();
//	tex.mainTexture = Resources.Load(strPath) as Texture;


    void DesctoryInfoView()
    {
        if (_InfoView)
        {
            GameObject.DestroyImmediate(_InfoView);
            _InfoView = null;
            return;
        }
    }

    public void OnButtonClick(GameObject obj)
    {
		GameManager.DungeonData = _SelectDungeonData;

        FactoryManager.Instance.CreatePopUp( POPUP_TYPE.BattleDeck ); 
    }

    public void OnClosePopUp(GameObject obj )
    {
		FactoryManager.Instance.CreatePopUp( POPUP_TYPE.DungeonTypePopUp ); 
    }

    public void OnInfoClick(GameObject obj)
    {
        DesctoryInfoView();

        GameObject View = Resources.Load("Prefabs/PopUp/InfoView") as GameObject;
        _InfoView = NGUITools.AddChild(_parent, View);

        _InfoView.transform.localPosition = new Vector3( 117, 23, 0 );
    }

    /// <summary>
    /// 
    /// </summary>
    public void DestroyStoryList()
    {
        if (_StoryName == null)
        {
            return;
        }


        for (int i_1 = 0; i_1 < _StoryName.Length; ++i_1)
        {
            GameObject.DestroyImmediate(_StoryName[i_1]);
        }

        _StoryName = null;
    }

    public void DestroyChapterList()
    {
        if (_StoryChapter == null)
        {
            return;
        }

        for (int i_1 = 0; i_1 < _StoryChapter.Length; ++i_1)
        {
            GameObject.DestroyImmediate(_StoryChapter[i_1]);
        }

        _StoryChapter = null;
    }

    /// <summary>
    /// 
    /// </summary>
    public void UpdateDungeonList( int nType, int nChapter )
    {
	    DestroyStoryList();

		List<DUNGEON_DATA> DunList = GameDataManagerDungeon.Instance.GetDungeonData(nType, nChapter);

        if (DunList == null)
        {
            Debug.Log("Error DungeonList Load");
            return;
        }

        if (DunList.Count <= 0)
        {
            return;
        }
        UILabel label = null;

        float posY = 118;

        _StoryName = new GameObject[DunList.Count];

        for (int i_1 = 0; i_1 < DunList.Count; ++i_1)
        {
            GameObject ObjTemp = Resources.Load("Prefabs/PopUp/StoryNameBnt", typeof(GameObject)) as GameObject;
			_StoryName[i_1] = Instantiate(ObjTemp, Vector3.zero, Quaternion.identity) as GameObject;

			_StoryName[i_1].transform.parent = _SlotGridTr;
			_StoryName[i_1].transform.localPosition = new Vector3(ObjTemp.transform.localPosition.x, ObjTemp.transform.localPosition.y, 0);
			_StoryName[i_1].transform.localScale = new Vector3(1, 1, 1);


            label = _StoryName[i_1].GetComponentInChildren<UILabel>();
			label.text =  GameDataManagerDungeon.Instance.GetStoryTextData( DunList[i_1].nTitleID ).strText;

            TabButtonCnt BuCnt = _StoryName[i_1].GetComponent<TabButtonCnt>();
            BuCnt.initWithTabButtonData(DunList[i_1]);

            UIButton button = _StoryName[i_1].GetComponent<UIButton>();
            EventDelegate eventBtn = new EventDelegate(this, "OnUpdateDungeonInfo");
            eventBtn.parameters[0].obj = _StoryName[i_1];
            button.onClick.Add(eventBtn);

            posY -= 25;
        }


		_SelectDungeonData = DunList [0];

		UpdateDungeonInfo(_SelectDungeonData);
    }


    public void UpdateDungeonInfo(DUNGEON_DATA dat)
    {
        UpdateStageItem(0);
        UpdateStageMonster(0);
        //UpdateInfoBackImage(0);
        //UpdateStageTileLabel(dat.nTitleID);
		UpdateStageDescLabel(GameDataManagerDungeon.Instance.GetStoryTextData(dat.nDescID).strText);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uStage"></param>
    public void UpdateStageItem( int uStage )
    {
        UITexture tex = null;

        for (int i_1 = 0; i_1 < _ItemIcon.Length; ++i_1)
        {
            tex = _ItemIcon[i_1].GetComponentInChildren<UITexture>();
            tex.mainTexture = Resources.Load("Image/PopUp/StagePopUp/1-1dropitem0" + Random.Range(1, 7).ToString()) as Texture;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="uStage"></param>
    public void UpdateStageMonster(int uStage)
    {
        UITexture tex = null;

        for (int i_1 = 0; i_1 < _MonsIcon.Length; ++i_1)
        {
            tex = _MonsIcon[i_1].GetComponentInChildren<UITexture>();
            tex.mainTexture = Resources.Load("Image/PopUp/StagePopUp/Mob0" + Random.Range(1, 7).ToString()) as Texture;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="uStage"></param>
    public void UpdateInfoBackImage(int uStage)
    {
        UITexture tex = null;

        tex = _InfoImage.GetComponentInChildren<UITexture>();
        tex.mainTexture = Resources.Load("Image/PopUp/StagePopUp/Dungeon-Image0" + Random.Range(1, 3).ToString()) as Texture;
    }


    public void UpdateStageTileLabel( int nTileID )
    {
        UILabel label = _PageLabel.GetComponent<UILabel>();

		string tile = label.text + " " + GameDataManagerDungeon.Instance.GetStoryTextData(nTileID).strText;

        label = _StoryTitlelabel.GetComponent<UILabel>();
        label.text = tile;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uStage"></param>
    public void UpdateStageDescLabel(string strText)
    {
        UILabel label = _StoryDescLabel.GetComponent<UILabel>();
        label.text = strText;
    }

    public void OnUpdateDungeonInfo(GameObject obj)
    {
        TabButtonCnt BntCnt = obj.GetComponent<TabButtonCnt>();

		_SelectDungeonData = BntCnt.DunInfoData;
		UpdateDungeonInfo(_SelectDungeonData );

        //Debug.Log( BntCnt.DunInfoData.nID.ToString() );

	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    public void OnUpdateStageLevel(GameObject obj)
    {
        UIToggle Toggle = obj.GetComponent<UIToggle>();

        if (Toggle.value == false)
        {
            return;
        }

        TabButtonCnt Tab = obj.GetComponent<TabButtonCnt>();

        if (Tab.DunChapterData.nID == _CurDongeonChapter)
        {
            return;
        }

        //UpdatePageText(Tab.DunChapterData.strName);
        UpdateDungeonList(_CurDungeonType, Tab.DunChapterData.nID);

        _CurDongeonChapter = Tab.DunChapterData.nID;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
	/// 
	public void OnUpdateStageType(DUNGEON_TYPE_DATA dat)
	{
		string strPath = "Image/PopUp/StagePopUp/mainmenu_A_0" + dat.nType.ToString();

		UITexture tex = _TitleTexImage.GetComponentInChildren<UITexture>();
		tex.mainTexture = Resources.Load(strPath) as Texture;

		UpdateChapterList(dat.nID);

		_CurDungeonType = dat.nID;
	}
//    public void OnUpdateStageType(GameObject obj)
//    {
//        UIToggle Toggle = obj.GetComponent<UIToggle>();
//
//        if (Toggle.value == false)
//        {
//            return;
//        }
//
//        TabButtonCnt Tab = obj.GetComponent<TabButtonCnt>();
//
//        if (Tab.DunTypeData.nID == _CurDungeonType )
//        {
//            return;
//        }
//
//        UpdateChapterList(Tab.DunTypeData.nID);
//
//        _CurDungeonType = Tab.DunTypeData.nID;
//    }

}
