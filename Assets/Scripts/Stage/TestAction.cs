using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : MonoBehaviour {
    Animator animator;
    [SerializeField]
    string[] clipNames;
    bool actionFinished = false;
    int nextActionNum = 0;
    int[] ActionNumArray = {1,2,1,2,1,2};

	void Start () {
        animator = GetComponent<Animator>();
        StartCoroutine(SetInOut(true));
        //StartCoroutine(changeAction(clipNames[2], new Vector3(1,0,0), 0.2f));

	}

	private void Update()
	{
        if(actionFinished){
            actionFinished = false;
            if (nextActionNum != -1)
            {
                StartCoroutine(ChangeAction(clipNames[ActionNumArray[nextActionNum]], new Vector3(1, 0, 0), 0.2f));
                if(nextActionNum >= ActionNumArray.Length-1){
                    nextActionNum = -1;
                }else{
                    nextActionNum++;
                }
            }else{
                StartCoroutine(SetInOut(false));
            }
        }
	}


	IEnumerator SetInOut(bool trueFalse){
        int num = 50;
        if (trueFalse)
        {
            for (int i = 0; i < num; i++)
            {
                transform.Rotate(new Vector3(0, i, 0));
                yield return new WaitForSeconds(0.01f);
            }
            for (int i = num; i > 0; i--)
            {
                transform.Rotate(new Vector3(0, i, 0));
                yield return new WaitForSeconds(0.01f);
            }
        }else{
            for (int i = 0; i < num; i++)
            {
                transform.Rotate(new Vector3(0, i, 0));
                yield return new WaitForSeconds(0.01f);
            }
        }
        setEnabledRenderers(trueFalse);
        transform.rotation = Quaternion.Euler(0,0,0);
        yield return new WaitForSeconds(0.2f);
        actionFinished = true;
    }

    IEnumerator ChangeAction(string clipName, Vector3 offset, float secToNextAction){
        for (int i = 0; i < 180; i+=10){
            transform.Rotate(0, 20, 0);
            if(i == 140){
                animator.Play(clipName);
                transform.position += offset;
            }
            yield return new WaitForSeconds(0.02f);
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(secToNextAction);
        actionFinished = true;
    }


    void setEnabledRenderers(bool e){
        SpriteRenderer[] renderers;
        renderers = GetComponentsInChildren<SpriteRenderer>();
        Debug.Log(renderers.Length);
        foreach (SpriteRenderer r in renderers)
        {
            r.enabled = e;
        }
    }


}
