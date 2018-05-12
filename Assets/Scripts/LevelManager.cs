using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	static public int levelNum = 0;
	static public int[] levelWhichScript = {0};

	private void Start()
	{
		InitializeLevel(levelNum);
	}

	private static void InitializeLevel(int num){
		switch(num){
			case 0: //Hide and seek
				GameObject.Find("Manager").GetComponent<HideAndSeekEvent>().ActiveIt(GameObject.Find("girl"),6);
				break;
			case 1:
				GameObject.Find("Manager").GetComponent<HideAndSeekEvent>().ActiveIt(GameObject.Find("Book"), 1);
				break;
		}
	}

	public static void NextLevel(){
		levelNum++;
		InitializeLevel(levelNum);
	}

}
