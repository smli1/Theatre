using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChasing : MonoBehaviour {

	public GameObject target;
	public GameObject chaseTarget;
	float zoomValue;
	public Vector3 offset;
	public Terrain terrian;
	private Vector3 avoidObstacleOffset;

	void Start(){
		
	}

	void Update () {
		transform.position = Vector3.Lerp (transform.position, target.transform.position+offset+new Vector3(0,terrian.terrainData.GetHeight((int)transform.position.x,(int)transform.position.z) / 2f,0) + avoidObstacleOffset ,Time.deltaTime);
		//Vector3 characterScreenPos = Camera.main.WorldToScreenPoint (target.transform.position);
		//if (characterScreenPos.x >= 0 && characterScreenPos.x <= Screen.width && characterScreenPos.y >= 0 && characterScreetnPos.y <= Screen.height) {
		//transform.LookAt (chaseTarget.transform.position - (chaseTarget.transform.position - target.transform.position).normalized * (chaseTarget.transform.position - target.transform.position).magnitude/2.0f);

		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(((chaseTarget.transform.position - (chaseTarget.transform.position - target.transform.position).normalized * (chaseTarget.transform.position - target.transform.position).magnitude/2.0f) - Camera.main.transform.position).normalized), Time.deltaTime);
		//}

		//Debug.Log( Camera.main.WorldToScreenPoint (target.transform.position));
	}

	void FixedUpdate(){
		Ray r1 = new Ray (transform.position + transform.right * 2,transform.forward);
		Ray r2 = new Ray (transform.position - transform.right * 2,transform.forward);
		RaycastHit hit1 = new RaycastHit();
		RaycastHit hit2 = new RaycastHit();
		if(Physics.Raycast(r1, out hit1 ,200f) || Physics.Raycast(r2, out hit2 ,200f)){
			
			float distance1 = Vector3.Distance(hit1.point,transform.position);
			float distance2 = Vector3.Distance(hit2.point,transform.position);
			//Debug.Log (hit.collider.name+" : " +distance);
			if (distance1 <= 5f || distance2 <= 5) {
				avoidObstacleOffset -= new Vector3 (0, 0, 0.5f);
			} else {
				if(avoidObstacleOffset.z < 0){
					avoidObstacleOffset += new Vector3 (0, 0, 0.1f);
				}else{
					avoidObstacleOffset = Vector3.zero;
				}
			}
		}
	}
}
