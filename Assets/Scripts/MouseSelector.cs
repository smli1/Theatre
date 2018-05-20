using System.Collections;
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
	private static GameObject lastTarget ;
	static List<GameObject> targets;
	private static GameObject searchIcon;

    /*
	void Start () {
		image = GetComponent<Image>();
		targets = new List<GameObject>();
		searchIcon = GameObject.Find("SearchIcon");
		searchIcon.SetActive(false);
		Debug.Log("SearchIcon set false");
	}
	*/


	void Update () {
		if(searchIcon == null){
			searchIcon = GameObject.Find("SearchIcon");
			return;
		}
		transform.position = Input.mousePosition;
		//Debug.Log("isScripting : " + ScriptManager.isScripting);
		if (!isAniming && isActive && !ScriptManager.isScripting && !GridManager.isActive)
		{
			if (!searchIcon.activeSelf)
			{
				searchIcon.SetActive(true);
			}
			if (Input.GetMouseButton(0) && image.fillAmount < 1f && !isPressingDown)
			{
				
				
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit;
				Physics.Raycast(ray, out raycastHit, 1000f);
				//Debug.Log("RayHit " + raycastHit.collider.name);
				if (raycastHit.collider.gameObject == target || targets.Contains(raycastHit.collider.gameObject))
				{
					if(targets.Contains(raycastHit.collider.gameObject)){
						target = raycastHit.collider.gameObject;
						if(lastTarget != target){
							count = 0;
						}                  
					}
					lastTarget = target;
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
				if (targets.Contains(target))
                {
                    targets.Remove(target);
                }
                if (targets.Count == 0)
                {
					searchIcon.SetActive(false);
                    isActive = false;

                }
				Debug.Log("count : " + targets.Count);
				target = null;
                

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
					if (targets.Contains(target))
                    {
                        targets.Remove(target);
                    }
					if (targets.Count == 0)
                    {
						searchIcon.SetActive(false);
                        isActive = false;

                    }
					Debug.Log("count : "+targets.Count);
					//target = null;
					//Debug.Log("Active!: " + " false");
					CameraZoom.isActive = false;

				}else{
					count = 0;
                    image.fillAmount = 0;
				}
			}


		}else{
			
			if (image)
			{
				count = 0;
				image.fillAmount = 0;
			}

		}

		if (Input.GetMouseButtonUp(0))
        {
            isPressingDown = false;
			if (targets.Count != 0)
			{
				CameraZoom.isActive = true;
			}
        }
		//Debug.Log("Active!Update: " + isActive);
	}

	public static void ActiveSelector(GameObject target){
		//Debug.Log("target: "+target);
			MouseSelector.target = target;
			isActive = true;
		    //searchIcon.SetActive(true);
			CameraZoom.isActive = true;
		Debug.Log("Active!: " + isActive);
	}

	public static void ActiveSelector(List<GameObject> targets)
    {
        //Debug.Log("target: "+target);
        MouseSelector.targets = targets;
        isActive = true;      
        CameraZoom.isActive = true;
        //Debug.Log("Active!: " + isActive);
    }

	public static GameObject GetSelected(){
		if(isSelected){
			isSelected = false;
			Debug.Log("lasttarget:"+lastTarget);
			return lastTarget;
		}
		return null;
	}

	IEnumerator countAnimTime(){
		yield return new WaitForSeconds(1.0f);
		count = 0;
        image.fillAmount = 0;
		isAniming = false;
	}

	public void Reset()
	{
		image = GetComponent<Image>();
		targets = new List<GameObject>();
		Debug.Log("Selector reset");
		isActive = false;
		isAniming = false;
		target = null;
		isPressingDown = false;
		searchIcon = GameObject.Find("SearchIcon");
        searchIcon.SetActive(false);
		count = 0;
		if (image)
		{
			image.fillAmount = 0;
		}
	}
}
