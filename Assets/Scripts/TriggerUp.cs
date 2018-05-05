using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUp : MonoBehaviour {

    bool isGrounded;
	private void Update()
	{
        float DistanceToTheGround = GetComponent<Collider>().bounds.extents.y;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, DistanceToTheGround + 0.1f);
        Debug.Log(isGrounded);
        if (!isGrounded)
        {
            
            GetComponent<Rigidbody>().AddForce(Vector3.right * 0.1f);

        }

        GetComponent<Rigidbody>().AddForce(-Vector3.up * 0.98f / 2);
	}

	private void OnTriggerEnter(Collider other)
	{
        
        if(other.tag == "fairy" && isGrounded){
            
            GetComponent<Rigidbody>().AddForce(Vector3.up * 100);

            GetComponent<Rigidbody>().AddForce(Vector3.right * 5);
        }
	}
}
