using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTargetCount {

	static float count = 0;
	static int[] missionTargetNum = new int[] {1};
	static int missionNum = 0;


	static public void updateTarget(int targetNum){
		count += targetNum;
		if(count >= missionTargetNum[missionNum]){
			count = 0;
			missionNum++;
			Debug.Log ("Check point move to "+(missionNum+1));
			GameObject.FindGameObjectWithTag ("Player").GetComponent<Movement> ().Enable ();
		}
	}
}
