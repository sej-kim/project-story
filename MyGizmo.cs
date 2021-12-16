using UnityEngine;
using System.Collections;

public class MyGizmo : MonoBehaviour {

	public Color _color = Color.yellow;
	public float _raduis = 0.2f;

	void OnDrawGizmos()
	{
		Gizmos.color = _color;
		Gizmos.DrawSphere( transform.position, _raduis );
	}
}
