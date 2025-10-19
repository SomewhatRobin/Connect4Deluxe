using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisHand : MonoBehaviour
{
    //public GameObject theHand;
    public GameObject Player1Chip;
    public GameObject Player2Chip;
    public GameObject GarbChip;

    bool whoTurn = false;

    private GameObject nowPlaying;

    public int boardHeight = 6;
    public int boardWidth = 7;
    private int[,] onBoard; //0 is empty, 1 is P1Chip, 2 is P2Chip, 3 is GarbageChip

    public KeyCode PlaceKey;
    public KeyCode AbiliKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;



   // private bool myWay = true;
    public int selColumn = 3;
    public GameObject[] handOver;
    public GameObject[] starsAbove;

    // Start is called before the first frame update
    void Start()
    {
        nowPlaying = Player1Chip;
        transform.position = new Vector3 (handOver[3].transform.position.x, handOver[3].transform.position.y + 0.5f, handOver[3].transform.position.z);
        onBoard = new int[boardWidth, boardHeight];
    }



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(PlaceKey))
        {
            PlaceChip(nowPlaying, selColumn);
            //PlaceChip calls SwapTurn
        }

        if (Input.GetKeyDown(LeftKey))
        {
            if(selColumn > 0)
            {
                selColumn--;
                transform.position = handOver[selColumn].transform.position;
            }
            
            //Else play nuh-uh.wav

        }

        if (Input.GetKeyDown(RightKey))
        {
            if (selColumn < 6)
            {
                selColumn++;
                transform.position = handOver[selColumn].transform.position;
            }

            //Else play nuh-uh.wav

        }

        if (Input.GetKeyDown(AbiliKey))
        {
            BoardFill();
            //This takes turn, so it calls SwapTurn
            SwapTurn();
        }


      

    }

    void PlaceChip(GameObject curPlayer, int column)
    {
        if (curPlayer == Player1Chip)
        {
            whoTurn = false;
        }
        else if (curPlayer == Player2Chip)
        {
            whoTurn = true;
        }
        else
        {
            Debug.Log("Didn't Update whoTurn neener neener");
        }

        if (UpdateOnBoard(whoTurn, column))
        {
            Instantiate(curPlayer, handOver[column].transform.position, Quaternion.identity);

            SwapTurn();

        }
        
    }



    void SwapTurn()
    {


        if (nowPlaying == Player1Chip)
        {
            nowPlaying = Player2Chip;
            
        }
        else if (nowPlaying == Player2Chip)
        {
            nowPlaying = Player1Chip;
            
        }
        /* //Forbidden Spray Nozzle
        if(selColumn < 5 && selColumn > 1 && myWay)
            {
                selColumn--;
            }
        else if(selColumn <= 1 && myWay)
            {
                myWay = false;
                selColumn++;
            }
        else if (selColumn < 5 && selColumn > 1 && !myWay)
            {
                selColumn++;
            }
        else if (selColumn >= 5 && !myWay)
            {
                myWay = true;
                selColumn--;
            }
        else
            {
            //   Debug.Log("HELP!");
            return;
            }

        //End of Nozzle
        */

    }

    void BoardFill()
    {
        //Quaternion.Euler(0f,90f,0f)

        //Check for col 0 full
        if (onBoard[0, boardHeight - 1] == 0)
        {
            //Places the Garbage
            Instantiate(GarbChip, starsAbove[0].transform.position, Quaternion.Euler(0f, 90f, 0f));
            //TODO: Add 3 to the proper spot in onBoard.
            //Maybe have a separate function for this?
            //This is copy/pastable, so be sure to for the others
			for (int row = 0; row < boardHeight; row++)
			{
            if (onBoard[0, row] == 0) //Finds empty spot
				{
					onBoard[0, row] = 3;
					break;
				}
				
			}
        }
        else
        {
            Debug.Log("Ouch!");
        }


        //Add check for col 1 full
        if (onBoard[1, boardHeight - 1] == 0)
        {
            Instantiate(GarbChip, starsAbove[1].transform.position, Quaternion.Euler(0f, 90f, 0f));
            //This is copy/pastable, so be sure to for the others
			for (int row = 0; row < boardHeight; row++)
			{
            if (onBoard[1, row] == 0) //Finds empty spot
				{
					onBoard[1, row] = 3;
						break;
				}
				
			}
        }
        else
        {
            Debug.Log("Ow!");
        }
        //Add check for col 2 full
        if (onBoard[2, boardHeight - 1] == 0)
        {
            Instantiate(GarbChip, starsAbove[2].transform.position, Quaternion.Euler(0f, 90f, 0f));
            //This is copy/pastable, so be sure to for the others
			for (int row = 0; row < boardHeight; row++)
			{
            if (onBoard[2, row] == 0) //Finds empty spot
				{
					onBoard[2, row] = 3;
						break;
				}
				
			}
        }
        else
        {
            Debug.Log("Owowow!");
        }
        //Add check for col 3 full
        if (onBoard[3, boardHeight - 1] == 0)
        {
          Instantiate(GarbChip, starsAbove[3].transform.position, Quaternion.Euler(0f, 90f, 0f));
            //This is copy/pastable, so be sure to for the others
            //Possibly add a win con if this makes col 3 full
			for (int row = 0; row < boardHeight; row++)
			{
            if (onBoard[3, row] == 0) //Finds empty spot
				{
					onBoard[3, row] = 3;
						break;
				}
				
			}
        }
        else
        {
            Debug.Log("Ay caramba!");
        }

        //Add check for col 4 full
        if (onBoard[4, boardHeight - 1] == 0)
        {
            Instantiate(GarbChip, starsAbove[4].transform.position, Quaternion.Euler(0f, 90f, 0f));
            //This is copy/pastable, so be sure to for the others
			for (int row = 0; row < boardHeight; row++)
			{
            if (onBoard[4, row] == 0) //Finds empty spot
				{
					onBoard[4, row] = 3;
						break;
				}
				
			}
        }
        else
        {
            Debug.Log("Owie!");
        }
        //Add check for col 5 full
        if (onBoard[5, boardHeight - 1] == 0)
        {
          Instantiate(GarbChip, starsAbove[5].transform.position, Quaternion.Euler(0f, 90f, 0f));
            //This is copy/pastable, so be sure to for the others
			for (int row = 0; row < boardHeight; row++)
			{
            if (onBoard[5, row] == 0) //Finds empty spot
				{
					onBoard[5, row] = 3;
						break;
				}
				
			}
        }
        else
        {
            Debug.Log("WOW!");
        }
        //Add check for col 6 full
        if (onBoard[6, boardHeight - 1] == 0)
        {
            Instantiate(GarbChip, starsAbove[6].transform.position, Quaternion.Euler(0f, 90f, 0f));
            //This is copy/pastable, so be sure to for the others
			for (int row = 0; row < boardHeight; row++)
			{
            if (onBoard[6, row] == 0) //Finds empty spot
				{
					onBoard[6, row] = 3;
						break;
				}
				
			}
        }
        else
        {
            Debug.Log("What the?!");
        }


    }


	//Might be able to cut out turn variable, this was a fix that didn't need to happen
    //TODO: Add a Update method for Garbage Dump
    //TODO: Add some detection for not using GarbDump on a spec. column if it's full.
    bool UpdateOnBoard(bool turn, int column)
    {
        for (int row = 0; row < boardHeight; row++)
        {
            if (onBoard[column, row] == 0) //Finds empty spot
            {
                if (!turn)
                {
                    onBoard[column, row] = 1;
                }
                else if (turn) 
                {
                    onBoard[column, row] = 2;
                }
                else
                {
                    onBoard[column, row] = 3;
                }
                Debug.Log("Piece being placed into (" + column + ", " + row + ")");
                return true;
            }
        }
        Debug.LogWarning("Column is FULL!");
        return false;
    }


}

