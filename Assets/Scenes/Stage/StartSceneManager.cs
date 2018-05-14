using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			StartCoroutine(NextAction());
		}
	}

	IEnumerator NextAction(){
		StageCurtainSwitch.SwitchCurtain(false);
		GameObject temp = GameObject.Find("DL");
		Light tempL = temp.GetComponent<Light>();
		while(tempL.intensity < 0.5f){
			tempL.intensity += 0.05f;
			yield return new WaitForSeconds(0.02f);
		}
		yield return new WaitForSeconds(3f);
		SceneManager.NextScene();
	}
}
