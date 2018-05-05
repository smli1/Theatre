using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyMovement : MonoBehaviour {

	[SerializeField]
	float MaxSpeed = 5;
	private float speed = 0;
	private float playerSpeed;
    Vector3 destPos;
    [SerializeField]
    GameObject target;
	float timeCount = 0;
	private Vector3 lastPoint;
	private bool enable;

	// Use this for initialization
	void Start () {
		enable = true;
		lastPoint = Vector3.zero;
		destPos = transform.position;
        //target = GameObject.FindGameObjectWithTag ("Player");
        //playerSpeed = target.GetComponent<Movement> ().currentSpeed;
        playerSpeed = 1;
	}
	
	// Update is called once per frame
	void Update () {
        //playerSpeed = target.GetComponent<Movement> ().currentSpeed;
        playerSpeed = 1;
		if (lastPoint == transform.position) {
			speed = 0;
		} 
		lastPoint = transform.position;
		if (Input.GetMouseButton (0) && enable) {
			//Debug.Log ("Click");
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 1000)) {
				//Debug.Log (hit.point);
				destPos = hit.point + Vector3.up * 0.5f;
			}
			if (speed < MaxSpeed) {
				speed += MaxSpeed * 0.1f;
			} else {
				speed = MaxSpeed;
			}

			if (Vector3.Distance (transform.position, destPos) >= 0.5f) {
				transform.position = Vector3.Lerp(transform.position,transform.position + (destPos - transform.position).normalized * speed ,Time.deltaTime);
			}
			timeCount = 0;
		} else {
			timeCount += Time.deltaTime;
			float distance = Vector3.Distance (transform.position, target.transform.position + Vector3.up);
			if(distance >= 0.75f && timeCount >= 3f){
				transform.position = Vector3.Lerp(transform.position,transform.position + (target.transform.position - transform.position + Vector3.up).normalized * (playerSpeed <= 0 ? MaxSpeed : playerSpeed) * Mathf.Clamp(distance + 0.25f,1,2) ,Time.deltaTime);
				//transform.position = (player.transform.position - transform.position + Vector3.up).normalized * Time.deltaTime * speed;
			}
		}
	}

	public void setEnable(bool b){
		enable = b;
	}
}
