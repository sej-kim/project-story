using UnityEngine;
using System.Collections;

public class ReulstDataView : MonoBehaviour {

	public GameObject _VictoryIcon;
	public Transform _BgParent;
	// Use this for initialization
	void Start () 
	{
		GameObject[] Objects = GameObject.FindGameObjectsWithTag( "PLAYER" );
		CreateSlot (Objects, -140.0f);


		Objects = GameObject.FindGameObjectsWithTag( "MONSTER" );
		CreateSlot (Objects, 140.0f);
	}

	void CreateSlot( GameObject[] ObjList, float fPosX )
	{
		float fPosY = 130.0f;

		int nIndex = 0;

		foreach( GameObject objtemp in ObjList )
		{
			if( nIndex > 4 )
			{
				break;
			}

			Character ObjScript = objtemp.GetComponent< Character >();
			
			GameObject DataSlot = Resources.Load( "Prefabs/PopUp/ResultDataSlot") as GameObject;
			DataSlot = Instantiate(DataSlot, Vector3.zero, Quaternion.identity) as GameObject;

			DataSlot.transform.parent = _BgParent;
			DataSlot.transform.localPosition = new Vector3( fPosX, fPosY, 0 );
			DataSlot.transform.localScale = new Vector3( 1, 1, 1 );


			ResultDataSlot SlotScript = DataSlot.GetComponent< ResultDataSlot >();
			SlotScript.initWithResultDataSlot( ObjScript.ObjDefaultData );

			fPosY -= 75.0f;
			++nIndex;
		}

		//Grid.transform.localPosition = new Vector3 (0, 140, 0);

	}

	public void CloseView( GameObject obj )
	{
		gameObject.SetActive (false);
	}
}
