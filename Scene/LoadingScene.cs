using UnityEngine;
using System.Collections;
using STORY_ENUM;

public class LoadingScene : MonoBehaviour 
{
    //private string path = "file:///sdcard/download/AndroidLocal.unity3d";
    //private string path = "https://dl.dropbox.com/s/4y7l8c7v9to9vz6/AndroidLocal.unity3d?dl=0";

    public void OnLoadingFinish()
    {
        UnityEngine.Application.LoadLevel("MainMenuScene");

        //StartCoroutine( LoadAsset( path, 3 ) );
    }


    IEnumerator LoadAsset(string path, int version)
    {
        while (!Caching.ready)
        {
            yield return null;
        }

        using (WWW asset = WWW.LoadFromCacheOrDownload(path, version))
        {
            yield return asset;

            if (asset.error != null)
            {
                Debug.Log("error : " + asset.error.ToString());
            }
            else
            {

            }

        }
    }


}
