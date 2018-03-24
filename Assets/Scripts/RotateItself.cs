using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItself : MonoBehaviour {

	public void rotateForce(float force){
		transform.Rotate(new Vector3(0,0,-5f),Space.Self);
		//Debug.Log ("Rotate!");
	}
}
