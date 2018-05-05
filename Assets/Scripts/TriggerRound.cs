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

    private static GameObject temp;

    public static void Initialize(){
        if(!isInitialized){
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
        }else{
            if(CheckVaildOrder(num)){
                
                orderString += "" + num; 
                if(orderString.Length == 4){
                    Debug.Log("Rounded !");
                    temp.GetComponent<TriggerUp>().ApplyForceUp(Vector3.up * 300 + Vector3.right * 500);
                    orderString = "";
                }
            }else{
                orderString = "";
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


}

