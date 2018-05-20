using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCurtainSwitch : MonoBehaviour {

    static Animator animator;
	//static bool isOpened = false;
	public bool isAwake = true;
	void Start () {
        animator = GetComponent<Animator>();
		if (isAwake)
		{
			StageCurtainSwitch.SwitchCurtain(true);
		}
	}

	public static void SwitchCurtain(bool isOC){
		//Debug.Log("isOC: "+isOC);
			//Debug.Log("In");
			animator.Play(isOC ? "OpenCurtain" : "CloseCurtain");
			//isOpened = isOC;
	}

}
