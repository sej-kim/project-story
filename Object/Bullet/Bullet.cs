using UnityEngine;
using System.Collections;
using STORY_GAMEDATA;

public class Bullet : MonoBehaviour
{
	public void Fire( BULLET_DATA dat )
	{
		Hashtable hash = new Hashtable();
		hash.Add("position", dat.tPos);
		hash.Add("Speed", dat.fSpeed);
		hash.Add("oncomplete", "Destroy");
		
		iTween.MoveTo( gameObject, hash );
	}
	
	void Destroy()
	{
		//Debug.Log("Destroy");
		GameObject ObjTemp = Resources.Load("Prefabs/Effect/HitEffect02", typeof(GameObject)) as GameObject;
		ObjTemp = Instantiate(ObjTemp, transform.position, transform.rotation) as GameObject;
		
		GameObject.Destroy ( ObjTemp, 2 );
		
		GameObject.DestroyImmediate( gameObject );
	}
}
