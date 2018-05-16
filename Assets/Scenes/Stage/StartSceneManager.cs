using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour {
	bool isClicked = false;

	void Update () {
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0 && !isClicked)
		{
			if (Input.GetMouseButtonDown(0))
			{
				isClicked = true;
				StartCoroutine(NextAction());
			}
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
		GameObject.Find("Manager").GetComponent<GameSceneManager>().NextScene();
	}
}
