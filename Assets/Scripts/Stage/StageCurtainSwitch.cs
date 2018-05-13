using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCurtainSwitch : MonoBehaviour {

    static Animator animator;
    static bool isOpened = false;

	void Start () {
        animator = GetComponent<Animator>();
        animator.Play(isOpened ? "CloseCurtain" : "OpenCurtain");
        isOpened = !isOpened;
	}

	public static void SwitchCurtain(bool isOC){
		if (!isOC == isOpened )
		{
			animator.Play(!isOC ? "CloseCurtain" : "OpenCurtain");
			isOpened = !isOC;
		}
	}

    /*
	IEnumerator CurtainProgress(bool isOpen){
        isOpened = isOpen;
        StopCoroutine("TurnCurtainOpenClose");
        StopCoroutine("TurnCurtainOpenClose");
        StartCoroutine(TurnCurtainOpenClose(isOpen, true));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(TurnCurtainOpenClose(isOpen, false));
    }

    IEnumerator TurnCurtainOpenClose(bool isOpen, bool isLeft)
    {
        if (isLeft)
        {
            if (isOpen)
            {
                while (curL.transform.position.x >= posL.x - 16)
                {

                    curL.transform.position -= new Vector3(0.1f, 0, 0);
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else
            {
                while (curL.transform.position.x <= posL.x)
                {

                    curL.transform.position += new Vector3(0.1f, 0, 0);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }
        else
        {
            if (isOpen)
            {
                while (curR.transform.position.x <= posR.x + 16)
                {
                    curR.transform.position += new Vector3(0.1f, 0, 0);
                    yield return new WaitForSeconds(0.01f);
                }
            }else{
                while (curR.transform.position.x <= posR.x)
                {
                    curR.transform.position -= new Vector3(0.1f, 0, 0);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }
    }

    void getStageCurtain(){
        string n = transform.GetChild(0).name;
        if(n[n.Length-1].CompareTo('L') == 0)
        {
            curL = transform.GetChild(0).gameObject;
            curR = transform.GetChild(1).gameObject;
        }
        else
        {
            curL = transform.GetChild(1).gameObject;
            curR = transform.GetChild(0).gameObject;
        }
    }
    */
}
