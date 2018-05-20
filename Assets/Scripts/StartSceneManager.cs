using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour {
	bool isClicked = false;
	bool isNextScene = false;
	bool isAcive = false;
	public static bool isStartScene = true;
	static bool isConfirmed = false;
	static GameObject choices;
	static GameObject shop;
	static GameObject shopman;
	GameObject redCurtain;

	private void Start()
	{

        if (GameSceneManager.sceneNum == 0)
        {
			Reset();
        }
	}

	public void Reset()
	{
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(StartIntro());
        }
	}

	void FixedUpdate () {
		if (isStartScene)
		{

			if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
			{
				if (isAcive)
				{
					if (!choices)
					{
						choices = GameObject.Find("Choices");
						return;
					}
					if (!shopman)
					{
						shopman = GameObject.Find("shopman");
						return;
					}
					if (!redCurtain)
					{
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
								//isClicked = false;
								//shop.GetComponent<Animator>().Play("GateCloseAnim");
								//choices.GetComponent<Animator>().Play("ChoicesFadeOut");

							}
							//Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 15.0f, Time.fixedDeltaTime);

						}
					}
					if (isConfirmed)
					{
						Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 45.0f, Time.fixedDeltaTime);
						if (!isNextScene)
						{
							StartCoroutine(NextAction());
							isNextScene = true;
						}
					}
				}
			}
			else
			{
				this.enabled = false;
			}
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

	IEnumerator StartIntro(){
		GameObject temp = GameObject.Find("Intro");
		Debug.Log(temp.name);
		if (temp)
		{
			Animator animator = GameObject.Find("Intro").GetComponent<Animator>();
			if (animator)
			{
				animator.Play("LogoFadeIn");
				yield return new WaitForSeconds(4f);
				animator.Play("AllFadeOut");
				yield return new WaitForSeconds(2.5f);
				isAcive = true;
			}
		}
	}

	IEnumerator NextAction(){
		yield return new WaitForSeconds(1f);
		StageCurtainSwitch.SwitchCurtain(false);
		//GameObject temp = GameObject.Find("DL");
		//Light tempL = temp.GetComponent<Light>();
		//while(tempL.intensity < 0.5f){
			//tempL.intensity += 0.05f;
			//yield return new WaitForSeconds(0.02f);
		//}
		yield return new WaitForSeconds(3f);
		GameObject.Find("Manager").GetComponent<GameSceneManager>().NextScene();
	}
}
