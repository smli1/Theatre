using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour {

	void OnTriggerEnter(Collider c){
		//if(gameObject.name == "Branch") Debug.Log (c.name);
		if(c.gameObject.transform.parent != null){
		if (c.transform.parent == transform.parent || c.transform.parent == gameObject.transform || c.gameObject != gameObject)
			return;
		}
		Debug.Log (ContentOfMail());
		GameObject.FindGameObjectWithTag ("Message").GetComponent<Text> ().text = ContentOfMail();
		NextAction ();
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

	public void NextAction(){
		switch(gameObject.name){
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
				bigYarn.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
				bigYarn.transform.parent = player.transform;
				MissionTargetCount.updateTarget(1);
			}
			break;
		case "Branch":
			if(MissionTargetCount.GetMissionNum() == 0){
				GameObject.FindGameObjectWithTag ("Player").GetComponent<Movement> ().Enable ();
				MissionTargetCount.updateTarget(1);
			}
			break;
		}
	}
}
