using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour {

	void OnCollisionEnter(Collision obj){
		if(obj.gameObject.GetComponent<Rigidbody>() != null){
			obj.gameObject.GetComponent<Rigidbody> ().useGravity = false;
		}
	}

	void OnCollisionExit(Collision obj){
		if(obj.gameObject.GetComponent<Rigidbody>() != null){
			obj.gameObject.GetComponent<Rigidbody> ().useGravity = true;
		}
	}
}
