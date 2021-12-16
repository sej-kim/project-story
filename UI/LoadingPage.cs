using UnityEngine;
using System.Collections;


public class LoadingPage : MonoBehaviour {

	public UISlider  Script;
	public UILabel  textScript;
	
	AsyncOperation   async;
	
	bool IsLoadGame = false;
	
	public IEnumerator StartLoad( string strSceneName )
	{
		if (IsLoadGame == false) 
		{
			IsLoadGame = true;
			
			AsyncOperation async = Application.LoadLevelAsync ( strSceneName );
			
			while(async.isDone == false) 
			{
				float p = async.progress *100f;
				int pRounded = Mathf.RoundToInt(p);
				
				textScript.text = pRounded.ToString();
				
				//progress 변수로 0.0f ~ 1.0f로 넘어 오기에 이용하면 됩니다.
				Script.sliderValue = async.progress;

				Debug.Log( textScript.text );
				
				yield return true;
			}
		}
	}

	void Awake()
	{
		UITexture tex = GetComponent< UITexture > ();
		tex.mainTexture = Resources.Load ("Image/LodingBg/Loading" + Random.Range (1, 9).ToString ()) as Texture;
	}


	void Start()
	{


		StartCoroutine( "StartLoad", GameManager.DungeonData.strMap );
	}
	
//	float fTime = 0.0f;
//	
//	//로딩 페이지에서 연속으로 애니메이션 만들때 Update 함수 내에서 만들면 됩니다.
//	void Update () 
//	{
//		fTime += Time.deltaTime;
//		
//		if( fTime >= 1.0f )
//		{
//			//fTime = 0.0f; 
//		}
//		
//		Script.sliderValue = fTime;
//	}
}
