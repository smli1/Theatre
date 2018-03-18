using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : MonoBehaviour {

	bool isActed = false;

	void OnCollisionEnter(Collision collision){
		if(!isActed){
			Debug.Log ("! "+collision.collider.gameObject.tag);
			if (collision.collider.gameObject.tag != "missionItem") {
				return;
			}
			Debug.Log ("Hit!");
			//isActed = true;
			GetComponent<Rigidbody> ().AddForce (Vector3.up * 100);
		}
	}
}
