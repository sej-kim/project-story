using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour {

	bool _bStop = false;

	//Vector3 _MovePos;
    //protected NavMeshAgent _nv = null;

	// Use this for initialization
	void Start () {
        //_nv = gameObject.GetComponentInChildren<NavMeshAgent>();

		//_MovePos = new Vector3 ();
    }


//	public bool IsMove(Vector3 postion, float fAtDist)
//    {
//		float fDist = Mathf.Abs(transform.localPosition.x - postion.x);
//
//
//		if ( fDist <= fAtDist )
//        {
//            return false;
//        }
//
//        return true;
//    }
//
//	public bool IsAttack( Vector3 postion, float fAtDist )
//    {
//		float fDist = Mathf.Abs(transform.localPosition.x - postion.x);
//
//		if (fDist <= fAtDist)
//        {
//            return true;
//        }
//
//        return false;
//    }

    public bool Move( Vector3 postion, float fSpeed ,float time )
    {
		if (_bStop) 
		{
			return false;
		}
        //_nv.destination = postion;

		float Dist = Mathf.Abs(transform.localPosition.x - postion.x);

		//Debug.Log (Dist.ToString ());

		if (Dist <= 0.1) 
		{
			return false;
		}

		float move = fSpeed * time;
		float movePos = postion.x > transform.position.x ? move : -move;

		Vector3 LocalPos = transform.localPosition;
		transform.localPosition = new Vector3 (LocalPos.x + movePos, LocalPos.y, LocalPos.z);

		return true;
    }

    public void Stop()
    {
		_bStop = true;
        //_nv.Stop();
    }

    public void Resum()
    {
		_bStop = false;
        //_nv.Resume();
    }
}
