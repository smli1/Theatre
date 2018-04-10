using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailBoxSystem : MonoBehaviour {
	[SerializeField]
	GameObject mailPrefab;
	// Use this for initialization
	LinkedList<GameObject> mailList;
	public GameObject mailContentUI;

	void Awake(){
		mailContentUI = GameObject.FindGameObjectWithTag ("MailContent");
	}

	void Start () {
		mailList = new LinkedList<GameObject> ();
		for(int i = 0 ; i < transform.childCount ; i ++){
			GameObject temp = transform.GetChild (i).gameObject;
			addNewMail(ref temp);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.G)) {
			//GameObject temp = Instantiate (mailPrefab, Vector3.zero, Quaternion.identity);
			//temp.GetComponent<Image> ().color = new Color (Random.Range(0.5f,0.95f),Random.Range(0.5f,0.95f),Random.Range(0.5f,0.95f));
			//addNewMail (ref temp);
		}
	}

	public void createNewMail(Color c){
		GameObject temp = Instantiate (mailPrefab, Vector3.zero, Quaternion.identity);
		temp.GetComponent<Image> ().color = c;
		addNewMail (ref temp);
	}

	void addNewMail(ref GameObject newMail){
		newMail.transform.SetParent(transform);
		newMail.transform.localScale = new Vector3(1.069045f,5.897617f,1f);
		mailList.AddFirst (newMail);
		newMail.GetComponent<Animator> ().Play ("MailFadeIn");
		if (mailList.Count > 5) {
			GameObject temp = mailList.Last.Value;
			mailList.RemoveLast ();
			temp.transform.SetParent(GameObject.FindGameObjectWithTag("canvas").transform);
			temp.GetComponent<Animator> ().Play ("MailFadeOut");
			StartCoroutine (waitForAnimaitonThenDestroy(temp, 1.0f));
		}
	}

	IEnumerator waitForAnimaitonThenDestroy(GameObject obj, float time){
		for(int i = 0 ; i <= time*50 ; i++){
			obj.transform.localPosition += new Vector3(0f,-2f,0f);
			obj.transform.localScale -= new Vector3(0.05f,0.05f,0.05f);
			yield return new WaitForSeconds (time/50);
		}
		Destroy (obj);
	}
}
