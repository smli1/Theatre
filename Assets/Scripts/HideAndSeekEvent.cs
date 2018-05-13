using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndSeekEvent : MonoBehaviour {


	public GameObject target;
	private int stepCount = 0;
	private int stepNum;
	private static bool isActive;

	public void Start()
	{
		isActive = false;
	}

	private void Update()
	{
		if (target)
		{
			if (MouseSelector.getSelected() && isActive && !ScriptManager.isScripting)
			{
				Debug.Log("Catched!");
				//if(target.tag == "actor"){
					//if(!target.GetComponent<TestAction>().GetReady()){
					//	return;
					//}
				//}
				stepCount++;

				if (stepCount <= stepNum)
				{
					ExecuteEvent();
                    isActive = false;
                    StartCoroutine(DelayActive(0.5f));
				}

				if(stepCount == stepNum){                     
                    stepCount = 0;
					isActive = false;
					target = null;
					StopCoroutine("delayActive");
                    LevelManager.NextLevel();            
				}
			}
		}
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
	}

	public IEnumerator DelayActive(float sec){
		yield return new WaitForSeconds(sec);
		isActive = true;
		MouseSelector.ActiveSelector(target);
	}

	void ExecuteEvent(){
		
		if(target.name == "Book"){
			GameObject.Find("Manager").GetComponent<ActionManager>().AllActorNextStep();
			print("Hit!");
		}
		if (target.tag == "actor")
		{
			if (target.GetComponent<TestAction>().isInOuting)
            {
                return;
            }
			GameObject.Find("Manager").GetComponent<ActionManager>().AllActorNextStep();
		}
	}
    
}
