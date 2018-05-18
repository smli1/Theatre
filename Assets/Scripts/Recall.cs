using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recall : MonoBehaviour {
	GameObject manager;
	// Use this for initialization
	void Start () {
		
		manager = GameObject.Find("Manager");
		StartCoroutine(WaitForLastSceneUnload());

	}

	IEnumerator WaitForLastSceneUnload(){
		while (!GameSceneManager.IsLastSceneUnloaded())
		{
			yield return new WaitForSeconds(0.5f);

		}
		if (manager)
        {      
            if (manager.GetComponent<ScriptManager>())
            {
                manager.GetComponent<ScriptManager>().Reset();
            }

            if (GameObject.Find("MouseSelector"))
            {
                GameObject.Find("MouseSelector").GetComponent<MouseSelector>().Reset();
            }

            if (Camera.main.GetComponent<CameraZoom>())
            {
                Camera.main.GetComponent<CameraZoom>().Reset();
            }

            if(manager.GetComponent<HideAndSeekEvent>())
            {
                manager.GetComponent<HideAndSeekEvent>().Reset();
            }

            if (manager.GetComponent<ReadJson>())
            {
                manager.GetComponent<ReadJson>().Reset();
            }
            if (manager.GetComponent<ActionManager>())
            {
                manager.GetComponent<ActionManager>().Reset();
            }
            if (manager.GetComponent<LevelManager>())
            {
                manager.GetComponent<LevelManager>().Reset();

            }
			Debug.Log("Recall!");
        }      
	}

}
