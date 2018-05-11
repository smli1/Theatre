using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndSeekEvent : MonoBehaviour {


	private GameObject target;
	private int stepCount = 0;
	private int stepNum;
	private bool isActive;

	public void Start()
	{
		isActive = false;
	}

	private void Update()
	{
		if (isActive && target)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit, 1000f))
				{
					
					Debug.Log(raycastHit.collider.gameObject);

					triggerAnim(raycastHit.collider.gameObject);
               
					if (target == raycastHit.collider.gameObject)
					{
						executeEvent();
						stepCount++;
						if (stepCount > stepNum)
						{
							StopCoroutine("delayActive");
							isActive = false;
							target = null;
						}
						else{
							isActive = false;
							StartCoroutine(delayActive(0.5f));
						}
					}
				}
			}
		}
	}

	public void triggerAnim(GameObject gameObject){
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

	public IEnumerator delayActive(float sec){
		yield return new WaitForSeconds(sec);
		isActive = true;
	}

	void executeEvent(){
		
       
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
