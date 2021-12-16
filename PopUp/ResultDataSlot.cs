using UnityEngine;
using System.Collections;

using STORY_GAMEDATA;

public class ResultDataSlot : MonoBehaviour {

	public OBJECT_DEFAULT_DATA _CharData;

	public UILabel Attacklabel;
	public UILabel DefenceLabel;

	public Transform CharIconParent;

	public void initWithResultDataSlot (OBJECT_DEFAULT_DATA dat)
	{
		GameObject Icon = Resources.Load( "Prefabs/PopUp/CharIcon") as GameObject;
		Icon = Instantiate(Icon, Vector3.zero, Quaternion.identity) as GameObject;
		Icon.transform.parent = CharIconParent;
		Icon.transform.localPosition = new Vector3( -95, 6, 0 );
		Icon.transform.localScale = new Vector3( 0.7f, 0.7f, 1 );
			
		CharIcon IconScript = Icon.GetComponent< CharIcon >();
		IconScript.initWithCharData( dat );
	}
}
