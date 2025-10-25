using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisHand : MonoBehaviour
{
    //TODO: Figure out ortho cam
    //TODO: Adjust Chip Hitboxes (Garb is more off?)
    //TODO: Music & SFX
    //TODO: GS:Horiz (slashed row is all 0s, then is row above, then row above is row 2 above, etc.)
    //TODO: Get GUI working with usedUp

    //public GameObject theHand;
    public GameObject Player1Chip;
    public GameObject Player2Chip;
    public GameObject GarbChip;

    public GameObject P1Ghost;
    public GameObject P2Ghost;
    public GameObject KnifeGhost;
    public GameObject MageHand;
    //This is an array of prefabs, there are 3 variants: Hrz, Vrt, Diag
    public GameObject[] WinLight;
    


   // bool whoTurn = false;

    private GameObject nowPlaying;

    public int boardHeight = 6;
    public int boardWidth = 7;
    public int[,] onBoard; //0 is empty, 1 is P1Chip, 2 is P2Chip, 3 is GarbageChip

    public KeyCode PlaceKey;
    public KeyCode AbiliKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;
    


    public bool chipHeld = true; //Condition check for if either player would be holding a chip/isn't paused
    static public int selColumn = 3;
    public int usedColumn = 0; //Value to track which column activates an ability (For ColSlash/ GS:H)
    public bool[] usedUp = new bool[7]; //Array for if abilities have been used in a round
    public GameObject[] handOver;
    public GameObject[] starsAbove;
    public Transform[] winSpots;
  

    // Start is called before the first frame update
    void Start()
    {
        //Turn off the knife
        KnifeGhost.SetActive(false);
        MageHand.SetActive(false);

        //"Coin-flip" chance for either player to go first. Seems to really like P2 goin first, may overhaul this so it works with a start button instead
        if (Random.value > .5f)
        {
            nowPlaying = Player2Chip;
            P1Ghost.SetActive(false);

        }
        else
        {
            nowPlaying = Player1Chip;
            P2Ghost.SetActive(false);
        }
        //

        transform.position = new Vector3(handOver[3].transform.position.x, handOver[3].transform.position.y + 0.5f, handOver[3].transform.position.z);
        onBoard = new int[boardWidth, boardHeight];
        


    }






    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetKeyDown(PlaceKey) && chipHeld)
        {
            PlaceChip(nowPlaying, selColumn);
            //PlaceChip calls SwapTurn
        }

        if (Input.GetKeyDown(LeftKey))
        {
            if (selColumn > 0)
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

        if (Input.GetKeyDown(AbiliKey) && (selColumn == 1 || selColumn == 3 || selColumn == 6) && chipHeld && !usedUp[selColumn])
        {
            usedUp[selColumn] = true;
            BoardFill();
            //This takes turn, so it calls SwapTurn
            SwapTurn();
        }

        else if (Input.GetKeyDown(AbiliKey) && (selColumn == 0 || selColumn == 2 || selColumn == 5) && chipHeld && !usedUp[selColumn])
        {
            //Store selCol in case the knife is dropped
            usedColumn = selColumn;


            //Swap to Knife Mode
            MageHand.SetActive(true);
            KnifeGhost.SetActive(true);
            chipHeld = false;
            

        }

        else if (Input.GetKeyDown(AbiliKey) && !chipHeld)
        {
            //Swap to Chip Mode
            Invoke("Hexed", 0.07f);

        }

        /*
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Debug SwapTurn Activator
            SwapTurn();
        }
        */

    }

    public void DispelMagic()
    {
        //Turns off MageHand so Knife defaults to the selected column/doesn't have visual mismatch
        MageHand.SetActive(false);
    }




    //Puts the chip back in hand, turns off MageHand
    //Can add stuff for other aimable abilities, if necessary
    public void Hexed()
    {
        chipHeld = true;
        KnifeGhost.SetActive(false);
        if (nowPlaying == Player1Chip)
        {
            P1Ghost.SetActive(true);
        }
        else if (nowPlaying == Player2Chip)
        {
            P2Ghost.SetActive(true);
        }

        Invoke("DispelMagic", 0.1f);
    }



    void PlaceChip(GameObject curPlayer, int column)
    {

        if (UpdateOnBoard(curPlayer, column))
        {
            Instantiate(curPlayer, handOver[column].transform.position, Quaternion.identity);

            SwapTurn();

        }
        
    }



    public void SwapTurn()
    {

        //if player 1 has taken their turn
        if (nowPlaying == Player1Chip)
        {
            //Turn off P1's Ghost, Say it's P2's turn, and Turn on P2's Ghost
            P1Ghost.SetActive(false);
            nowPlaying = Player2Chip;
            P2Ghost.SetActive(true);
            //Check for Wins->Draws
            if (DidWin(1) == true)
            {
                Debug.LogWarning("Player 1 Wins!");
            }
            else if (DidDraw())
            {
                Debug.LogWarning("It's a Draw!");
            }
        }
        //if player 2 has taken their turn
        else if (nowPlaying == Player2Chip)
        {
            //Turn off P2's Ghost, Say it's P1's turn, and Turn on P1's Ghost
            P2Ghost.SetActive(false);
            nowPlaying = Player1Chip;
            P1Ghost.SetActive(true);
            //Check for Wins->Draws
            if (DidWin(2) == true)
            {
                Debug.LogWarning("Player 2 Wins!");
            }
            else if (DidDraw())
            {
                Debug.LogWarning("It's a Draw!");
            }
            //Deactivate abilities, no matter what
            Invoke("DispelMagic", 0.1f);

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
        //Quaternion.Euler(0f,90f,0f) to rotate GarbChips to match other chips

        //Check for col 0 full
        if (onBoard[0, boardHeight - 1] == 0)
        {
            //Places the Garbage
            Instantiate(GarbChip, starsAbove[0].transform.position, Quaternion.Euler(0f, 90f, 0f));
            

			for (int row = 0; row < boardHeight; row++)
			{
            if (onBoard[0, row] == 0) //Finds empty spot
				{
                    //Adds garbage to that spot
					onBoard[0, row] = 3;
					break; //So the entire column doesn't count as being filled with Garbage
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



    bool UpdateOnBoard(GameObject curPlayer, int column)
    {
        for (int row = 0; row < boardHeight; row++)
        {
            if (onBoard[column, row] == 0) //Finds empty spot
            {
                if (curPlayer == Player1Chip)
                {
                    onBoard[column, row] = 1;
                }
                else if (curPlayer == Player2Chip) 
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

    //Bottom Left of Board is x = 3.4, y = 3.8, z = 0.0334
    //3rd Row, Middle Column is x = 6.4, y = 5.8, z = 0.0334
    //Top Right of Board is x = 9.4, y = 8.8, z = 0.0334
    //A Cube w/ x,y = 1 / z = 2.3 scale covers the cells at those positions 

    bool DidDraw()
    {
        for (int x = 0; x < boardWidth; x++)
        {
            if (onBoard[x, boardHeight - 1] == 0)
            { 
                return false; 
            }
        }
        return true;
    }

    bool DidWin(int playerNum)
    { 
        // winSpots[] Order mirrors order within array
        //Horizontal Check - [0]
        for (int x = 0; x < boardWidth - 3; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                if (onBoard[x,y] == playerNum && onBoard[x + 1, y] == playerNum &&
                    onBoard[x + 2, y] == playerNum && onBoard[x + 3, y] == playerNum)
                {
                    //Offsets to where the script found the win
                    Instantiate(WinLight[0], new Vector3(winSpots[0].position.x + (float)x,
                                                         winSpots[0].position.y + (float)y,
                                                         winSpots[0].position.z), Quaternion.identity);
                    return true;
                }
            }
        }

        //Vertical check - [1]
        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight - 3; y++)
            {
                if (onBoard[x, y] == playerNum && onBoard[x, y + 1] == playerNum &&
                    onBoard[x, y + 2] == playerNum && onBoard[x, y + 3] == playerNum)
                {
                    //Offsets to where the script found the win
                    Instantiate(WinLight[1], new Vector3(winSpots[1].position.x + (float)x,
                                                         winSpots[1].position.y + (float)y,
                                                         winSpots[1].position.z), Quaternion.identity);
                    return true;
                }
            }
        }

        //Diag Upstairs - [2]
        for (int x = 0; x < boardWidth - 3; x++)
        {
            for (int y = 0; y < boardHeight - 3; y++)
            {
                if (onBoard[x, y] == playerNum && onBoard[x + 1, y+1] == playerNum &&
                    onBoard[x + 2, y+2] == playerNum && onBoard[x + 3, y+3] == playerNum)
                {
                    //Offsets to where the script found the win, rotates to match the winspot transform
                    Instantiate(WinLight[2], new Vector3(winSpots[2].position.x + (float)x,
                                                         winSpots[2].position.y + (float)y,
                                                         winSpots[2].position.z), Quaternion.Euler(0f, 0f, 45f));
                    return true;
                }
            }
        }

        //Diag Reading - [3]
        for (int x = 0; x < boardWidth - 3; x++)
        {
            for (int y = 0; y < boardHeight - 3; y++)
            {
                if (onBoard[x + 3, y] == playerNum && onBoard[x + 2, y + 1] == playerNum &&
                    onBoard[x + 1, y + 2] == playerNum && onBoard[x, y + 3] == playerNum)
                {
                    Instantiate(WinLight[2], new Vector3(winSpots[3].position.x + (float)x,
                                                         winSpots[3].position.y + (float)y,
                                                         winSpots[3].position.z), Quaternion.Euler(0f, 0f, 135f));
                    return true;
                }
            }
        }

        return false;
    }

}


