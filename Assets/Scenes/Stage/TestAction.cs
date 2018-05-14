using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : MonoBehaviour {
    Animator animator;
    bool actionFinished = false;
    bool isInOut = false;
	public bool  isInOuting = false;
    public int nextActionNum = 0;
    Vector3 defaultPosY;

    public string[] clipNames;
	//public double[] afterClipDelayArray;
	public int[] actionNumArray;
	public double[] eachActionDelayArray;
	public Vector3[] offsetArray;
	public Transform[] offsetMarkerArray;
	public bool isFixedPos;
	//public bool isReadyForNext;
	//public bool[] applyClipDelay;
	public GameObject smoke;

	void Start () {
		//isReadyForNext = false;
        defaultPosY = new Vector3(0,transform.position.y,0);
        animator = GetComponent<Animator>();
        SetInOut(true);
        //StartCoroutine(changeAction(clipNames[2], new Vector3(1,0,0), 0.2f));
        Initialize();

	}

    void Initialize(){
        if(offsetMarkerArray == null){
            offsetMarkerArray = new Transform[actionNumArray.Length];
        }
        if (offsetMarkerArray == null)
        {
            offsetMarkerArray = new Transform[actionNumArray.Length];
        }
        if (offsetArray == null)
        {
            offsetArray = new Vector3[actionNumArray.Length];
        }
        if (eachActionDelayArray == null)
        {
			eachActionDelayArray = new double[actionNumArray.Length];
        }

        printTotalActionTime();
    }

	private void Update()
	{
		if (actionFinished && !ScriptManager.isScripting)
        {
            actionFinished = false;
			//Debug.Log(nextActionNum);
            if (nextActionNum != -1)
            {
				double delayTime = -1; // -1 equals to pause action
                if(eachActionDelayArray[nextActionNum] >= 0){
                    delayTime = eachActionDelayArray[nextActionNum];
                }

                ChangeAction(clipNames[actionNumArray[nextActionNum]], getOffsetPosition(nextActionNum), delayTime,isFixedPos);

                if (nextActionNum >= actionNumArray.Length -1)
                {
                    nextActionNum = -1;
                }
                else
                {
                    nextActionNum++;
                }
            }
            else
            {
				//SetInOut(false);
				//LevelManager.NextLevel();

            }
        }
        else
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
             //       NextAction();
            //}
        }
	}

	//public bool GetReady(){
		//if(isReadyForNext){
		//	isReadyForNext = false;
		//	return true;
		//}else{
		//	return false;
		//}
	//}


	IEnumerator SetInOutThread(bool trueFalse){
        int num = 50;

        if (trueFalse && !isInOut)
        {
            isInOut = true;
            Instantiate(smoke,transform.position-Vector3.up,Quaternion.identity);
            setEnabledRenderers(trueFalse);
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
        }else if (!trueFalse && isInOut ){
            isInOut = false;
            for (int i = 0; i < num; i++)
            {
                transform.Rotate(new Vector3(0, i, 0));
                yield return new WaitForSeconds(0.01f);
            }
            setEnabledRenderers(trueFalse);
            Instantiate(smoke, transform.position- Vector3.up, Quaternion.identity);
        }

        transform.rotation = Quaternion.Euler(0,0,0);
        yield return new WaitForSeconds(0.2f);
        actionFinished = true;
		isInOuting = false;
		//isReadyForNext = true;
    }

	IEnumerator ChangeActionThread(string clipName, Vector3 offset, double secToNextAction, bool offsetToFixedPos = false){
        for (int i = 0; i < 90; i+=5){
            transform.Rotate(0, 10, 0);
            if(i == 45){
                transform.rotation = Quaternion.Euler(0, -90, 0);
                animator.Play(clipName);
                if (!offsetToFixedPos)
                {
                    transform.position += offset;
                }else{
                    transform.position = offset;
                }
				if (nextActionNum != -1)
                {
                    GameObject.Find("Manager").GetComponent<ScriptManager>().ChangeTextScript(nextActionNum);
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (secToNextAction >= 0 )
        {
			yield return new WaitForSeconds((float)secToNextAction);
            actionFinished = true;

        }
		//isReadyForNext = true;

    }

	public void ChangeAction(string clipName, Vector3 offset, double secToNextAction, bool offsetToFixedPos = false){
        StartCoroutine(ChangeActionThread(clipName, offset, secToNextAction, offsetToFixedPos));
    }

    public void SetInOut(bool trueFalse){
		isInOuting = true;
        StartCoroutine(SetInOutThread(trueFalse));
    }

    public void NextAction(){
        StopCoroutine("ChangeActionThread");
        actionFinished = true;
    }


    void setEnabledRenderers(bool e){
        SpriteRenderer[] renderers;
        renderers = GetComponentsInChildren<SpriteRenderer>();
        //Debug.Log(renderers.Length);
        foreach (SpriteRenderer r in renderers)
        {
            r.enabled = e;
        }
    }

    Vector3 getOffsetPosition(int num){
        if (num < offsetMarkerArray.Length)
        {
            if (offsetMarkerArray[num] != null)
            {
                return offsetMarkerArray[num].position;
            }else{
                if (num < offsetArray.Length)
                {
                    return offsetArray[num];
                }
                else
                {
                    Debug.LogError("offsetArray out of index");
                }
            }
        }
        else
        {
            //Debug.LogError("offsetMarkerArray out of index");
            if (num < offsetArray.Length)
            {
                return offsetArray[num];
            }
            else
            {
                Debug.LogError("offsetArray out of index");
            }
        }
       
        return Vector3.zero;
    }

    void printTotalActionTime(){
		double sum = 0;
        for (int i = 0; i < actionNumArray.Length; i++){
            if(eachActionDelayArray[i] >= 0f){
                sum += eachActionDelayArray[i];
            }

        }
        sum += 2; // in and out animation Time
        sum += 0.68f * actionNumArray.Length; // total change anim time
        Debug.Log("TotalAnimTime("+gameObject.name+"): " + sum+" sec");
    }

}
