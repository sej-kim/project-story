using UnityEngine;
using System.Collections;

public class RootCamera : MonoBehaviour {

    public Camera _Camera;
	// Use this for initialization
	void Start () {
        _Camera.aspect = 800.0f / 480.0f;
	}
}
