using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
	public static int level = 1;
	[SerializeField]
	GameObject cellPrefab;
	GridLayout gridLayout;
	//static int sizeX = 3, sizeY = 3;
	static GameObject[][] cells;
	GameObject boardUI;
	GameObject actor;
	public static bool isActive = false;

	private readonly int[] pattern1 = { 0, 0, 1,
        							    1, 1, 0,
        							    0, 1, 0 };
	private readonly int[] pattern2 = { 1, 0, 0,
        							    1, 0, 0,
        							    0, 0, 0 };
	private readonly int[] pattern3 = { 1, 0, 0, 0,
        							    1, 1, 1, 0,
        							    0, 0, 1, 0,
        							    0, 0, 1, 1};
	private readonly int[] pattern4 = { 1, 1, 0, 1,
                                        0, 0, 0, 1,
                                        0, 0, 0, 0,
                                        0, 0, 0, 1};
	private readonly int[] pattern5 = { 0, 1, 0, 1, 0,
                                        1, 1, 1, 0, 1,
                                        1, 0, 1, 1, 1,
                                        0, 0, 1, 1, 0,
        							    0, 1, 1, 1, 1};
	//private static bool isGot = false;

	void Start()
	{
		//Initialize(5,5);
		//ShuffleCells();
		//StartCoroutine(FindSolution());
		//FindSolution();
		boardUI = GameObject.Find("BoardUI");
        boardUI.SetActive(false);
		gridLayout = GetComponent<GridLayout>();
        
       
	}

	private void Update()
	{
		if (isActive)
		{
			if (IsWon() || Input.GetKeyDown(KeyCode.P))
			{
				if (actor)
				{
					actor.GetComponent<TestAction>().NextAction();
				}
				for (int y = 0; y < getLevelSize(); y++)
                {
					for (int x = 0; x < getLevelSize(); x++)
                    {
						Destroy(cells[x][y]);
                    }
                }
				boardUI.SetActive(false);
				isActive = false;
				level++;
			}
		}
	}

	private void Initialize(){
		int sizeX = 0, sizeY = 0;
		switch(level){
			case 1:
				sizeX = sizeY = 3;
				break;
			case 2:
				goto case 1;
			case 3:
				sizeX = sizeY = 4;
				break;
			case 4:
				goto case 3;
			case 5:
				sizeX = sizeY = 5;
				break;
		}
		cells = new GameObject[sizeX][];
        for (int i = 0; i < sizeX; i++)
        {
            cells[i] = new GameObject[sizeY];
        }
		GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX * 40, sizeY * 40);


        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                GameObject temp = Instantiate(cellPrefab);
				temp.transform.SetParent(gameObject.transform);
                temp.name = "cell_" + (x + 1) + "x" + (y + 1);
                temp.transform.localScale = Vector3.one;
                temp.GetComponent<GridCell>().SetXY(x, y);
                cells[x][y] = temp;
            }
        }
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
			}
			else
			{
				Debug.Log("Win");
				break;
			}

		}
	}

	private static int getLevelSize(){
		switch (level)
        {
            case 1:
				return 3;
            case 2:
                goto case 1;
            case 3:
				return 4;
            case 4:
                goto case 3;
            case 5:
				return 5;
        }
		return -1;
	}

	private static bool IsWon()
	{
		for (int y = 0; y < getLevelSize(); y++)
		{
			for (int x = 0; x < getLevelSize(); x++)
			{
				if (cells[x][y].GetComponent<Image>().color == Color.red)
				{
					return false;
				}
			}
		}
		return true;
	}

	private static void ShuffleCells()
	{
		for (int i = 0; i < cells.Length; i++)
		{
			for (int j = 0; j < cells[i].Length; j++)
			{
				if (Random.Range(0, 5) == 0)
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

				if (vaildIndex(x + i, y + j))
				{
					Image temp = cells[x + i][y + j].GetComponent<Image>();
					if (temp.color == Color.red)
					{
						temp.color = Color.yellow;
					}
					else
					{
						temp.color = Color.red;
					}
				}
			}
		}

	}

	static bool vaildIndex(int x, int y)
	{
		if (x < 0 || x >= getLevelSize())
		{
			return false;
		}
		if (y < 0 || y >= getLevelSize())
		{
			return false;
		}
		return true;
	}

	public void activeIt(GameObject actor){
		
		this.actor = actor;
		boardUI.SetActive(true);
		Initialize();
		nextLevel();
		isActive = true;
		//GameObject 
	}
    
	private void nextLevel(){
		if(level == 1){
			for (int y = 0; y < 3; y++)
            {
				for (int x = 0; x < 3; x++)
                {
					Color temp = (pattern1[x + y * 3] == 1) ? Color.yellow : Color.red;
					cells[x][y].GetComponent<Image>().color = temp;
                }
            }
		}
		if (level == 2)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Color temp = (pattern2[x + y * 3] == 1) ? Color.yellow : Color.red;
                    cells[x][y].GetComponent<Image>().color = temp;
                }
            }
        }
		if (level == 3)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    Color temp = (pattern3[x + y * 4] == 1) ? Color.yellow : Color.red;
                    cells[x][y].GetComponent<Image>().color = temp;
                }
            }
        }
		if (level == 4)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    Color temp = (pattern4[x + y * 4] == 1) ? Color.yellow : Color.red;
                    cells[x][y].GetComponent<Image>().color = temp;
                }
            }
        }
		if (level == 5)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    Color temp = (pattern5[x + y * 5] == 1) ? Color.yellow : Color.red;
                    cells[x][y].GetComponent<Image>().color = temp;
                }
            }
        }
	}   
}
