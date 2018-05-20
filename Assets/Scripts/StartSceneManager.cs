using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour {
	bool isClicked = false;
	bool isNextScene = false;
	static bool isConfirmed = false;
	static GameObject choices;
	static GameObject shop;
	static GameObject shopman;
	GameObject redCurtain;

	void FixedUpdate () {
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0 )
		{
			if(!choices){
				choices = GameObject.Find("Choices");
				return;
			}
			if(!shopman){
				shopman = GameObject.Find("shopman");
				return;
			}
			if(!redCurtain){
				redCurtain = GameObject.Find("Red_cloth_front");
				return;
			}
			if (!isClicked)
			{
				if (!isConfirmed)
				{
					if (Input.GetMouseButtonDown(0))
					{

						//StartCoroutine(NextAction());
						Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
						RaycastHit raycastHit;
						Physics.Raycast(ray, out raycastHit, 1000f);
						GameObject temp = raycastHit.collider.gameObject;
						Debug.Log(temp.name + " triggered");
						if (temp.name == "TicketShop")
						{
							isClicked = true;
							shop = temp;
							temp.GetComponent<Animator>().Play("GateOpenAnim");
							choices.GetComponent<Animator>().Play("ChoicesFadeIn");
						}


					}
					Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 25.0f, Time.fixedDeltaTime);

				}
			}
			else
			{
				if (!isConfirmed)
				{
					if (Input.GetMouseButtonDown(1))
					{
						isClicked = false;
						//StartCoroutine(NextAction());
						shop.GetComponent<Animator>().Play("GateCloseAnim");
						choices.GetComponent<Animator>().Play("ChoicesFadeOut");

					}
					Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 15.0f, Time.fixedDeltaTime);

				}
			}
			if(isConfirmed){
				Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 45.0f, Time.fixedDeltaTime);
				if(!isNextScene){
					StartCoroutine(NextAction());
				}
			}
		}else{
			this.enabled = false;
		}      
	}

	public static void Confirmed(){
		if (!isConfirmed)
		{
			isConfirmed = true;
			choices.GetComponent<Animator>().Play("ChoicesFadeOut");
			shopman.GetComponent<Animator>().Play("shopmanAnim");
		}
	}

	IEnumerator NextAction(){
		yield return new WaitForSeconds(1f);
		StageCurtainSwitch.SwitchCurtain(false);
		GameObject temp = GameObject.Find("DL");
		//Light tempL = temp.GetComponent<Light>();
		//while(tempL.intensity < 0.5f){
			//tempL.intensity += 0.05f;
			//yield return new WaitForSeconds(0.02f);
		//}
		yield return new WaitForSeconds(3f);
		GameObject.Find("Manager").GetComponent<GameSceneManager>().NextScene();
	}
}
