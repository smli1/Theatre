using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoundParts : MonoBehaviour {

    int ownNum;
    string otherTag;

	private void Start()
	{
        ownNum = getNum();
        otherTag = "fairy";
        TriggerRound.Initialize();
        TriggerRound.temps[TriggerRound.testNum++] = gameObject;
	}

	private void OnTriggerEnter(Collider other)
	{
        if(other.tag == otherTag){
            TriggerRound.TriggerObject(ownNum, transform.parent.parent.gameObject);
        }
	}

    int getNum(){
        switch(transform.name){
            case "front":
                return 1;
            case "left":
                return 2;
            case "back":
                return 3;
            case "right":
                return 4;
                
        }
        return 0;
    }


}
