using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LinkObject : MonoBehaviour {

	GameObject clickedObject;
	GameObject linkedObject1, linkedObject2;
	LineRenderer line;

	[SerializeField]
	Material LineMaterial;

	public void Start(){
		line = GetComponent<LineRenderer> ();
		line.sortingLayerName = "OnTop";
		line.sortingOrder = 5;
		line.positionCount = 2;
		line.startWidth = 0.5f;
		line.endWidth = 0.5f;
		//line.widthCurve;
		line.useWorldSpace = true;
		line.material = LineMaterial;
	}

	public void Update(){
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100)) {
				Debug.Log (hit.transform.gameObject.name);
				clickedObject = hit.transform.gameObject;
			}
		}else if(Input.GetMouseButtonUp (0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100)) {
				if(hit.transform.gameObject != clickedObject){
					Debug.Log (hit.transform.gameObject.name);
					linkedObject1 = clickedObject;
					linkedObject2 = hit.transform.gameObject;
					clickedObject = null;
				}
			}
		}

		if(linkedObject1 != null && linkedObject2 != null){
			line.SetPosition(0, linkedObject1.transform.position);
			line.SetPosition(1, linkedObject2.transform.position);
		}
	}
}
