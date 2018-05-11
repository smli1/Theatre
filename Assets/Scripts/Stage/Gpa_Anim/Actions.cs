using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : ScriptableObject {

	[SerializeField]
    string clipNames;
    [SerializeField]
    float afterClipDelayArray;
    [SerializeField]
    int actionNumArray;
    [SerializeField]
    float eachActionDelayArray;
    [SerializeField]
    Vector3 offsetArray;
    [SerializeField]
    Transform offsetMarkerArray;
    [SerializeField]
    bool isFixedPos;
    [SerializeField]
    bool applyClipDelay;
    [SerializeField]
    GameObject smoke;
}
