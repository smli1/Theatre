using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyMovement : MonoBehaviour {

	[SerializeField]
	float MaxSpeed = 10;
	private float speed = 0;
    Vector3 destPos;
    [SerializeField]
    GameObject target;
	float timeCount = 0;
	private Vector3 lastPoint;
	private bool enable;


	void Start () {
		enable = true;
		lastPoint = Vector3.zero;
		destPos = transform.position;
        //target = GameObject.FindGameObjectWithTag ("Player");
        //playerSpeed = target.GetComponent<Movement> ().currentSpeed;
	}
	

	void Update () {
        //playerSpeed = target.GetComponent<Movement> ().currentSpeed;
        //playerSpeed = 1;
		if (lastPoint == transform.position) {
			speed = 0;
		} 
		lastPoint = transform.position;
		if (Input.GetMouseButton (0) && enable) {
			//Debug.Log ("Click");
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 100000)) {
				//Debug.Log (hit.point);
				destPos = hit.point;
			}
			if (speed < MaxSpeed) {
				speed += MaxSpeed * 0.1f;
			} else {
				speed = MaxSpeed;
			}

			if (Vector3.Distance (transform.position, destPos) >= 0.5f) {
				transform.position = Vector3.Lerp(transform.position,transform.position + (destPos - transform.position).normalized * speed ,Time.deltaTime / 2);
			}
			timeCount = 0;
		} else {
			timeCount += Time.deltaTime;
			float distance = Vector3.Distance (transform.position, target.transform.position + Vector3.up);
			if(timeCount >= 3f){
                //transform.position = Vector3.Lerp(transform.position,transform.position + (target.transform.position - transform.position - Vector3.right * 2 ).normalized * MaxSpeed * Mathf.Clamp(distance + 0.25f,1,2) ,Time.deltaTime /2);
				//transform.position = (player.transform.position - transform.position + Vector3.up).normalized * Time.deltaTime * speed;
			}
		}
	}

	public void setEnable(bool b){
		enable = b;
	}
}
