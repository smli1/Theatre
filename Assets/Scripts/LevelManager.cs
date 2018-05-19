using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public static int levelNum = 0;
	public static int[] levelWhichScript = {0,0,0,0,1,1,2,2,2,2};
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
			case 0: //Hide and seek
				manager.GetComponent<HideAndSeekEvent>().ActiveIt(GameObject.Find("girl"), 6);
				break;
			case 1:
				manager.GetComponent<HideAndSeekEvent>().ActiveIt(GameObject.Find("Book"), 1);
				break;
			case 2:  
				
				break;
			case 3:
				StartCoroutine(WaitForAnimEnd(2f));
				break;
			case 4:
				
				manager.GetComponent<HideAndSeekEvent>().ActiveIt(GameObject.Find("Fairy_2").transform.GetChild(0).gameObject, 1);
				break;
			case 5:
				//Debug.Log("Wait for -1");
				GameObject.Find("Manager").GetComponent<ActionManager>().WaitForMinusOne("Fairy_2", true, false);
				break;
			
			case 6:
                StartCoroutine(WaitForAnimEnd(2f));
                break;
			case 7:
                GameObject.Find("Manager").GetComponent<ActionManager>().WaitForMinusOne("Fairy_5", false, true);
                break;
			case 8:
				List<GameObject> temp = new List<GameObject>();
                for (int i = 1; i <= 5; i++)
                {
                    temp.Add(GameObject.Find("Mail_" + i));
                }
                manager.GetComponent<HideAndSeekEvent>().ActiveIt(temp, temp.Count);
                break;
			case 9:
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
				GameObject.Find("Manager").GetComponent<LevelManager>().InitializeLevel(3);
				levelNum = 4;
				break;
			case 1:
				GameObject.Find("Manager").GetComponent<LevelManager>().InitializeLevel(6);
                levelNum = 7;
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
