using UnityEngine;
using System.Collections;

public class FollowText : MonoBehaviour 
{
	public GameObject target = null;
	public Vector3 tPos;
	public UIFont[] Fons;

	Vector3 TextPos;


	void Awake()
	{
		GameObject.Destroy (gameObject, 3);
	}

	Camera worldCam;
	Camera guiCam;

	void Start()
	{
		//월드좌표의 카메라객체입니다.
	 	worldCam = NGUITools.FindCameraForLayer(target.layer);
		//GUI객체의 카메라 객체입니다.
		guiCam = NGUITools.FindCameraForLayer(gameObject.layer);


		TextPos = guiCam.ViewportToWorldPoint(worldCam.WorldToViewportPoint(tPos));
		//transform.localScale = new Vector3 (2, 2, 2);
	}

	public void SetDamageText ( int nType, string strDamage )
	{
		UILabel label = gameObject.GetComponent< UILabel > ();
		label.bitmapFont = Fons [nType];
		label.text = strDamage;
	}
	public void Destroy()
	{
		target = null;
		GameObject.DestroyImmediate (gameObject);
	}

	// Update is called once per frame
	void Update () 
	{
		if (target == null) 
		{
			return;
		}
		//Vector3 pos = guiCam.ViewportToWorldPoint(worldCam.WorldToViewportPoint(tPos));

		//pos.y += 0.35f;

		float fPosY = 0.2f * Time.deltaTime;

		transform.position = new Vector3( TextPos.x, transform.position.y + fPosY, TextPos.z );

	}
}
