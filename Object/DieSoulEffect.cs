using UnityEngine;
using System.Collections;
using STORY_ENUM;

public class DieSoulEffect : MonoBehaviour {

	public GameObject target;
	//public Vector3 movePos;
	bool _UpMove = true;

	// Use this for initialization
	void Start () 
	{
		Invoke ("MoveToTarget", 1.5f);
	}

	void Update()
	{
		if (_UpMove) 
		{
			float fMoveY = 1.5f * Time.deltaTime;

			transform.position = new Vector3( transform.position.x, transform.position.y + fMoveY, transform.position.z );
		}
	}
	

	void MoveToTarget()
	{
		_UpMove = false;

		Character charScript = target.GetComponent< Character> ();
		Vector3 movePos = charScript._EffectPosDictionary [eEffectPos.FXDummy_breast.ToString ()].position;

		Hashtable hash = new Hashtable();
		hash.Add("position", movePos);
		hash.Add("Speed", 3.0f);
		hash.Add("oncomplete", "Destroy");
		
		iTween.MoveTo( gameObject, hash );
	}

	void Destroy()
	{
		//Debug.Log("Destroy");
		GameObject ObjTemp = Resources.Load("Prefabs/Effect/Object/fx_charcter_die_hit", typeof(GameObject)) as GameObject;
		ObjTemp = Instantiate(ObjTemp, transform.position, transform.rotation) as GameObject;
		
		GameObject.Destroy ( ObjTemp, 1.0f );
		
		GameObject.DestroyImmediate( gameObject );
	}
}
