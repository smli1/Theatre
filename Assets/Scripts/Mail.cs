using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mail : MonoBehaviour {

	public MailData data;
	public bool isLocked;
	GameObject sender;

	public void intialMail(GameObject obj, bool isLocked = false){
		sender = obj;
		data = new MailData (obj.name);
		GetComponent<Image> ().sprite = data.getSprite ();
		this.isLocked = isLocked;
		if(this.isLocked){
			transform.GetChild(0).gameObject.SetActive (!transform.GetChild(0).gameObject.activeSelf);
		}
	}

	public void openMail(){
		GetComponent<Image> ().sprite = data.getSpriteOpened ();
	}

	public void sendToTarget(GameObject target){
		string caseName = target.name.Split('_')[0];
		//Debug.Log (target.name);
		switch(caseName){
		case "Character":
			Debug.Log ("R:Character");
			//Action
			caseName = sender.name.Split('_')[0];
			switch(caseName){
			case "Leave":
				Debug.Log ("S:Leave");
				break;
			}
			break;	

		}
	}
}
