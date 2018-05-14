using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneManager {

	public static int sceneNum = 0;

	public static void NextScene(){
		sceneNum++;
		UnityEngine.SceneManagement.SceneManager.LoadScene("scene_"+sceneNum);
	}

}
