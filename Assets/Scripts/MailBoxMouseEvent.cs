using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MailBoxMouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public static FairyMovement fairyScript;

	// Use this for initialization
	void Start () {
		fairyScript = GameObject.FindGameObjectWithTag ("fairy").GetComponent<FairyMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		fairyScript.setEnable (false);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (MailMouseEvent.draggingObj == null) {
			fairyScript.setEnable (true);
		} 
	}
}
