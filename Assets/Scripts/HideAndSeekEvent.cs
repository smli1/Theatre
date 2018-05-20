using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndSeekEvent : MonoBehaviour {


	public GameObject target;
	private int stepCount;
	private int stepNum;
	private static bool isActive;
	public List<GameObject> targets;

	public void Start()
	{
		//Reset();
	}

	public void Reset()
	{
		target = null;
		isActive = false;
		stepCount = 0;
	}

	private void Update()
	{
		if (isActive)
		{
			GameObject temp = MouseSelector.GetSelected();
			if (temp)
			{
				Debug.Log("temp:"+temp.name);


				if(targets.Contains(temp)){
					targets.Remove(temp);
				}

				stepCount++;

				if (stepCount < stepNum)
				{
					if (targets.Count == 0)
					{
						isActive = false;

					}
					target = temp;
					Debug.Log("Catched! -> " + temp);
					ExecuteEvent();
                    StartCoroutine(DelayActive(0.5f));
				}

				if(stepCount == stepNum){  
					
                    
					if (targets.Count == 0)
                    {
                        isActive = false;
					}
					target = temp;
					ExecuteEvent();
					//target = null;
					stepCount = 0;
					StopCoroutine("delayActive");
                    LevelManager.NextLevel();            
				}
			}
		}
		//Debug.Log(stepCount+"/"+stepNum);
	}

	public void TriggerAnim(GameObject gameObject){
		Animator animator = gameObject.GetComponent<Animator>();
        if (animator)
        {
			if(animator){
				animator.Play(gameObject.name + "Anim");
			}
        }
	}

	public void ActiveIt(GameObject target, int stepNum){
		this.stepNum = stepNum;
		stepCount = 0;
		this.target = target;
		isActive = true;
		MouseSelector.ActiveSelector(target);
		targets = new List<GameObject>();
		Debug.Log("Active");
	}

	public void ActiveIt(List<GameObject> targets, int stepNum)
    {
        this.stepNum = stepNum;
        stepCount = 0;
        this.targets = targets;
        isActive = true;
		MouseSelector.ActiveSelector(targets);
        //Debug.Log("Active");
    }

	public IEnumerator DelayActive(float sec){
		yield return new WaitForSeconds(sec);
		isActive = true;
		MouseSelector.ActiveSelector(target);
	}

	void ExecuteEvent(){
		if (target)
		{
			if (target.name == "Book" && LevelManager.levelWhichScript[LevelManager.levelNum] == 1)
			{
				GameObject.Find("Manager").GetComponent<ActionManager>().AllActorNextStep();
				//print("Hit!");
			}
			if (target.tag == "actor" && LevelManager.levelWhichScript[LevelManager.levelNum] == 1)
			{
				Debug.Log("All actor Next step");
				GameObject.Find("Manager").GetComponent<ActionManager>().AllActorNextStep();
			}
			if (target.tag == "actor" && LevelManager.levelWhichScript[LevelManager.levelNum] == 0)
            {
                GameObject.Find("Manager").GetComponent<ActionManager>().AllActorNextStep();
            }

			if (target.tag == "actor" && LevelManager.levelWhichScript[LevelManager.levelNum] == 3)
			{
				//target.GetComponent<TestAction>().NextAction();
				if (target.name.Split('_')[0] == "Mail")
				{
					Debug.Log("Minigame start!");
					GameObject.Find("Grid").GetComponent<GridManager>().activeIt(target);
				}
			}
		}
	}
    
}
