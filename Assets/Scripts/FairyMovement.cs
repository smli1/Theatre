using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyMovement : MonoBehaviour {

	[SerializeField]
	float MaxSpeed = 5;
	private float speed = 0;
	private float playerSpeed;
	Vector3 targetPos;
	GameObject player;
	float timeCount = 0;
	private Vector3 lastPoint;

	// Use this for initialization
	void Start () {
		lastPoint = Vector3.zero;
		targetPos = transform.position;
		player = GameObject.FindGameObjectWithTag ("Player");
		playerSpeed = player.GetComponent<Movement> ().currentSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		playerSpeed = player.GetComponent<Movement> ().currentSpeed;
		if (lastPoint == transform.position) {
			speed = 0;
		} 
		lastPoint = transform.position;
		if (Input.GetMouseButton (0)) {
			//Debug.Log ("Click");
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 1000)) {
				//Debug.Log (hit.point);
				targetPos = hit.point + Vector3.up * 0.5f;
			}
			if (speed < MaxSpeed) {
				speed += MaxSpeed * 0.1f;
			} else {
				speed = MaxSpeed;
			}

			if (Vector3.Distance (transform.position, targetPos) >= 0.5f) {
				transform.position = Vector3.Lerp(transform.position,transform.position + (targetPos - transform.position).normalized * speed ,Time.deltaTime);
			}
			timeCount = 0;
		} else {
			timeCount += Time.deltaTime;
			float distance = Vector3.Distance (transform.position, player.transform.position + Vector3.up);
			if(distance >= 0.75f && timeCount >= 3f){
				transform.position = Vector3.Lerp(transform.position,transform.position + (player.transform.position - transform.position + Vector3.up).normalized * (playerSpeed == 0 ? MaxSpeed : playerSpeed) * Mathf.Clamp(distance + 0.25f,1,2) ,Time.deltaTime);
				//transform.position = (player.transform.position - transform.position + Vector3.up).normalized * Time.deltaTime * speed;
			}
		}
	}
}
