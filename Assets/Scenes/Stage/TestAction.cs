using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAction : MonoBehaviour {
    Animator animator;
    bool actionFinished = false;
    bool isInOut = false;
	public bool  isInOuting = false;
    public int nextActionNum = 0;
    Vector3 defaultPosY;

    public string[] clipNames;
	public int[] actionNumArray;
	public double[] eachActionDelayArray;
	public Vector3[] offsetArray;
	public Transform[] offsetMarkerArray;
	public bool isFixedPos;
	public int startActionNum;
	public GameObject smoke;
	private string lastMarkerName = "";
	private bool isDisabled = false;
	public bool isNeedFadeOut = false;
	public bool isNeedFadeIn = true;
	private bool isInitailized = false;
	public bool isSpinable = true;

	private static int classActionNum = 0;

	public static void updateClassActionNum(int num){
		if(num > classActionNum){
			classActionNum = num;
			//Debug.Log("classActionNum: "+classActionNum);
		}
	}

	public void Initialize(){
		//classActionNum = 0;
		if(isInitailized){
			return;	
		}
		isInitailized = true;
		defaultPosY = new Vector3(0, transform.position.y, 0);
        animator = GetComponent<Animator>();
		//Debug.Log(gameObject.name + " / "+startActionNum);
        if (startActionNum == 0)
        {
			if (isNeedFadeIn)
			{
				SetInOut(true);
			}else{
				isInOut = true;
				Enable();
				actionFinished = true;

			}
        }
        else
        {
			//Debug.Log(gameObject.name+" disable");
            Disable();
        }

	}

	private void Disable(){
		if (!isDisabled)
		{
			isDisabled = true;
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(false);
			}
			Image image = GetComponent<Image>();
			if (image)
			{
				image.enabled = false;
			}
			if (animator)
			{
				animator.enabled = false;
			}
		}
	}

	private void Enable()
    {
		if (isDisabled)
		{
			isDisabled = false;
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(true);
			}
			Image image = GetComponent<Image>();
			if (image)
			{
				image.enabled = true;
			}
			if (animator)
			{
				animator.enabled = true;
			}
		}
    }

	private void Update()
	{
		if (actionFinished && !ScriptManager.isScripting && classActionNum >= startActionNum)
        {
            actionFinished = false;

            if (nextActionNum != -1)
            {
				double delayTime = -1; // -1 equals to pause action
                if(eachActionDelayArray[nextActionNum] >= 0){
                    delayTime = eachActionDelayArray[nextActionNum];
                }

				string mName = lastMarkerName;
				//Debug.Log("LastMarker: "+mName);
				Vector3 temp = GetOffsetPosition(nextActionNum);
				ChangeNextAction(clipNames[actionNumArray[nextActionNum]], temp, delayTime, isFixedPos,mName != lastMarkerName);

                if (nextActionNum >= actionNumArray.Length -1)
                {
                    nextActionNum = -1;
					//actionFinished = true;
                }
                else
                {
                    nextActionNum++;
					updateClassActionNum(nextActionNum);
                }
			}else if(nextActionNum == -1){
				if(eachActionDelayArray[eachActionDelayArray.Length-1] <= -1){

					return;
				}
				ActionManager.FinishedAllAction(this);
				if (isNeedFadeOut)
                {
					if (name == "Mail_1")
                    {
                        Debug.Log("Mail_1 : setOut");
                    }
                    SetInOut(false);
                }
			}
        }

		if (classActionNum == startActionNum && startActionNum != 0 && isDisabled)
		{
			nextActionNum = 0;
			//smDebug.Log(transform.name + ": " + nextActionNum + " In!");
			SetInOut(true);
			return;
		}
	}

	IEnumerator SetInOutThread(bool trueFalse){      
		int num = 50;      
        if (trueFalse && !isInOut)
        {
			Enable();
            isInOut = true;
            Instantiate(smoke,transform.position-Vector3.up,Quaternion.identity);
			//SetEnabledRenderers(trueFalse);
			if (isSpinable)
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
				yield return new WaitForSeconds(1f);
			}
        }else if (!trueFalse && isInOut ){
            isInOut = false;
			if (isSpinable)
			{
				for (int i = 0; i < num; i++)
				{
					transform.Rotate(new Vector3(0, i, 0));
					yield return new WaitForSeconds(0.01f);
				}
			}else{
				yield return new WaitForSeconds(0.5f);
			}
			Disable();
            Instantiate(smoke, transform.position- Vector3.up, Quaternion.identity);
			Destroy(gameObject);
        }

        transform.rotation = Quaternion.Euler(0,0,0);
        yield return new WaitForSeconds(0.2f);
        actionFinished = true;
		//Debug.Log("action finished (SetInOutThread)");
		isInOuting = false;
		//isReadyForNext = true;
    }

	IEnumerator MoveSmoothToDes(Vector3 des){
		while (Vector3.Distance(des,transform.position) >= 0.1f)
		{
			transform.position = Vector3.Lerp(transform.position, des, 0.1f);
			yield return new WaitForSeconds(0.02f);
		}
	}

	IEnumerator NoChangeActionThread(string clipName, Vector3 offset, double secToNextAction, bool offsetToFixedPos = false){
		yield return new WaitForSeconds(0.09f);
		if (nextActionNum != -1)
		{
			GameObject.Find("Manager").GetComponent<ScriptManager>().ChangeTextScript(nextActionNum);
		}
		yield return new WaitForSeconds(0.09f);
		if (secToNextAction >= 0)
        {
            yield return new WaitForSeconds((float)secToNextAction);
            actionFinished = true;
			//Debug.Log("action finished (NoChangeActionThread)");
        }      
	}

	IEnumerator ChangeActionThread(string clipName, Vector3 offset, double secToNextAction, bool offsetToFixedPos = false){
		if (isSpinable)
		{
			for (int i = 0; i < 18; i++)
			{
				transform.Rotate(0, 10, 0);
				if (i == 9)
				{
					transform.rotation = Quaternion.Euler(0, -90, 0);
					if (animator)
					{
						animator.Play(clipName);
					}
					if (!offsetToFixedPos)
					{
						transform.position += offset;
					}
					else
					{
						transform.position = offset;
					}
					if (nextActionNum != -1)
					{
						GameObject.Find("Manager").GetComponent<ScriptManager>().ChangeTextScript(nextActionNum);
					}
				}
				yield return new WaitForSeconds(0.01f);
			}
		}else{
			yield return new WaitForSeconds(0.09f);
			Debug.Log(clipName+" / "+gameObject.name);
			if (animator)
            {
                animator.Play(clipName);
            }
			if (!offsetToFixedPos)
            {
                transform.position += offset;
            }
            else
            {
				//transform.position = offset;
				//StopCoroutine("MoveSmoothToDes");
				StartCoroutine(MoveSmoothToDes(offset));
            }
			if (nextActionNum != -1)
            {
                GameObject.Find("Manager").GetComponent<ScriptManager>().ChangeTextScript(nextActionNum);
            }
			yield return new WaitForSeconds(0.09f);
		}
        transform.rotation = Quaternion.Euler(0, 0, 0);
        if (secToNextAction >= 0 )
		{
			yield return new WaitForSeconds((float)secToNextAction);
            actionFinished = true;
			//Debug.Log("action finished (ChangeACtionThread) / "+secToNextAction+ " / "+nextActionNum);
			//Debug.Log("secToNextAction: "+secToNextAction+" / "+"curAct: "+(nextActionNum - 1));

        }
		//isReadyForNext = true;

    }

	public void ChangeNextAction(string clipName, Vector3 offset, double secToNextAction, bool offsetToFixedPos, bool haveChangeAction){
		if (haveChangeAction)
		{
			StartCoroutine(ChangeActionThread(clipName, offset, secToNextAction, offsetToFixedPos));
		}else{
			StartCoroutine(NoChangeActionThread(clipName, offset, secToNextAction, offsetToFixedPos));
		}
    }

    public void SetInOut(bool trueFalse){
		isInOuting = true;
        StartCoroutine(SetInOutThread(trueFalse));
    }

    public void NextAction(){
		Debug.Log("NextAction");
		if (this)
		{
			StopCoroutine("ChangeActionThread");
			actionFinished = true;
            
			Debug.Log("action finished "+"(NextAction)");
			if(nextActionNum == -1 && eachActionDelayArray[eachActionDelayArray.Length - 1] <= -1){
				ActionManager.FinishedAllAction(this);
                if (isNeedFadeOut)
                {
                    SetInOut(false);
                }
			}
		}

    }

	private void SetEnabledRenderers(bool e){
        SpriteRenderer[] renderers;
        renderers = GetComponentsInChildren<SpriteRenderer>();
        //Debug.Log(renderers.Length);
        foreach (SpriteRenderer r in renderers)
        {
            r.enabled = e;
        }
    }

	Vector3 GetOffsetPosition(int num){
        if (num < offsetMarkerArray.Length)
        {
            if (offsetMarkerArray[num] != null)
            {
				lastMarkerName = offsetMarkerArray[num].name;
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

	void PrintTotalActionTime(){
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
