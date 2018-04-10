using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour {


	private Vector3 origialScale;
	private bool isShaking = false;

	public void Start(){
		origialScale = transform.localScale;
	}

	void OnTriggerEnter(Collider c){
		if(c.tag != "fairy"){
			return;
		}
		if (c.gameObject.transform.parent != null) {
			if (c.transform.parent == transform.parent || c.transform.parent == gameObject.transform || c.gameObject == gameObject)
				return;
		} else {
			if (c.gameObject == gameObject || c.tag == "Terrian") {
				return;
			}
		}
		//Debug.Log (c.gameObject);
		//Debug.Log (ContentOfMail());
		GameObject.FindGameObjectWithTag ("Message").GetComponent<Text> ().text = ContentOfMail();
		NextAction (c.name);
	}

	public string ContentOfMail() {
		switch(gameObject.name){
		case "cat":
			return "I want a yarn";
		case "BigYarn":
			return "";
		}
		return "";
	}

	public void NextAction(string name){
		switch(gameObject.name.Split('_')[0]){
		case "cat":
			if (MissionTargetCount.GetMissionNum () == 1) {
				MissionTargetCount.updateTarget (1);
				GameObject.FindGameObjectWithTag ("Player").transform.GetChild (0).gameObject.SetActive (true);
			}
			break;
		case "BigYarn":
			if(MissionTargetCount.GetMissionNum() == 2){
				
				GameObject bigYarn = GameObject.Find ("BigYarn");
				GameObject player = GameObject.FindGameObjectWithTag ("Player");
				player.transform.GetChild (0).gameObject.SetActive (true);
				bigYarn.transform.position = player.transform.position + Vector3.right * 2.3f;
				bigYarn.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
				bigYarn.transform.parent = player.transform;
				MissionTargetCount.updateTarget(1);
			}
			break;
		case "Character":
			if(MissionTargetCount.GetMissionNum() == 0){
				GameObject.FindGameObjectWithTag ("Player").GetComponent<Movement> ().Enable ();
				MissionTargetCount.updateTarget(1);
			}
			break;
		case "Leave":
			/*if(gameObject.name == "Leave_m_Ans_B"){
				if(MissionTargetCount.GetMissionNum() == 0){
					GameObject.FindGameObjectWithTag ("Player").GetComponent<Movement> ().Enable ();
					MissionTargetCount.updateTarget(1);
				}
			}
			*/
			if (!isShaking) {
				isShaking = true;
				StartCoroutine (ShakeSelf (10, name));
				if (transform.childCount > 0) {
					foreach (Rigidbody temp in transform.GetComponentsInChildren<Rigidbody>()) {
						//if(temp.gameObject.tag == "missionItem"){
						//	Debug.Log ("Update Mission!");
						//	MissionTargetCount.updateTarget (1);
						//}
						temp.transform.parent = null;
						temp.isKinematic = false;
					}
				}
			}
			goto case "sendMail";
		case "sendMail":
			GameObject.FindGameObjectWithTag ("MailBoxContainer").GetComponent<MailBoxSystem>().createNewMail(gameObject.GetComponent<Renderer>().material.color);
			break;
		}
	}

	IEnumerator ShakeSelf(int shakeTime, string name){
		string[] temp = transform.name.Split ('_');
		if(name == "Fairy"){
			if(temp.Length > 3){
				if (temp [2] == "mark") {
					if (temp [3] == "A") {
						gameObject.GetComponent<Renderer> ().material.color = new Color (0.5f, 0, 0);
					} else if (temp [3] == "B") {
						gameObject.GetComponent<Renderer> ().material.color = new Color (0.5f, 0.7f, 0f);
					} else if (temp [3] == "C") {
						gameObject.GetComponent<Renderer> ().material.color = new Color (0, 0, 0.5f);
					}
				} else if(temp[2] == "Ans") {
					if (temp [3] == "A") {
						gameObject.GetComponent<Renderer> ().material.color = new Color (0.5f, 0, 0);
					} else if (temp [3] == "B") {
						gameObject.GetComponent<Renderer> ().material.color = new Color (0.5f, 0.7f, 0f);
					} else if (temp [3] == "C") {
						gameObject.GetComponent<Renderer> ().material.color = new Color (0, 0, 0.5f);
					}
				}
			}else{
				gameObject.GetComponent<Renderer> ().material.color = new Color (Random.Range(0.5f,0.95f),Random.Range(0.5f,0.95f),Random.Range(0.5f,0.95f));
			}
		}
		for(int i = 0 ; i < shakeTime ; i++){
			transform.localScale = origialScale * Random.Range (0.95f,1.05f);
			yield return new WaitForSeconds (0.03f);
		}
		transform.localScale = origialScale;
		isShaking = false;
	}
}
