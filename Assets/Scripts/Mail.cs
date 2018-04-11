using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mail : MonoBehaviour {

	public MailData data;
	public bool isLocked;

	public void intialMail(string name){
		data = new MailData (name);
		GetComponent<Image> ().sprite = data.getSprite ();
		isLocked = false;
	}
}
