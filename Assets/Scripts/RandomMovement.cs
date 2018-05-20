using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomMovement : MonoBehaviour {

	[SerializeField]
	Transform moveArea;
	[SerializeField]
	float areaDistance = 1;
	//Vector3 targetPos;
	float count = 0;
	SpriteRenderer sprite;
    
	void Start () {
		//targetPos = transform.position;
		sprite = GetComponent<SpriteRenderer>();
	}


	void FixedUpdate () {
		if (gameObject)
		{
			count += Time.fixedDeltaTime;
			if (count >= 2f)
			{
				count = 0;

				Vector3 nextPos = moveArea.position + Vector3.one * Random.Range(-areaDistance,areaDistance);
				Debug.Log(nextPos);
				StartCoroutine(SpinForMove(nextPos));

			}
		}      
	}

	IEnumerator SpinForMove(Vector3 pos)
    {
       
        for (int i = 0; i < 18; i++)
        {
            transform.Rotate(0, 10, 0);
            if (i == 9)
            {
                
				transform.position = pos;
				if(sprite){
					sprite.flipX = Random.Range(0, 2) == 1 ? true : false;
				}
				transform.rotation = Quaternion.Euler(0, 0, 0);
                  
            }
            yield return new WaitForSeconds(0.01f);
        }
		transform.rotation = Quaternion.Euler(0, 0, 0);
    }


}
