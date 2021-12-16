using UnityEngine;
using System.Collections;
using STORY_ENUM;


public class FactoryManager : SingletonControl<FactoryManager> 
{
    public GameObject[] _PopUpList;
    public GameObject[] _UIList;
    private GameObject _OpenPopup = null;
    // Use this for initialization

    public string POPUP_PATH( string strpath )
    {
        //return "Prefabs/PopUp/" + strpath;
		return "Prefabs/PopUp/" + strpath;
    }

    public string CHAR_PATH(string strpath)
    {
        return "Prefabs/Chars/" + strpath;
    }


	public GameObject CreatePopUp( POPUP_TYPE type )
    {
		//yield return null;

        DeletePopUp();

		GameObject parent = GameObject.FindWithTag("UI_CAMERA") as GameObject;

        GameObject obj = Resources.Load(POPUP_PATH(type.ToString()), typeof(GameObject)) as GameObject;
        _OpenPopup = NGUITools.AddChild(parent, obj);

        _OpenPopup.transform.localScale = obj.transform.localScale;
        _OpenPopup.transform.localPosition = obj.transform.localPosition;

		return _OpenPopup;

    }

    public GameObject GetPopUP()
    {
        return _OpenPopup;
    }

    public void DeletePopUp()
    {
        if (_OpenPopup != null)
        {
            DestroyObject(_OpenPopup);
            _OpenPopup = null;
        }
    }

    public GameObject CreateUIObject()
    {
        return null;
    }

	public GameObject CreateEffect( eEFFECT_TYPE type )
	{

		return null;
	}








































}
