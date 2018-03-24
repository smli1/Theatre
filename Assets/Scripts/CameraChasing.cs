using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChasing : MonoBehaviour {

	public GameObject target;
	public GameObject chaseTarget;
	float zoomValue;
	public Vector3 offset;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (transform.position, target.transform.position+offset,Time.deltaTime);
		//Vector3 characterScreenPos = Camera.main.WorldToScreenPoint (target.transform.position);
		//if (characterScreenPos.x >= 0 && characterScreenPos.x <= Screen.width && characterScreenPos.y >= 0 && characterScreenPos.y <= Screen.height) {
		//transform.LookAt (chaseTarget.transform.position - (chaseTarget.transform.position - target.transform.position).normalized * (chaseTarget.transform.position - target.transform.position).magnitude/2.0f);

		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(((chaseTarget.transform.position - (chaseTarget.transform.position - target.transform.position).normalized * (chaseTarget.transform.position - target.transform.position).magnitude/2.0f) - Camera.main.transform.position).normalized), Time.deltaTime);
		//}

		//Debug.Log( Camera.main.WorldToScreenPoint (target.transform.position));
	}
}
