using UnityEngine;
using System.Collections;

public class MainMenuScene : MonoBehaviour {

    void Awake()
    {
		GameDataManagerDungeon DataMgr = GameDataManagerDungeon.Instance;
		GameDataManagerChar CharMgr = GameDataManagerChar.Instance;
		GameDataManagerItem ItemMgr = GameDataManagerItem.Instance;
		Sqlprocess sql = Sqlprocess.Instance;

		//Application.LoadLevelAsync( "BattleScene" );
    }
}
