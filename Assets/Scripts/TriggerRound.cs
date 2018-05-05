using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRound {

    // 1234
    // 1432
    // 2341
    // 2143
    // 3412
    // 3214
    // 4123
    // 4321

    private static string orderString;

    private static string[] orderArray = { "1234","1432","2341","2143","3412","3214","4123","4321" };

    private static bool[] enterBools;
    private static bool[] stayBools;
    //private static bool[] exitBools;
    private static int dirNum = 4;
    private static bool isInitialized = false;

    public static GameObject[] temps;
    public static int testNum = 0;
    private static GameObject temp;

    public static void Initialize(){
        if(!isInitialized){
            temps = new GameObject[4];
            Debug.Log("Initialized!");
            isInitialized = true;
            enterBools = new bool[dirNum];
            stayBools = new bool[dirNum];
            for (int i = 0; i < dirNum; i++){
                enterBools[i] = false;
                stayBools[i] = false;
            }
            orderString = "";
        }
    }

    public static void TriggerObject(int num, GameObject obj){
        temp = obj;
        CheckOrder(num);

        temp = null;
    }

    private static void CheckOrder(int num){

        if(orderString.Length == 0){
            orderString += "" + num;
            //temp.GetComponent<Renderer>().material.color = Color.red;
        }else{
            if(CheckVaildOrder(num)){
                
                //temp.GetComponent<Renderer>().material.color = Color.red;
                orderString += "" + num; 
                if(orderString.Length == 4){
                    Debug.Log("Rounded !");
                    temp.GetComponent<Rigidbody>().AddForce(Vector3.up * 20);
                    orderString = "";
                    Reset();
                }
            }else{
                orderString = "";
                Reset();
            }
        }
        //Debug.Log(orderString);
    }

    private static bool CheckVaildOrder(int num){
        string temp1 = orderString + num;
        for (int i = 0; i < orderArray.Length; i++){
            string temp2 = orderArray[i].Substring(0, temp1.Length);

            if(temp2 == temp1){
                //Debug.Log(temp2 + " == " + temp1);
                return true;
            }
        }
        return false;
    }

    private static void Reset(){
        for (int i = 0; i < 4; i++){
            temps[i].GetComponent<Renderer>().material.color = Color.white;
        }
    }
}

