using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSelect : MonoBehaviour {
	int optionNum = 0;
	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag == "fairy"){
			switch(optionNum){
			case 0:
				GameObject.FindGameObjectWithTag ("Player").GetComponent<Movement> ().MoveBackward ();
				break;
			case 1:
				GameObject.FindGameObjectWithTag ("Player").GetComponent<Movement> ().MoveForward ();
				break;

			}
			optionNum++;
			Debug.Log ("optionNum : "+ optionNum);
			gameObject.SetActive (false);
		}
	}

	public int getOptionNum(){
		return optionNum;
	}
}
