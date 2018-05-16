﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSelector : MonoBehaviour {

	Image image;
	//private float selectTime = 2;
	private float count = 0;
	private bool isAniming = false;
	private static bool isActive = false;
	private static bool isSelected = false;
	private static bool isPressingDown = false;
	static GameObject target;

	void Start () {
		image = GetComponent<Image>();
	}


	void Update () {
		transform.position = Input.mousePosition;
		Debug.Log("Active!:(Update) " + isActive);
		if (!isAniming && isActive && !ScriptManager.isScripting)
		{
			if (Input.GetMouseButton(0) && image.fillAmount < 1f && !isPressingDown)
			{
				
				
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit;
				Physics.Raycast(ray, out raycastHit, 1000f);
				if (raycastHit.collider.gameObject == target)
				{
					count += Time.deltaTime;

				}else{
					if(count - Time.deltaTime > 0){
						count -= Time.deltaTime;
					}else{
						count = 0;
					}
				}
				image.fillAmount = Mathf.Clamp(count, 0f, 1f);
			}else if(image.fillAmount >= 1) {
				isPressingDown = true;
				GetComponent<Animator>().Play("SelectedAnim");
                StartCoroutine(countAnimTime());
                isAniming = true;
                isSelected = true;
                isActive = false;
				Debug.Log("Active!: " +" false");
                CameraZoom.isActive = false;
			}
			else if (Input.GetMouseButtonUp(0))
			{
				if (image.fillAmount >= 1)
				{
					GetComponent<Animator>().Play("SelectedAnim");
					StartCoroutine(countAnimTime());
					isAniming = true;
					isSelected = true;
					isActive = false;
					Debug.Log("Active!: " + " false");
					CameraZoom.isActive = false;
				}else{
					count = 0;
                    image.fillAmount = 0;
				}
			}

			if(Input.GetMouseButtonUp(0)){
				isPressingDown = false;
				CameraZoom.isActive = true;
			}
		}
		//Debug.Log("Active!Update: " + isActive);
	}

	public static void ActiveSelector(GameObject target){
		//Debug.Log("target: "+target);
			MouseSelector.target = target;
			isActive = true;
			if (!isPressingDown)
			{
				CameraZoom.isActive = true;
			}
		Debug.Log("Active!: " + isActive);
	}

	public static bool GetSelected(){
		if(isSelected){
			bool temp = isSelected;
			isSelected = false;
			return temp;
		}
		return isSelected;
	}

	IEnumerator countAnimTime(){
		yield return new WaitForSeconds(1.0f);
		count = 0;
        image.fillAmount = 0;
		isAniming = false;
	}

	public void Reset()
	{
		Debug.Log("Selector reset");
		isActive = false;
		Debug.Log("Active!: " + " false");
		isAniming = false;
		isPressingDown = false;
		count = 0;
		if (image)
		{
			image.fillAmount = 0;
		}
	}
}
