using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mail : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	bool isOver;
	public GameObject mailContent;

	void Awake () {
		isOver = false;
		mailContent = GameObject.FindGameObjectWithTag ("MailBoxContainer").GetComponent<MailBoxSystem>().mailContentUI;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnPointerEnter(PointerEventData eventData)
	{
		mailContent.SetActive (true);
		Debug.Log("Mouse enter");
		isOver = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		mailContent.SetActive (false);
		Debug.Log("Mouse exit");
		isOver = false;
	}
}
