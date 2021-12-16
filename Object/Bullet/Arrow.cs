using UnityEngine;
using System.Collections;

public class Arrow : Bullet {

	// Use this for initialization
    public void Fire( Vector3 pos )
    {
        
        Hashtable hash = new Hashtable();
        hash.Add("position", pos);
        hash.Add("Speed", 3.0f);
		hash.Add("oncompletetarget", gameObject);
        hash.Add("oncomplete", "Destroy");

        iTween.MoveTo( gameObject, hash );
    }

    void Destroy()
    {
        //Debug.Log("Destroy");
		GameObject ObjTemp = Resources.Load("Prefabs/Effect/Object/fx_hit_damage_02", typeof(GameObject)) as GameObject;
        ObjTemp = Instantiate(ObjTemp, transform.position, transform.rotation) as GameObject;

		GameObject.Destroy ( ObjTemp, 0.5f );

        GameObject.DestroyImmediate( gameObject );
    }
}
