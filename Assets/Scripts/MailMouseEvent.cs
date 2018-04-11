using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MailMouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,IBeginDragHandler, IDragHandler, IEndDragHandler {

	public GameObject mailContent;
	private Transform previousParent;
	private bool isDragging;
	public static GameObject draggingObj;
	float lastClickTime = 0;

	void Start() {
		isDragging = false;
		mailContent = GameObject.FindGameObjectWithTag ("MailBoxContainer").GetComponent<MailBoxSystem>().mailContentUI;
		previousParent = transform.parent;
	}

	public void OnBeginDrag (PointerEventData pointerEventData) {
		if(draggingObj == null){
			draggingObj = gameObject;
		}
		if(draggingObj == gameObject){
			isDragging = true;
			mailContent.SetActive (false);
			GameObject.FindGameObjectWithTag ("MailBoxContainer").GetComponent<MailBoxSystem> ().removeMail (gameObject);
			transform.SetParent(GameObject.FindGameObjectWithTag("canvas").transform);
		}
	}

	public void OnDrag(PointerEventData pointerEventData) {
		if (draggingObj == gameObject) {
			transform.position = new Vector3 (pointerEventData.position.x, pointerEventData.position.y, 0);
		}
	}

	public void OnEndDrag(PointerEventData pointerEventData) {
		//transform.SetParent( previousParent );
		if (draggingObj == gameObject) {
			isDragging = false;
			Ray ray = Camera.main.ScreenPointToRay (transform.position);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 1000f)) {
				Debug.Log (hit.collider.name);
			}
			draggingObj = null;
			Destroy (gameObject);
		}
	}



	public void OnPointerEnter(PointerEventData eventData)
	{
		if(draggingObj != gameObject && draggingObj == null){
			Text t = mailContent.GetComponentInChildren<Text> ();
			t.text = GetComponent<Mail>().data.content;
			t.font = GetComponent<Mail>().data.getFont();
			t.fontSize = GetComponent<Mail> ().data.fontSize;
			mailContent.SetActive (true);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (draggingObj != gameObject && draggingObj == null) {
			mailContent.SetActive (false);
		}
	}

	public void OnPointerDown(PointerEventData eventData){
		
		if( Time.time - lastClickTime <= 0.3f){
			transform.GetChild(0).gameObject.SetActive (!transform.GetChild(0).gameObject.activeSelf);
			GetComponent<Mail> ().isLocked = transform.GetChild (0).gameObject.activeSelf;
		}
		lastClickTime = Time.time;
	}
}
