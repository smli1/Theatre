using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndSeekEvent : MonoBehaviour {


	public GameObject target;
	private int stepCount = 0;
	private int stepNum;
	private bool isActive;

	public void Start()
	{
		isActive = false;
	}

	private void Update()
	{
		if (target)
		{
			if (Input.GetMouseButtonDown(0) && isActive && !ScriptManager.isScripting)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit, 1000f))
				{
					TriggerAnim(raycastHit.collider.gameObject);
               
					if (target == raycastHit.collider.gameObject)
					{    
						if(target.tag == "actor"){
							if(!target.GetComponent<TestAction>().GetReady()){
								return;
							}
						}
						stepCount++;
						//print("" + stepCount);
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
	}

	public IEnumerator DelayActive(float sec){
		yield return new WaitForSeconds(sec);
		isActive = true;
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
