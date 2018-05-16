using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndSeekEvent : MonoBehaviour {


	public GameObject target;
	private int stepCount;
	private int stepNum;
	private static bool isActive;

	public void Start()
	{
		Reset();
	}

	public void Reset()
	{
		isActive = false;
		stepCount = 0;
	}

	private void Update()
	{
		if (target)
		{
			if (MouseSelector.GetSelected() && isActive && !ScriptManager.isScripting)
			{
				Debug.Log("Catched! -> "+target);
				//if(target.tag == "actor"){
					//if(!target.GetComponent<TestAction>().GetReady()){
					//	return;
					//}
				//}
				stepCount++;

				if (stepCount < stepNum)
				{               
                    isActive = false;
					ExecuteEvent();
                    StartCoroutine(DelayActive(0.5f));
				}

				if(stepCount == stepNum){  
					ExecuteEvent();
                    stepCount = 0;
					isActive = false;
					target = null;
					StopCoroutine("delayActive");
                    LevelManager.NextLevel();            
				}
			}
		}
		Debug.Log(stepCount+"/"+stepNum);
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
		//Debug.Log("Active");
	}

	public IEnumerator DelayActive(float sec){
		yield return new WaitForSeconds(sec);
		isActive = true;
		MouseSelector.ActiveSelector(target);
	}

	void ExecuteEvent(){
		
		if(target.name == "Book" && LevelManager.levelWhichScript[LevelManager.levelNum] == 0){
			GameObject.Find("Manager").GetComponent<ActionManager>().AllActorNextStep();
			//print("Hit!");
		}
		if (target.tag == "actor" && LevelManager.levelWhichScript[LevelManager.levelNum] == 0)
		{
			if (target.GetComponent<TestAction>().isInOuting)
            {
                return;
            }
			//Debug.Log("Next");
			GameObject.Find("Manager").GetComponent<ActionManager>().AllActorNextStep();
		}
		if(target.tag == "actor" && LevelManager.levelWhichScript[LevelManager.levelNum] == 1){
			//GameObject.Find("Manager").GetComponent<ActionManager>().WaitForMinusOne();
		}
	}
    
}
