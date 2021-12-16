using UnityEngine;
using System.Collections;
using STORY_GAMEDATA;

public class TabButtonCnt : MonoBehaviour {

    DUNGEON_TYPE_DATA       _DunTypeData;
    DUNGEON_CHAPTER_DATA    _DunChapterData;
    DUNGEON_DATA            _DunInfoData;

    void initTabButton( string strIn, string strOut )
    {
        UISprite[] sp = GetComponentsInChildren<UISprite>();

        sp[0].spriteName = strOut;
        sp[1].spriteName = strIn;

        BoxCollider col = GetComponent<BoxCollider>();
        col.size = sp[0].localSize;
    }

    public bool initWithTabButtonData(DUNGEON_TYPE_DATA dat)
    {
        _DunTypeData = dat;
        
        initTabButton(dat.strTabImageIn, dat.strTabImageOut);

        return true;
    }

    public bool initWithTabButtonData(DUNGEON_CHAPTER_DATA dat)
    {
		//Debug.Log (dat.strImageIn);
		//Debug.Log (dat.strImageOut);
        _DunChapterData = dat;
		initTabButton("stage_button_1_A", "stage_button_1_B");
        return true;
    }

    public bool initWithTabButtonData(DUNGEON_DATA dat)
    {
        _DunInfoData = dat;
        return true;
    }

    public DUNGEON_TYPE_DATA DunTypeData
    {
        get
        {
            return _DunTypeData;
        }
    }

    public DUNGEON_CHAPTER_DATA DunChapterData
    {
        get
        {
            return _DunChapterData;
        }
    }

    public DUNGEON_DATA DunInfoData
    {
        get
        {
            return _DunInfoData;
        }
    }
}
