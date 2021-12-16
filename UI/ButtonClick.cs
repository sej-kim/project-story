using UnityEngine;
using System.Collections;
using STORY_ENUM;

public class ButtonClick : MonoBehaviour {

//    public void OnStartClick(GameObject obj)
//    {
//		UnityEngine.Application.LoadLevel(GameManager.DungeonData.strMap);
//    }
//
//    public void OnBackClick(GameObject obj)
//    {
//        FactoryManager.Instance.CreatePopUp(POPUP_TYPE.StagePopUp);
//    }

    public void OnBattleDeck(GameObject obj )
    {
        FactoryManager.Instance.CreatePopUp(POPUP_TYPE.BattleDeck);
    }

    public void OnBackMainMenu( GameObject obj )
    {
		//FactoryManager.Instance.CreatePopUp (POPUP_TYPE.ResultPopUp);
        UnityEngine.Application.LoadLevel("MainMenuScene");
    }
    
}
