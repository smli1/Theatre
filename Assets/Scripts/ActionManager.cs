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
		ImportDataToActors();
	}

	public void AllActorNextStep(){
		foreach(TestAction t in actions){
			t.NextAction();
		}
	}
    

	private void ImportDataToActors(){
	    List<Script> scripts = ReadJson.data.script;
		for (int i = 0; i < scripts.Count; i++){
			if(LevelManager.levelNum == i){
				List<Actor> actors = scripts[i].actors;
				for (int j = 0; j < actors.Count; j++){
					foreach(TestAction ta in actions){
						if(ta.gameObject.name == actors[j].name){
							ta.clipNames = actors[j].actor_clip_name.ToArray();
							ta.actionNumArray = actors[j].actor_steps_action.ToArray();
							ta.eachActionDelayArray = actors[j].actor_steps_delay.ToArray();
							ta.isFixedPos = actors[j].isFixedPosition;
							ta.offsetArray = new Vector3[actors[j].actor_total_steps];
							ta.offsetMarkerArray = new Transform[actors[j].actor_total_steps];
							for (int k = 0; k < ta.offsetArray.Length; k++){
								Vector3 temp = new Vector3((float)actors[j].actor_step_offset[k].x, (float)actors[j].actor_step_offset[k].y, (float)actors[j].actor_step_offset[k].z);
								ta.offsetArray[k] = temp;
								ta.offsetMarkerArray[k] = GameObject.Find(actors[j].actor_markers_name + actors[j].actor_markers_num[k]).transform;
							}
							break;
						}
					}
				}
				return;
			}
		}
	}

}
