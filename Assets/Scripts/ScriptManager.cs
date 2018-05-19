using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScriptManager : MonoBehaviour
{

	public static string[] textScripts;
	public static string[] textScriptsColor;
	public static string[] textActor;
	public static int[] actionNum;
	public static int currentIndex = 0;
	float delayTime = 2f;
	[SerializeField]
	Text textScriptContainer;
	public static bool isScripting = false;
	private static int lastActionNum = -1;

	private List<int> scriptingTextIndex;
	float timeCount = 0;
	private string currentText = "";
	private bool isActive = false;
	private string currentActorName;

	private void Start()
	{
		Reset();
	}

	public void Reset()
	{
		isActive = false;
		currentIndex = 0;
		lastActionNum = -1;
		timeCount = 0;
		isScripting = false;
		StopCoroutine("ShowWordWithAnimation");
		currentText = "";
		textScriptContainer = GameObject.Find("TextScript").GetComponent<Text>();
		ImportDataToTextScripts();
	}

	private void Update()
	{
		if(!textScriptContainer){
			Debug.Log("textContainer disappear");
			textScriptContainer = GameObject.Find("TextScript").GetComponent<Text>();
			return;
		}
		if (isActive)
		{
			if (isScripting)
			{
				timeCount += Time.deltaTime;
				if (timeCount >= delayTime)
				{
					timeCount = 0;
					if (scriptingTextIndex.Count != 0)
					{
						//Debug.Log(""+currentText);
						currentText = textScripts[scriptingTextIndex[0]];
						currentIndex = scriptingTextIndex[0];
						StartCoroutine(ShowWordWithAnimation(currentText, delayTime / 5.0f));

						scriptingTextIndex.RemoveAt(0);
					}
					else
					{
						isScripting = false;
					}
				}
			}

			if (Input.GetMouseButtonDown(0))
			{
				string temp = "<color=" + textScriptsColor[currentIndex] + ">" + currentText + "</color>";
				//string temp = currentText;
				StopCoroutine("ShowWordWithAnimation");
				if (isScripting)
				{

					if (textScriptContainer.text == temp)
					{
						timeCount = delayTime;
					}
					else
					{
						textScriptContainer.text = temp;
					}
				}
				else
				{
					textScriptContainer.text = temp;
				}
			}
			StartCoroutine(animActor());
		}

	}

	IEnumerator animActor(){
		
		int index = currentIndex;
		GameObject actor = GameObject.Find(textActor[index]);      
		if (actor)
		{
			if (currentActorName == actor.name)
            {
				yield break;
            }
			Vector3 scale = actor.transform.localScale;
			currentActorName = actor.name;
			Vector3 targetScale = new Vector3(scale.x * Random.Range(1.1f,0.8f), scale.y * Random.Range(1.05f, 0.95f),scale.z);
			Vector3 currentScale = new Vector3(scale.x,scale.y,scale.z);
			float count = 0;
			while (currentIndex == index)
			{
				float xDiff = Mathf.Abs(targetScale.x - currentScale.x);
				float yDiff = Mathf.Abs(targetScale.y - currentScale.y);
				if( xDiff > 0.01f){
					currentScale.x = Mathf.Lerp(currentScale.x , targetScale.x,Mathf.Sin(count) );
				}
				if (yDiff > 0.01f)
                {
					currentScale.y = Mathf.Lerp(currentScale.y, targetScale.y, Mathf.Sin(count ) );
                }
				if (actor)
				{
					actor.transform.localScale = currentScale;
				}else{
					yield break;
				}
				//Debug.Log("Scale-mag. : "+(actor.transform.localScale - targetScale).magnitude);
				if(xDiff <= 0.01f && yDiff <= 0.01f){
					Debug.Log("Re-random scale.");
					targetScale = new Vector3(scale.x * Random.Range(1.1f, 0.8f), scale.y * Random.Range(1.05f, 0.95f), scale.z);
				}
				yield return new WaitForSeconds(0.02f);
				count += (float)Mathf.PI * 0.02f;
			}
			actor.transform.localScale = scale;
			currentActorName = "";
		}
	}

	public void ChangeTextScript(int currentActionNum)
	{
		isActive = true;
		if (currentActionNum != lastActionNum)
		{
			int[] temp = getActionTextScript(currentActionNum);
			if (temp[0] != -1)
			{
				currentText = textScripts[temp[0]];
				currentIndex = temp[0];
				StartCoroutine(ShowWordWithAnimation(textScripts[temp[0]], 0.38f));
			}
			lastActionNum = currentActionNum;
		}
	}

	public int[] getActionTextScript(int currentActionNum)
	{
		//string temp = "none";
		List<int> temp = new List<int>();
		for (int i = 0; i < actionNum.Length; i++)
		{
			if (currentActionNum == actionNum[i])
			{
				temp.Add(i);

			}
		}
		//Debug.Log(temp.ToArray().Length);
		if (temp.Count == 1)
		{
			currentIndex = temp[0];
			return temp.ToArray();
		}
		else if (temp.Count > 1)
		{
			isScripting = true;
			scriptingTextIndex = temp;
			currentIndex = scriptingTextIndex[0];
			return new int[] { -1 };
		}
		else
		{
			return new int[] { -1 };
		}
	}
	private IEnumerator ShowWordWithAnimation(string text, float duration)
	{
		string[] temp = text.Split(' ');
		string wholeText = "";
		for (int i = 0; i < temp.Length; i++)
		{

			if (i != 0)
			{
				wholeText += " ";
			}
			wholeText += temp[i];
			textScriptContainer.text = "<color=" + textScriptsColor[currentIndex] + ">" + wholeText + "</color>";
			//textScriptContainer.text = wholeText;
			yield return new WaitForSeconds(0.2f);
		}
	}

	private void ImportDataToTextScripts()
	{
		List<Script> scripts = ReadJson.data.script;
		for (int i = 0; i < scripts.Count; i++)
		{
			if (LevelManager.levelWhichScript[LevelManager.levelNum] == i)
			{
				List<Textscript> tsTemp = scripts[i].textscript;
				textScripts = new string[tsTemp.Count];
				textScriptsColor = new string[tsTemp.Count];
				textActor = new string[tsTemp.Count];
				actionNum = new int[tsTemp.Count];
				for (int j = 0; j < tsTemp.Count; j++)
				{
					textScripts[j] = tsTemp[j].text;
					//Debug.Log(textScripts[j]);
					textScriptsColor[j] = tsTemp[j].textColor;
					actionNum[j] = tsTemp[j].showOnActionNum;
					textActor[j] = tsTemp[j].text_actor_name;
				}
				return;
			}
		}
	}
}
