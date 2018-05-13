using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public static int levelNum = 0;
	public static int[] levelWhichScript = {0};
	private GameObject manager;
	private void Start()
	{
		manager = GameObject.Find("Manager");
		InitializeLevel(levelNum);
	}

	public void InitializeLevel(int num){
		switch(num){
			case 0: //Hide and seek
				manager.GetComponent<HideAndSeekEvent>().ActiveIt(GameObject.Find("girl"),6);
				break;
			case 1:
				manager.GetComponent<HideAndSeekEvent>().ActiveIt(GameObject.Find("Book"), 1);
				break;
			case 2:
				//StageCurtainSwitch.SwitchCurtain(false);

				StartCoroutine(WaitForAnimEnd(2f));
				break;
		}
	}

	public static void NextLevel(){
		levelNum++;
		GameObject.Find("Manager").GetComponent<LevelManager>().InitializeLevel(levelNum);
	}

	IEnumerator WaitForAnimEnd(float sec){
		yield return new WaitForSeconds(sec);
		StageCurtainSwitch.SwitchCurtain(false);
		manager.GetComponent<ScriptManager>().Reset();
        GameObject.Find("MouseSelector").GetComponent<MouseSelector>().Reset();
        Camera.main.GetComponent<CameraZoom>().Reset();
	}
   
}
