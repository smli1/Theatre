using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour {
	
	GameObject limitedAreaGameobejct;
	Vector3 targetPos;
	float count = 0;
	float speed = 10;

	// Use this for initialization
	void Start () {
		targetPos = transform.position;
		limitedAreaGameobejct = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		count += Time.fixedDeltaTime;
		if (count >= 0.5f) {
			count = 0;
			float rx, ry, rz;
			rx = Random.Range (-5,5);
			ry = Random.Range (-5,5);
			rz = Random.Range (-5,5);
			speed = Random.Range (10, 20);

			if( transform.position.y + ry < 0){
				ry = 1;
			}
			if (Vector3.Distance(transform.position + new Vector3 (rx, ry, rz), limitedAreaGameobejct.transform.position) >= 10) {
				targetPos = transform.position + (limitedAreaGameobejct.transform.position - transform.position).normalized * Random.Range (1, 5);
			} else {
				targetPos = transform.position + new Vector3 (rx, ry, rz);
			}
		} else {
			if(Vector3.Distance(transform.position,targetPos) >= 0.5f){
				transform.position += (targetPos - transform.position ).normalized * Time.fixedDeltaTime * speed;
			}
		}

	}
}
