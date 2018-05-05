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
    private float limitedCount = 2;

    private float disMax = 250;
	void Start(){
		
	}

	void Update () {
        

        Vector3 targetPos = target.transform.position;
        targetPos += new Vector3(0, terrian.terrainData.GetHeight((int)transform.position.x, (int)transform.position.z) / 2.0f, 0);
        targetPos += offset;
        targetPos += avoidObstacleOffset;

        transform.position = Vector3.Lerp (transform.position, targetPos ,Time.deltaTime);

        float disVector = (chaseTarget.transform.position - target.transform.position).magnitude;
        Vector3 lookAtPoint = (chaseTarget.transform.position - target.transform.position).normalized;
        lookAtPoint = lookAtPoint * (disVector / 2.0f + Mathf.Clamp(disVector/disMax, 0f, 1f) * disVector / 2);
        lookAtPoint = ((chaseTarget.transform.position - lookAtPoint) - Camera.main.transform.position).normalized;


        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookAtPoint), Time.deltaTime);

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
