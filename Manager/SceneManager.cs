using UnityEngine;
using System.Collections;
using STORY_ENUM;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _instance;

    public static SceneManager instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(SceneManager)) as SceneManager;

                if (!_instance)
                {
                    GameObject Container = new GameObject();
                    Container.name = "Xml_GameSceneManager";

                    _instance = Container.AddComponent(typeof(SceneManager)) as SceneManager;
                }
            }

            return _instance;
        }
    }

	// Use this for initialization
	void Start () 
    {

	}

    public bool LoadScene( SCENE_TYPE type )
    {
        string strScene = "LoadingScene";

        switch (type)
        {
            case SCENE_TYPE.EMPTY_SCENE:
                {
                    strScene = "EmptyScene";
                }
                break;
            case SCENE_TYPE.LOGO_SCENE:
                {
                    strScene = "LogoScene";
                }
                break;
            case SCENE_TYPE.LOADING_SCENE:
                {
                    strScene = "LoadingScene";
                }
                break;
            case SCENE_TYPE.MAINMENU_SCENE:
                {
                    strScene = "MainMenuScene";
                }
                break;
            case SCENE_TYPE.BATTLE_SCENE:
                {
                    strScene = "BattleScene";
                }
                break;

        }

        UnityEngine.Application.LoadLevel( strScene );

        return true;
    }
}
