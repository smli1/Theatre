using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

	Quaternion originalRotation;
	private float disMax = 10;
    GameObject chaseTarget;
	float originalFOV;

	bool isConfirmed = false;
	public static bool isActive = false;

	void Start () {
		originalRotation = transform.rotation;
        chaseTarget = GameObject.Find("StageCenter");
		originalFOV = Camera.main.fieldOfView;
		//isActive = false;
	}

    
	void FixedUpdate () {
		if(!chaseTarget){
			chaseTarget = GameObject.Find("StageCenter");
			return;
		}
		if (Input.GetMouseButton(0) && !ScriptManager.isScripting && !GridManager.isActive)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			Physics.Raycast(ray, out raycastHit, 2000f);
			Vector3 target = raycastHit.point;
           
			float disVector = (chaseTarget.transform.position - target).magnitude;
			Vector3 lookAtPoint = (chaseTarget.transform.position - target).normalized;
			lookAtPoint = lookAtPoint * (disVector / 2.0f + Mathf.Clamp(disVector / disMax, 0f, 1f) * disVector / 2);
			lookAtPoint = ((chaseTarget.transform.position - lookAtPoint) - Camera.main.transform.position).normalized;


			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookAtPoint), Time.fixedDeltaTime * 1f);

			if (raycastHit.collider.gameObject)
			{
				//Debug.Log(raycastHit.collider.gameObject.name);
				TriggerAnim(raycastHit.collider.gameObject);
			}
            
			float extraFOV = Mathf.Clamp(1f - Vector3.Distance(transform.position, raycastHit.point) / 100f, 0, 1f);

			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 20 - extraFOV * 5, Time.fixedDeltaTime* 4f);
    
		}
		else
		{
			//transform.rotation = originalRotation;
			if (transform.rotation.eulerAngles.magnitude - originalRotation.eulerAngles.magnitude > 0.1f )
			{
				transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.fixedDeltaTime * 2f);
			}else{
				transform.rotation = originalRotation;
			}
			if (Camera.main.fieldOfView < originalFOV - 0.2f)
			{
				Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, originalFOV, Time.fixedDeltaTime * 3f);
			}else{
				Camera.main.fieldOfView = originalFOV;
			}
		}
	}

	public void TriggerAnim(GameObject gameObject)
    {
        Animator animator = gameObject.GetComponent<Animator>();
		if (animator && animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Animator.StringToHash("Default"))
        {
            animator.Play(gameObject.name + "Anim");
        }
    }

	public void Reset()
	{
		Debug.Log("Camera Zoom reset");
		isActive = false;
		originalRotation = transform.rotation;
        chaseTarget = GameObject.Find("StageCenter");
        originalFOV = Camera.main.fieldOfView;
	}
}
