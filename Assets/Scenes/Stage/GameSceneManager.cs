using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

	public static int sceneNum = 0;

	private GameObject manager;
	public void NextScene(){
		sceneNum++;      
        manager = GameObject.Find("Manager");
		StartCoroutine(LoadAsyncScene());

	}

	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.N)){
			NextScene();
		}
	}



	IEnumerator LoadAsyncScene()
    {
		
        Scene currentScene = SceneManager.GetActiveScene();


		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("scene_" + sceneNum, LoadSceneMode.Additive);


        while (!asyncLoad.isDone)
        {
            yield return null;
        }

		SceneManager.MoveGameObjectToScene(manager, SceneManager.GetSceneByName("scene_" + sceneNum));

        SceneManager.UnloadSceneAsync(currentScene);
    }
    
}
