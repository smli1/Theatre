using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCurtainSwitch : MonoBehaviour {

    static Animator animator;
	static bool isOpened = false;

	void Start () {
        animator = GetComponent<Animator>();
		StageCurtainSwitch.SwitchCurtain(true);
	}

	public static void SwitchCurtain(bool isOC){
		//Debug.Log("isOC: "+isOC);
		if (!isOC == isOpened )
		{
			//Debug.Log("In");
			animator.Play(isOC ? "OpenCurtain" : "CloseCurtain");
			isOpened = isOC;
		}
	}

}
