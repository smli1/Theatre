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
			addNewMail(temp);
		}
	}


	public void createNewMail(GameObject sender,bool isLocked = false){
		GameObject temp = Instantiate (mailPrefab, Vector3.zero, Quaternion.identity);
		temp.GetComponent<Mail> ().intialMail (sender,isLocked);
		temp.GetComponent<Image> ().color = sender.GetComponent<Renderer>().material.color;
		addNewMail (temp);
	}

	void addNewMail(GameObject newMail){
		newMail.transform.SetParent(transform);
		newMail.transform.localScale = new Vector3(1.069045f,5.897617f,1f);
		mailList.AddFirst (newMail);
		newMail.GetComponent<Animator> ().Play ("MailFadeIn");
		if (mailList.Count > 5) {
			LinkedListNode<GameObject> nobe = getRemoveableMail();
			if (nobe != null) {
				mailList.Remove (nobe);
				GameObject temp = nobe.Value;
				temp.transform.SetParent (GameObject.FindGameObjectWithTag ("canvas").transform);
				temp.GetComponent<Animator> ().Play ("MailFadeOut");
				StartCoroutine (waitForAnimaitonThenDestroy (temp, 1.0f));
			} else {
				newMail.transform.SetParent (GameObject.FindGameObjectWithTag ("canvas").transform);
				newMail.GetComponent<Animator> ().Play ("MailFadeOut");
				StartCoroutine (waitForAnimaitonThenDestroy (newMail, 1.0f));
			}
		}
	}

	public void removeMail(GameObject mail){
		LinkedListNode<GameObject> temp = mailList.Find (mail);
		if(temp != null){
			mailList.Remove(temp);
		}
	}

	LinkedListNode<GameObject> getRemoveableMail(){
		LinkedListNode<GameObject> temp = mailList.Last;
		while (temp.Value.GetComponent<Mail>().isLocked){
			if (temp.Previous != null) {
				temp = temp.Previous;
			} else {
				temp = null;
				break;
			}
		}
		return temp;

	}

	IEnumerator waitForAnimaitonThenDestroy(GameObject obj, float time){
		for(int i = 0 ; i <= time*50 ; i++){
			obj.transform.localPosition += new Vector3(0f,-2f,0f);
			if (obj.transform.localScale.x - 0.05f > 0f) {
				obj.transform.localScale -= new Vector3 (0.05f, 0.05f, 0.05f);
			} else {
				break;
			}
			yield return new WaitForSeconds (time/50);
		}
		Destroy (obj);
	}
}
