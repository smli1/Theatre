using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	// leveNum [1]:2 [2]:6 [3]:9
	public static int levelNum = 0;
	//public static int[] levelWhichScript = {0,0,0,0,1,1,2,2,2,2};
	public static int[] levelWhichScript = {0, 0, 1, 1, 1, 1, 2, 2, 3, 3, 3, 3 };
	private GameObject manager;

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
				//Catch Rose
				break;
			case 1:
				//Wait for Rose end script
				break;
			case 2: //Hide and seek
				manager.GetComponent<HideAndSeekEvent>().ActiveIt(GameObject.Find("girl"), 6);
				break;
			case 3:
				manager.GetComponent<HideAndSeekEvent>().ActiveIt(GameObject.Find("Book"), 1);
				break;
			case 4:  
				
				break;
			case 5:
				StartCoroutine(WaitForAnimEnd(2f));
				break;
			case 6:
				
				manager.GetComponent<HideAndSeekEvent>().ActiveIt(GameObject.Find("Fairy_2").transform.GetChild(0).gameObject, 1);
				break;
			case 7:
				//Debug.Log("Wait for -1");
				GameObject.Find("Manager").GetComponent<ActionManager>().WaitForMinusOne("Fairy_2", true, false);
				break;
			
			case 8:
                StartCoroutine(WaitForAnimEnd(2f));
                break;
			case 9:
                GameObject.Find("Manager").GetComponent<ActionManager>().WaitForMinusOne("Fairy_5", false, true);
                break;
			case 10:
				List<GameObject> temp = new List<GameObject>();
                for (int i = 1; i <= 5; i++)
                {
                    temp.Add(GameObject.Find("Mail_" + i));
                }
                manager.GetComponent<HideAndSeekEvent>().ActiveIt(temp, temp.Count);
                break;
			case 11:
				GameObject.Find("Manager").GetComponent<ActionManager>().WaitForEndNum(5);
				break;
			
		}
	}

	public static void NextLevel(){
		levelNum++;
		GameObject.Find("Manager").GetComponent<LevelManager>().InitializeLevel(levelNum);
	}

	public static void EndScene()
    {
		switch(levelWhichScript[levelNum]){
			case 0:
				GameObject.Find("Manager").GetComponent<LevelManager>().InitializeLevel(5);
				levelNum = 6;
				break;
			case 1:
				GameObject.Find("Manager").GetComponent<LevelManager>().InitializeLevel(8);
                levelNum = 9;
				break;
			case 2:
				StageCurtainSwitch.SwitchCurtain(false);
				break;
		}
    }

	IEnumerator WaitForAnimEnd(float sec){
		yield return new WaitForSeconds(sec);
		StageCurtainSwitch.SwitchCurtain(false);
		yield return new WaitForSeconds(sec);
		GameObject.Find("Manager").GetComponent<GameSceneManager>().NextScene();
	}
   
}
