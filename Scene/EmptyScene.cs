using UnityEngine;
using System.Collections;
using STORY_ENUM;

public class EmptyScene : MonoBehaviour {

    // Use this for initialization
	void Start () 
    {
		//GameDataManagerDungeon DataMgr = GameDataManagerDungeon.Instance;
		//GameDataManagerChar CharMgr = GameDataManagerChar.Instance;
		//Sqlprocess sql = Sqlprocess.Instance;

        UnityEngine.Application.LoadLevel( "LogoScene" );
    }
}
