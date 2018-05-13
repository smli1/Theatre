using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScriptManager : MonoBehaviour {

	public static string[] textScripts;
	public static string[] textScriptsColor;
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

	private void Start()
	{
		ImportDataToTextScripts();
	}
    
	private void Update()
	{
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
		}
	}

	public void ChangeTextScript(int currentActionNum){
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

	public int[] getActionTextScript(int currentActionNum){
		//string temp = "none";
		List<int> temp = new List<int>();
		for (int i = 0; i < actionNum.Length; i++){
			if(currentActionNum == actionNum[i]){
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
			return new int[]{-1};
		}
		else
		{
			return new int[] { -1 };
		}
	}
	private IEnumerator ShowWordWithAnimation(string text, float duration){
		string[] temp = text.Split(' ');
		string wholeText = "";
		for (int i = 0; i < temp.Length; i++)
		{
			
			if (i != 0 )
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
				actionNum = new int[tsTemp.Count];
				for (int j = 0; j < tsTemp.Count; j++){
					textScripts[j] = tsTemp[j].text;
					textScriptsColor[j] = tsTemp[j].textColor;
					actionNum[j] = tsTemp[j].showOnActionNum;
				}
                return;
            }
        }
    }

	public void Reset()
	{
		isActive = false;
		StopCoroutine("ShowWordWithAnimation");      
		currentText = "";
		textScriptContainer.text = "";
	}
}
