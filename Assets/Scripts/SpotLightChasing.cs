using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightChasing : MonoBehaviour {

	public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(target){
			transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
		}
	}
}
