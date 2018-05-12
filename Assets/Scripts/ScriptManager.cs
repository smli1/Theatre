using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScriptManager : MonoBehaviour {

	public static string[] textScripts;
	public static int[] actionNum;
	float delayTime = 2f;
	[SerializeField]
	Text textScriptContainer;
	public static bool isScripting = false;

	private List<string> scriptingText;
	float timeCount = 0;
	private string currentText = "";

	private void Start()
	{
		ImportDataToTextScripts();
	}
    
	private void Update()
	{
		if(isScripting){
			timeCount += Time.deltaTime;
			if(timeCount >= delayTime){
				timeCount = 0;
				if (scriptingText.Count != 0)
				{
					currentText = scriptingText[0];
					StartCoroutine(ShowWordWithAnimation(currentText,delayTime/5.0f)); 
                  
					scriptingText.RemoveAt(0);
				}else{
					isScripting = false;
				}
			}
		}

        if (Input.GetMouseButtonDown(0))
        {
            StopCoroutine("ShowWordWithAnimation");
			if (isScripting)
			{
				if(textScriptContainer.text == currentText){
					timeCount = delayTime;
				}else{
					textScriptContainer.text = currentText;
				}
			}else{
				textScriptContainer.text = currentText;
			}
        }
	}

	public void changeTextScript(int currentActionNum){
		string[] temp = getActionTextScript(currentActionNum);
		if(temp[0] != "none"){
			currentText = temp[0];
			StartCoroutine(ShowWordWithAnimation(temp[0], 0.38f));
		}
	}

	public string[] getActionTextScript(int currentActionNum){
		//string temp = "none";
		List<string> temp = new List<string>();
		for (int i = 0; i < actionNum.Length; i++){
			if(currentActionNum == actionNum[i]){
				temp.Add(textScripts[i]);
			}
		}

		if (temp.Count == 1)
		{
			return temp.ToArray();
		}
		else if (temp.Count > 1)
		{
			isScripting = true;
			scriptingText = temp;

			return new string[]{"none"};
		}
		else
		{
			temp.Add("none");
            return temp.ToArray();
		}
	}
	private IEnumerator ShowWordWithAnimation(string text, float duration){
		string[] temp = text.Split(' ');
		string wholeText = "";
		for (int i = 0; i < temp.Length; i++)
		{
			
			if (i != 0)
			{
				wholeText += " ";
			}
			wholeText += temp[i];
			textScriptContainer.text = wholeText;
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
				actionNum = new int[tsTemp.Count];
				for (int j = 0; j < tsTemp.Count; j++){
					textScripts[j] = tsTemp[j].text;
					actionNum[j] = tsTemp[j].showOnActionNum;
				}
                return;
            }
        }
    }
}
