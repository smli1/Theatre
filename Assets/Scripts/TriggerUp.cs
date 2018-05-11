using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerUp : MonoBehaviour {

    bool isGrounded;
    Rigidbody rigb;

	private void Start()
	{
        rigb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
        float DistanceToTheGround = GetComponent<Collider>().bounds.extents.y;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, DistanceToTheGround + 0.1f);
        Debug.Log(isGrounded);
        if (!isGrounded)
        {
            
            rigb.AddForce(-Vector3.right * 0.1f);

        }

        GetComponent<Rigidbody>().AddForce(-Vector3.up * 0.98f / 2);
	}
    /*
	private void OnTriggerEnter(Collider other)
	{
        
        if(other.tag == "fairy"){
            
            rigb.AddForce(Vector3.up * 50);
        }
	}
    */

    public void ApplyForceUp(Vector3 f){
        if (transform.position.y <= 50f)
        {
            rigb.AddForce(f);
        }
    }
}
