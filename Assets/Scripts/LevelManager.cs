using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	// leveNum [0]:0 [1]:3 [2]:7 [3]:10
	public static int levelNum = 0;
	//public static int[] levelWhichScript = {0,0,0,0,1,1,2,2,2,2};
	public static int[] levelWhichScript = {0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3 };
	private static GameObject manager;

	private void Start()
	{
		//manager = GameObject.Find("Manager");
		//InitializeLevel(levelNum);
		//Reset();
	}

	public void Reset()
	{
		manager = GameObject.Find("Manager");
        InitializeLevel(levelNum);
		//Debug.Log(""+levelNum);
	}

	public void InitializeLevel(int num){
		Debug.Log("Now Level:" + num);
		switch(num){
			case 0:
				manager.GetComponent<ActionManager>().WaitForMinusOne("Rose", true, true, false);
				break;
			case 1:
				//Catch Rose
				manager.GetComponent<HideAndSeekEvent>().ActiveIt(GameObject.Find("Rose"), 1);
				break;
			case 2:
				//Wait for Rose end script
				break;
			case 3:
                StartCoroutine(WaitForAnimEnd(2f));
                break;
			case 4: //Hide and seek
				manager.GetComponent<HideAndSeekEvent>().ActiveIt(GameObject.Find("girl"), 6);
				break;
			case 5:
				manager.GetComponent<HideAndSeekEvent>().ActiveIt(GameObject.Find("Book"), 1);
				break;
			case 6:  
				
				break;
			case 7:
				StartCoroutine(WaitForAnimEnd(2f));
				break;
			case 8:
				manager.GetComponent<ActionManager>().WaitForMinusOne("Fairy_2", true, true);
				break;
			case 9:
				
				manager.GetComponent<HideAndSeekEvent>().ActiveIt(GameObject.Find("Fairy_2").transform.GetChild(0).gameObject, 1);
				break;
			case 10:
				//Debug.Log("Wait for -1");
				manager.GetComponent<ActionManager>().WaitForMinusOne("Fairy_2", true, false);
				break;
			
			case 11:
                StartCoroutine(WaitForAnimEnd(2f));
                break;
			case 12:
				manager.GetComponent<ActionManager>().WaitForMinusOne("Fairy_5", false, true);
                break;
			case 13:
				List<GameObject> temp = new List<GameObject>();
                for (int i = 1; i <= 5; i++)
                {
                    temp.Add(GameObject.Find("Mail_" + i));
                }
                manager.GetComponent<HideAndSeekEvent>().ActiveIt(temp, temp.Count);
                break;
			case 14:
				manager.GetComponent<ActionManager>().WaitForEndNum(5);
				break;
			
		}
	}

	public static void NextLevel(){
		levelNum++;
		GameObject.Find("Manager").GetComponent<LevelManager>().InitializeLevel(levelNum);
	}

	public static void EndScene()
    {
		Debug.Log("EndScene / Level num :"+levelNum);
		switch(levelWhichScript[levelNum]){
			case 0:
				manager.GetComponent<LevelManager>().InitializeLevel(3);
                levelNum = 4;
                break;
			case 1:
				manager.GetComponent<LevelManager>().InitializeLevel(7);
				levelNum = 8;
				break;
			case 2:
				manager.GetComponent<LevelManager>().InitializeLevel(11);
                levelNum = 12;
				break;
			case 3:
				StageCurtainSwitch.SwitchCurtain(false);
				break;
		}
    }

	IEnumerator WaitForAnimEnd(float sec){
		yield return new WaitForSeconds(sec);
		StageCurtainSwitch.SwitchCurtain(false);
		yield return new WaitForSeconds(sec);
		//Debug.Log(manager);
		manager.GetComponent<GameSceneManager>().NextScene();
	}
   
}
