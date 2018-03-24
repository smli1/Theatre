using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour {

	private Vector3 origialScale;
	private bool isShaking = false;

	void Start(){
		origialScale = transform.localScale;
	}

	void OnTriggerEnter(Collider collider){
		if (collider.transform.parent == gameObject.transform)
			return;
		if(!isShaking){
			isShaking = true;
			StartCoroutine (ShakeSelf (10));
			if(transform.childCount > 0){
				foreach(Rigidbody temp in transform.GetComponentsInChildren<Rigidbody>() ){
					//if(temp.gameObject.tag == "missionItem"){
					//	Debug.Log ("Update Mission!");
					//	MissionTargetCount.updateTarget (1);
					//}
					temp.transform.parent = null;
					temp.isKinematic = false;
				}
			}
		}
	}

	IEnumerator ShakeSelf(int shakeTime){
		gameObject.GetComponent<Renderer> ().material.color = new Color (Random.Range(0.2f,1.0f),Random.Range(0.2f,1.0f),Random.Range(0.2f,1.0f));
		for(int i = 0 ; i < shakeTime ; i++){
			transform.localScale = origialScale * Random.Range (0.95f,1.05f);
			yield return new WaitForSeconds (0.03f);
		}
		transform.localScale = origialScale;
		isShaking = false;
	}
}
