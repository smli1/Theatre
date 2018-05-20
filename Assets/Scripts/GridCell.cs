using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridCell : MonoBehaviour,IPointerClickHandler {
	public int x, y;

	public void OnPointerClick(PointerEventData eventData)
	{
		//Debug.Log("Hit");
        GridManager.CellTrigger(x, y);
	}

	public void SetXY(int x, int y){
		this.x = x;
		this.y = y;
	}

}
