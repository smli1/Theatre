using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	static int levelNum = 0;

	private void Start()
	{
		InitializeLevel(levelNum++);
	}

	private static void InitializeLevel(int num){
		switch(num){
			case 0: //Hide and seek
				GameObject.Find("Manager").GetComponent<HideAndSeekEvent>().ActiveIt(GameObject.Find("Girl"),7);
				break;
			case 1:
				GameObject.Find("Manager").GetComponent<HideAndSeekEvent>().ActiveIt(GameObject.Find("Book"), 1);
				break;
		}
	}

	public static void NextLevel(){
		InitializeLevel(levelNum++);
	}

}
