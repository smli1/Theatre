using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour,IMailable {

	void OnTriggerEnter(Collider c){
		Debug.Log (ContentOfMail());
	}

	public string ContentOfMail() {
		switch(gameObject.name){
		case "cat":
			return "I want a yarn";
		}
		return "";
	}
}
