using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : MonoBehaviour {
    Animator animator;
    bool actionFinished = false;
    bool isInOut = false;
	public bool  isInOuting = false;
    int nextActionNum = 0;
    Vector3 defaultPosY;

    [SerializeField]
    string[] clipNames;
    [SerializeField]
    float[] afterClipDelayArray;
    [SerializeField]
    int[] actionNumArray;
    [SerializeField]
    float[] eachActionDelayArray;
    [SerializeField]
    Vector3[] offsetArray;
    [SerializeField]
    Transform[] offsetMarkerArray;
    [SerializeField]
    bool[] isFixedPos;
    [SerializeField]
    bool[] applyClipDelay;
    [SerializeField]
    GameObject smoke;

	void Start () {
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
        if (isFixedPos == null)
        {
            isFixedPos = new bool[actionNumArray.Length];
        }
        if (offsetArray == null)
        {
            offsetArray = new Vector3[actionNumArray.Length];
        }
        if (eachActionDelayArray == null)
        {
            eachActionDelayArray = new float[actionNumArray.Length];
        }
        if(applyClipDelay == null){
            applyClipDelay = new bool[actionNumArray.Length];
        }
        printTotalActionTime();
    }

	private void Update()
	{
        if (actionFinished)
        {
            actionFinished = false;
            if (nextActionNum != -1)
            {
                float delayTime = -1; // -1 equals to pause action
                if(eachActionDelayArray[nextActionNum] >= 0){
                    delayTime = eachActionDelayArray[nextActionNum];
                }else if(applyClipDelay[nextActionNum]){
                    if (afterClipDelayArray[actionNumArray[nextActionNum]] >= 0)
                    {
                        delayTime = afterClipDelayArray[actionNumArray[nextActionNum]];
                    }
                }
                ChangeAction(clipNames[actionNumArray[nextActionNum]], getOffsetPosition(nextActionNum), delayTime,isFixedPos[nextActionNum]);
                if (nextActionNum >= actionNumArray.Length - 1)
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
				LevelManager.NextLevel();

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                    NextAction();
            }
        }
	}


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
    }

    IEnumerator ChangeActionThread(string clipName, Vector3 offset, float secToNextAction, bool offsetToFixedPos = false){
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
            }
            yield return new WaitForSeconds(0.01f);
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (secToNextAction >=0 )
        {
            yield return new WaitForSeconds(secToNextAction);
            actionFinished = true;
        }
    }

    public void ChangeAction(string clipName, Vector3 offset, float secToNextAction, bool offsetToFixedPos = false){
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
        float sum = 0;
        for (int i = 0; i < actionNumArray.Length; i++){
            if(eachActionDelayArray[i] >= 0f){
                sum += eachActionDelayArray[i];
            }else if(applyClipDelay[i]){
                if(afterClipDelayArray[actionNumArray[i]] > 0f){
                    sum += afterClipDelayArray[actionNumArray[i]];
                }
            }
        }
        sum += 2; // in and out animation Time
        sum += 0.68f * actionNumArray.Length; // total change anim time
        Debug.Log("TotalAnimTime("+gameObject.name+"): " + sum+" sec");
    }

}
