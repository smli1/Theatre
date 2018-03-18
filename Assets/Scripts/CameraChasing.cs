using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChasing : MonoBehaviour {

	public GameObject target;
	float zoomValue;
	public Vector3 offset;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (transform.position, target.transform.position+offset,0.5f);
	}
}
