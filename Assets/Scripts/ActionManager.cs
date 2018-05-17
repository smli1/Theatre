using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour {

	static List<TestAction> actions;
	static List<TestAction> endedActions;
	private static int endedActorNum = 0;
	static int[] sceneActorNum = {0,3,4,6};

	private void Start()
	{
		//Reset();
	}

	public void Reset()
	{
		endedActions = new List<TestAction>();
		endedActorNum = 0;
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("actor");
		actions = new List<TestAction>();
		actions.Clear();
        for (int i = 0; i < gameObjects.Length; i++)
        {
			if (!actions.Contains(gameObjects[i].GetComponent<TestAction>()))
			{
				actions.Add(gameObjects[i].GetComponent<TestAction>());
			}
			//actions.Add(gameObjects[i].GetComponent<TestAction>());
            
        }
        ImportDataToActors();
        for (int i = 0; i < actions.Count; i++)
        {
            actions[i].Initialize();
			//Debug.Log(i + " " + actions[i].name);
			Debug.Log("Actors ["+i+"] : "+actions[i].name);
        }
		Debug.Log("Reseted : "+actions.Count);
	}
    /*
	public static void DeteleAllActors(){
		foreach(TestAction t in actions){
			Destroy(t.gameObject);
		}
		actions.Clear();
	}*/

	public static void FinishedAllAction(TestAction testAction){
		//Debug.Log("FinisedAllAction : "+testAction.gameObject.name);
		if (actions.Contains(testAction))
		{
			if(endedActions.Contains(testAction)){
				return;
			}
			endedActions.Add(testAction);
			endedActorNum++;
			Debug.Log("FinisedAllAction : " + testAction.gameObject.name+"("+endedActorNum+"/"+actions.Count+")");
			//if (endedActorNum == sceneActorNum[GameSceneManager.sceneNum])
			if (endedActorNum == actions.Count)
			{
				endedActorNum = 0;
				actions.Clear();
				LevelManager.EndScene();
				Debug.Log("EndScene");
			}
			//actions.Remove(testAction);
		}
	}

	public void WaitForMinusOne(string actorName, bool isAllActorNext, bool isNextLevel){
		StartCoroutine(WaitFor(actorName, isAllActorNext, isNextLevel));
	}

	IEnumerator WaitFor(string n, bool isAllActorNext, bool isNextLevel)
	{
		
		bool isContinue = true;
		while (isContinue)
		{
			for (int i = 0; i < actions.Count; i++)
			{
				if (actions[i])
				{
					//Debug.Log("Wait for "+actions[i].gameObject);
					if (actions[i].gameObject.name == n)
					{
						Debug.Log("gameobject name : " + n);
						//Debug.Log("NAN:" + (actions[i].nextActionNum) + " / ");
						//Debug.Log(actions[i].eachActionDelayArray[actions[i].nextActionNum ]);
						int temp = actions[i].nextActionNum - 1;
						if (temp>=0)
						{
							if (actions[i].eachActionDelayArray[temp] <= -1)
							{
								if (isNextLevel)
								{
									LevelManager.NextLevel();
								}
								if (isAllActorNext)
								{
									AllActorNextStep();
								}
								else
								{
									actions[i].NextAction();
								}
								isContinue = false;
								break;
							}
						}else{
							Debug.LogError("index = -1");
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
							ta.isNeedFadeOut = actors[j].after_actions_isFadeOut;
							ta.isNeedFadeIn = actors[j].after_actions_isFadeIn;
							ta.isSpinable = actors[j].actor_spinable;
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
