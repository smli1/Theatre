using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

	// 0 : start scene
	// 1 : tutorial
	// 2 : Grandma & Girl
	// 3 : Catch fairy 
	// 4 : Mails seeking
	public static int sceneNum = 0;

	private GameObject manager;
	public void NextScene(){
		sceneNum++;
		//ActionManager.DeteleAllActors();
        manager = GameObject.Find("Manager");
		if(manager.GetComponent<StartSceneManager>().enabled){
			manager.GetComponent<StartSceneManager>().enabled = false;
		}
		StartCoroutine(LoadAsyncScene());

	}

	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.N)){
			NextScene();
		}
	}

	public static bool IsLastSceneUnloaded()
	{
		Debug.Log("isLoad : "+!SceneManager.GetSceneByBuildIndex(sceneNum-1).isLoaded);
		return !SceneManager.GetSceneByBuildIndex(sceneNum -1).isLoaded;
	}

	IEnumerator LoadAsyncScene()
    {
		
        Scene currentScene = SceneManager.GetActiveScene();


		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNum, LoadSceneMode.Additive);


        while (!asyncLoad.isDone)
        {
            yield return null;
        }

		SceneManager.MoveGameObjectToScene(manager, SceneManager.GetSceneByBuildIndex(sceneNum));

        SceneManager.UnloadSceneAsync(currentScene);
    }
    
}
