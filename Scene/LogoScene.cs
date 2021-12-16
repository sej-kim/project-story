using UnityEngine;
using System.Collections;
using STORY_ENUM;

public class LogoScene : MonoBehaviour {

    public void OnLogoFinish()
    {
        UnityEngine.Application.LoadLevel("LoadingScene");
    }
}
