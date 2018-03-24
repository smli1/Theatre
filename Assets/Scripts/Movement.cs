using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Movement : MonoBehaviour {

	//Rigidbody rigidbody;
	[SerializeField]
	GameObject middlePointPrefab;
	Transform startPoint,endPoint, realStart, realEnd;
	GameObject[] middlePoints;
	Vector3 targetPoint;
	public float speed = 10;
	float currentSpeed = 0;
	float targetSpeed = 0;
	float[] speedMag;
	int speedMagIndex = 0;
	bool isEnable = false;
	bool moveBackward = false;

	[SerializeField]
	bool isRepeat = false;

	LinkedList<Transform> noArrivedPoints;

	void Start () {
		isRepeat = false;
		isEnable = false;
		//speedMag = new float[]{1f,1.1f,1f,0.5f,0.75f,0.6f,1f,1f,0f};
		speedMag = new float[]{3f};
		//rigidbody = GetComponent<Rigidbody> ();
		realStart = startPoint = GameObject.FindWithTag ("startPoint").transform;
		realEnd = endPoint = GameObject.FindWithTag ("endPoint").transform;
		//noArrivedPoints = new LinkedList<Transform> ();

		Reset();
	}

	void Update () {
		if(isEnable){
			//Vector3 pointToEnd = endPoint.position - transform.position;
			//transform.position = Vector3.Lerp(transform.position,targetPoint,0.01f * Time.deltaTime * speed);
			if(targetPoint != Vector3.zero){
				currentSpeed = Mathf.Lerp (currentSpeed,targetSpeed, 0.9f);
				transform.position += ( targetPoint - transform.position).normalized * Time.deltaTime * currentSpeed;
				DoExtra ();
				if(Vector3.Distance(transform.position,targetPoint) <= 1f){
					FindNextPoint ();
				}
			}
		}
	}

	void FindNextPoint(){
		targetSpeed = speed * getSpeedMag(speedMagIndex++);
		if (noArrivedPoints.Count != 0) {
			targetPoint = noArrivedPoints.Last().position;
			//Debug.Log (targetPoint);
			noArrivedPoints.RemoveLast ();
		} else {
			if (targetPoint != endPoint.position) {
				targetPoint = endPoint.position;
			} else {
				if (isRepeat) {
					Reset ();
				} else {
					targetPoint = Vector3.zero;
				}
			}
		}
		//Debug.Log(targetPoint);
	}

	void Reset(){
		if (noArrivedPoints == null) {
			noArrivedPoints = new LinkedList<Transform> ();
		} else {
			noArrivedPoints.Clear ();
		}

		FindMiddlePoints ();

		if(noArrivedPoints.Count == 0){
			CreateMiddlePoints(5);
		}
		transform.position = startPoint.position + new Vector3(0,1,0);
		FindNextPoint ();
	}

	Vector3 randPos(Vector3 startPos, Vector3 endPos){
		float randomX, randomZ;
		randomX = Mathf.Abs (startPos.x - endPos.x);
		randomZ = Mathf.Abs (startPos.z - endPos.z);
		Vector3 temp = new Vector3();
		temp.x = Random.Range (0.0f,randomX);
		temp.y = 0.5f;
		temp.z = Random.Range (0.0f,randomZ);
		//Debug.Log (temp);
		return temp;
	}

	void CreateMiddlePoints(int num){
		List<GameObject> tempPoints = new List<GameObject>();
		for(int i = 0; i < num; i++){
			Vector3 temp = randPos (startPoint.position,endPoint.position);
			temp.x += startPoint.position.x;
			temp.z -= startPoint.position.z;
			GameObject tempObj = (GameObject)Instantiate(middlePointPrefab,temp,Quaternion.identity);
			tempPoints.Add (tempObj);
		}
		middlePoints = tempPoints.ToArray().OrderBy ((a) => a.transform.position.x).ToArray();
		for(int j = 0; j < middlePoints.Length; j++){
			noArrivedPoints.AddFirst (middlePoints [j].transform);
		}
	}

	void FindMiddlePoints(){
		if (moveBackward) {
			middlePoints = GameObject.FindGameObjectsWithTag ("middlePoint").OrderByDescending ((a) => a.transform.position.x).ToArray ();
		} else {
			middlePoints = GameObject.FindGameObjectsWithTag ("middlePoint").OrderBy ((a) => a.transform.position.x).ToArray ();
		}
		for(int j = 0; j < middlePoints.Length; j++){
			noArrivedPoints.AddFirst (middlePoints [j].transform);
		}
	}

	public void MoveBackward(){
		moveBackward = true;
		startPoint = realEnd;
		endPoint = realStart;
		Reset ();
		Enable ();
	}

	public void MoveForward(){
		moveBackward = false;
		startPoint = realStart;
		endPoint = realEnd;
		Reset ();
		Enable ();
	}

	public void Enable(){
		isEnable = true;
	}

	float getSpeedMag(int index){
		if (index > speedMag.Length-1) {
			return speedMag [0];
		} else {
			return speedMag [index];
		}
	}

	void DoExtra(){
		//Debug.Log ("count : " + GetComponentsInChildren<RotateItself> ().Count());
		foreach (RotateItself ri in GetComponentsInChildren<RotateItself> ()) {
			ri.rotateForce (50f);
		}
	}
}
