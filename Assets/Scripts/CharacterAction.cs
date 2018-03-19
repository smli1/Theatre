using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : MonoBehaviour {


	int statusNum = 0;
	GameObject catchObject;

	void Start(){
		catchObject = GameObject.Find ("BigYarn");
	}

	void Update(){
		switch(statusNum){
		case 1:
			//transform.position += (catchObject.transform.position + Vector3.right - transform.position).normalized * Time.deltaTime * 5;
			break;
		}
	}

	public void switchStatus(int num){
		statusNum = num;
	}
}
