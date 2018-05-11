using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

    LinkedList<GameObject> followingQueue;

	private void OnTriggerEnter(Collider other)
	{
        if(other.tag == "mail"){
            if(!followingQueue.Contains(other.gameObject)){
                followingQueue.AddLast(other.gameObject);
                Debug.Log("Added");
            }
        }
	}

	private void Start()
	{
        followingQueue = new LinkedList<GameObject>();
	}

	private void Update()
	{
        if (followingQueue.Count != 0)
        {
            LinkedListNode<GameObject> node = followingQueue.First;
            while (node != null)
            {
                GameObject temp = node.Value;
                if (node == followingQueue.First)
                {
                    float disTemp = Vector3.Distance(gameObject.transform.position, temp.transform.position);
                    if (disTemp >= 3f)
                    {
                        Vector3 vectorTemp = (gameObject.transform.position - temp.transform.position).normalized;
                        //temp.GetComponent<Rigidbody>().AddForce(vectorTemp * 5);
                        //temp.transform.position += vectorTemp * 10f * Time.deltaTime;
                        temp.transform.position = Vector3.Lerp(temp.transform.position, temp.transform.position + vectorTemp * 10 , 0.1f);
                        //Debug.Log(""+temp.name);
                    }
                }else{
                    GameObject temp2 = node.Previous.Value;
                    float disTemp = Vector3.Distance(temp2.transform.position, temp.transform.position);
                    if (disTemp >= 3f)
                    {
                        Vector3 vectorTemp = (temp2.transform.position - temp.transform.position).normalized;
                        //temp.GetComponent<Rigidbody>().AddForce(vectorTemp * 5);
                        //temp.transform.position += vectorTemp * 10f * Time.deltaTime;
                        temp.transform.position = Vector3.Lerp(temp.transform.position, temp.transform.position + vectorTemp * 10f, 0.1f);
                    }
                }
                node = node.Next;
            }
        }

	}
}
