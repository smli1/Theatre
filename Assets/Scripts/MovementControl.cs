using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour {

	[SerializeField]
	float speed = 5;
	Vector3 targetPos;
	GameObject player;
	float timeCount = 0;

	// Use this for initialization
	void Start () {
		targetPos = transform.position;
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			//Debug.Log ("Click");
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 1000)) {
				//Debug.Log (hit.point);
				targetPos = hit.point + Vector3.up * 0.5f;
			}

			if (Vector3.Distance (transform.position, targetPos) >= 0.5f) {
				transform.position = Vector3.Lerp(transform.position,transform.position + (targetPos - transform.position).normalized * speed ,Time.deltaTime);
			}
			timeCount = 0;
		} else {
			timeCount += Time.deltaTime;
			if(Vector3.Distance(transform.position, player.transform.position+Vector3.up) >= 0.75f && timeCount >= 3f){
				transform.position = Vector3.Lerp(transform.position,transform.position + (player.transform.position - transform.position + Vector3.up).normalized * speed ,Time.deltaTime);
				//transform.position = (player.transform.position - transform.position + Vector3.up).normalized * Time.deltaTime * speed;
			}
		}
	}
}
