using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverChoice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler {

	Animator animator;

	public void OnPointerDown(PointerEventData eventData)
	{
		StartSceneManager.Confirmed();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		animator.Play("Select_Choice_2");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		animator.Play("UnSelect_Choice_2");
	}


	void Start () {
		animator = GetComponent<Animator>();
	}



}
