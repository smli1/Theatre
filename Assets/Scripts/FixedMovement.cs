using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedMovement : MonoBehaviour {

	[SerializeField]
	string markerTag = "";
	GameObject[] markers;
	float count = 0;
    SpriteRenderer sprite;
	private int lastIndex = 0;

	void Start () {
		markers = GameObject.FindGameObjectsWithTag(markerTag);
		sprite = GetComponent<SpriteRenderer>();
	}


	void FixedUpdate () {
		if (gameObject)
        {
            count += Time.fixedDeltaTime;
            if (count >= 2f)
            {
                count = 0;
				int nextIndex;
				do
				{
					nextIndex = Random.Range(0, markers.Length);
				} while (nextIndex == lastIndex);
				lastIndex = nextIndex;
				//Debug.Log(markers[Random.Range(0,markers.Length)].transform.position);
				StartCoroutine(SpinForMove(markers[nextIndex].transform.position));

            }
        } 
	}

	IEnumerator SpinForMove(Vector3 pos)
    {

        for (int i = 0; i < 18; i++)
        {
            transform.Rotate(0, 5, 0);
            if (i == 9)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                transform.position = pos;
                if (sprite)
                {
                    sprite.flipX = Random.Range(0, 2) == 1 ? true : false;
                }

            }
            yield return new WaitForSeconds(0.01f);
        }
		transform.rotation = Quaternion.Euler(0, 0, 0);


    }
}
