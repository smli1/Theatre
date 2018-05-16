using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour {

	static TestAction[] actions;
	private static int endedActorNum = 0;
	private void Start()
	{
		Reset();
	}

	public void Reset()
	{
		endedActorNum = 0;
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("actor");
        actions = new TestAction[gameObjects.Length];
        for (int i = 0; i < gameObjects.Length; i++)
        {
            actions[i] = gameObjects[i].GetComponent<TestAction>();
            //Debug.Log( actions[i].name);
        }
        ImportDataToActors();
        for (int i = 0; i < gameObjects.Length; i++)
        {
            actions[i].Initialize();

        }
	}

	public static void FinishedAllAction(){
		endedActorNum++;
		if(endedActorNum == actions.Length){
			LevelManager.EndScene();
			Debug.Log("EndScene");
		}
	}

	public void WaitForMinusOne(string actorName){
		StartCoroutine(WaitFor(actorName));
	}

	IEnumerator WaitFor(string n)
	{
		Debug.Log("Num of action : "+actions.Length);
		bool isContinue = true;
		while (isContinue)
		{
			for (int i = 0; i < actions.Length; i++)
			{
				if (actions[i])
				{
					Debug.Log("Wait for "+actions[i].gameObject);
					if (actions[i].gameObject.name == n)
					{
						//Debug.Log("NAN:" + (actions[i].nextActionNum) + " / ");
						//Debug.Log(actions[i].eachActionDelayArray[actions[i].nextActionNum - 1]);
						if (actions[i].eachActionDelayArray[actions[i].nextActionNum - 1] <= -1)
						{
							AllActorNextStep();
							isContinue = false;
							break;
						}
					}
				}
			}
			yield return new WaitForSeconds(0.5f);
		}

	}

	public void AllActorNextStep(){
		foreach(TestAction t in actions){
			t.NextAction();
		}      
	}
    

	private void ImportDataToActors(){
	    List<Script> scripts = ReadJson.data.script;
		for (int i = 0; i < scripts.Count; i++){
			if(LevelManager.levelWhichScript[LevelManager.levelNum] == i){
				//Debug.Log(i);
				List<Actor> actors = scripts[i].actors;
				for (int j = 0; j < actors.Count; j++){
					foreach(TestAction ta in actions){
						if(ta.gameObject.name == actors[j].name){
							//Debug.Log(actors[j].name+" "+j);
							ta.clipNames = actors[j].actor_clip_name.ToArray();
							//Debug.Log(ta.clipNames[0]);
							ta.actionNumArray = actors[j].actor_steps_action.ToArray();
							ta.eachActionDelayArray = actors[j].actor_steps_delay.ToArray();
							ta.isFixedPos = actors[j].isFixedPosition;
							ta.startActionNum = actors[j].start_action_step;
							ta.offsetMarkerArray = new Transform[actors[j].actor_total_steps];
							for (int k = 0; k < ta.offsetMarkerArray.Length; k++)
                            {
                                ta.offsetMarkerArray[k] = GameObject.Find(actors[j].actor_markers_name + actors[j].actor_markers_num[k]).transform;
                            }
							if (!ta.isFixedPos)
							{
								ta.offsetArray = new Vector3[actors[j].actor_total_steps];
								for (int k = 0; k < ta.offsetArray.Length; k++)
                                {
                                    Vector3 temp = new Vector3((float)actors[j].actor_step_offset[k].x, (float)actors[j].actor_step_offset[k].y, (float)actors[j].actor_step_offset[k].z);
                                    ta.offsetArray[k] = temp;
                                }

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
