using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour {

	TestAction[] actions;

	private void Start()
	{
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("actor");
		actions = new TestAction[gameObjects.Length];
		for (int i = 0; i < gameObjects.Length; i++){
			actions[i] = gameObjects[i].GetComponent<TestAction>();
		}
	}

	public void AllActorNextStep(){
		foreach(TestAction t in actions){
			t.NextAction();
		}
	}
}
