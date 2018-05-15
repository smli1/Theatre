using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour {
	[SerializeField]
	GameObject cellPrefab;
	GridLayout gridLayout;
	static int sizeX = 5,sizeY = 5;
	static GameObject[][] cells;

	void Start () {      
        gridLayout = GetComponent<GridLayout>();
		cells = new GameObject[sizeX][];
		for (int i = 0; i < sizeX; i++){
			cells[i] = new GameObject[sizeY];
		}
		GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX * 40, sizeY * 40);

		//cells = new GameObject[sizeX * sizeY + 1];
		for (int y = 0; y < sizeY; y++ ){
			for (int x = 0; x < sizeX; x++){
				GameObject temp = Instantiate(cellPrefab);
				temp.transform.parent = gameObject.transform;
				temp.name = "cell_" + (x +1) + "x" + (y+1);
				temp.transform.localScale = Vector3.one;
				temp.GetComponent<GridCell>().SetXY(x,y);
				cells[x][y] = temp;
			}
		}
		ShuffleCells();
		//StartCoroutine(FindSolution());
		//FindSolution();
	}

	private static void FindSolution()
	{
		while (true)
		{
			Debug.Log("Loading");
			for (int i = 1; i < 5; i++)
			{


				for (int j = 0; j < 5; j++)
				{


					// show while working 
					//yield return new WaitForSeconds(0.02f);
					//yield return null;

					if (cells[j][i - 1].GetComponent<Image>().color == Color.yellow)
					{
						CellTrigger(j, i);

					}
				}
			}

			if (!IsWon())
			{

				bool gotit = false;

				if ((cells[0][4].GetComponent<Image>().color == Color.yellow) & (gotit == false))
				{
					gotit = true;
					cells[3][0].GetComponent<Image>().color = Color.yellow;
					cells[4][0].GetComponent<Image>().color = Color.yellow;
				}
				if ((cells[1][4].GetComponent<Image>().color == Color.yellow) & (gotit == false))
				{
					gotit = true;
					cells[1][0].GetComponent<Image>().color = Color.yellow;
					cells[4][0].GetComponent<Image>().color = Color.yellow;
				}
				if ((cells[2][4].GetComponent<Image>().color == Color.yellow) & (gotit == false))
				{
					gotit = true;
					cells[3][0].GetComponent<Image>().color = Color.yellow;
				}

				if (gotit == false)
				{
					int top_num = Random.Range(0, 5);
					cells[top_num][0].GetComponent<Image>().color = Color.yellow;
					//ShuffleCells();
				}
			}else{
				Debug.Log("Win");
				break;
			}

		}
	}


	private static bool IsWon(){
		for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
				if (cells[x][y].GetComponent<Image>().color == Color.red)
				{
					return false;
				}
            }
        }
		return true;
	}

	private static void ShuffleCells(){
		for (int i = 0; i < cells.Length; i++)
		{
			for (int j = 0; j < cells[i].Length; j++)
			{
				if (Random.Range(0,5) == 0)
				{
					cells[i][j].GetComponent<Image>().color = Color.yellow;
				}
			}
		}
	}

	public static void CellTrigger(int x, int y)
	{
		
		for (int i = -1; i < 2; i++)
		{
            for (int j = -1; j < 2; j++)
			{
				//if (i == 0 && j == 0)
					//continue;

				if (i == -1 && j == -1)
					continue;
				if (i == -1 && j == 1)
					continue;
				if (i == 1 && j == -1)
                    continue;
				if (i == 1 && j == 1)
                    continue;
				
				if(vaildIndex(x+i,y+j)){
					Image temp = cells[x + i][y + j].GetComponent<Image>();
					if(temp.color == Color.red){
						temp.color = Color.yellow;
					}else{
						temp.color = Color.red;
					}
				}
			}
		}
        
	}

	static bool vaildIndex(int x, int y){
		if(x < 0 || x >= sizeX){
			return false;
		}
		if(y < 0 || y >= sizeY){
			return false;
		}
		return true;
	}
}
