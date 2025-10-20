using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbDump : MonoBehaviour
{
    /* Abandon all hope ye who enter here
    
    public GameObject GarbChip;

    public int boardHeight = 6;
    public int boardWidth = 7;
    private int[,] theBoard; //0 is empty, 1 is P1Chip, 2 is P2Chip, 3 is GarbageChip
    public GameObject[] starsAbove;


    // Start is called before the first frame update
    void Start()
    {

        theBoard = new int[boardWidth, boardHeight];

        

    }
    


        public void BoardFill()
    {

        for (int i = 0; i < 42; i++)
        {
            
                theBoard[i % 7, (i - (i % 7) / 7)] = InvisHand.boardPass(i);
                if (theBoard[i % 7, (i - (i % 7) / 7)] != 0) //Check for successful pass
                {
                    Debug.Log("Chip found at " + i % 7 + ", " + (i - (i % 7) / 7) + "!");
                }
            

        }
        //Quaternion.Euler(0f,90f,0f) sets GarbChips to align with other chips/not be sideways [Could Random z for rotations?]

        //Check for col 0 full
        if (theBoard[0, boardHeight - 1] == 0)
        {
            //Places the Garbage
            Instantiate(GarbChip, starsAbove[0].transform.position, Quaternion.Euler(0f, 90f, 0f));
            

			for (int row = 0; row < boardHeight; row++)
			{
            if (theBoard[0, row] == 0) //Finds empty spot
				{
                    //Adds garbage to that spot
					theBoard[0, row] = 3;
					break; //So the entire column doesn't count as being filled with Garbage
				}
				
			}
        }
        else
        {
            Debug.Log("Ouch!");
        }


        //Add check for col 1 full
        if (theBoard[1, boardHeight - 1] == 0)
        {
            Instantiate(GarbChip, starsAbove[1].transform.position, Quaternion.Euler(0f, 90f, 0f));
   
			for (int row = 0; row < boardHeight; row++)
			{
            if (theBoard[1, row] == 0) //Finds empty spot
				{
					theBoard[1, row] = 3;
						break;
				}
				
			}
        }
        else
        {
            Debug.Log("Ow!");
        }
        //Add check for col 2 full
        if (theBoard[2, boardHeight - 1] == 0)
        {
            Instantiate(GarbChip, starsAbove[2].transform.position, Quaternion.Euler(0f, 90f, 0f));
   
			for (int row = 0; row < boardHeight; row++)
			{
            if (theBoard[2, row] == 0) //Finds empty spot
				{
					theBoard[2, row] = 3;
						break;
				}
				
			}
        }
        else
        {
            Debug.Log("Owowow!");
        }
        //Add check for col 3 full
        if (theBoard[3, boardHeight - 1] == 0)
        {
          Instantiate(GarbChip, starsAbove[3].transform.position, Quaternion.Euler(0f, 90f, 0f));
   
            //Possibly add a win con if this makes col 3 full
			for (int row = 0; row < boardHeight; row++)
			{
            if (theBoard[3, row] == 0) //Finds empty spot
				{
					theBoard[3, row] = 3;
						break;
				}
				
			}
        }
        else
        {
            Debug.Log("Ay caramba!");
        }

        //Add check for col 4 full
        if (theBoard[4, boardHeight - 1] == 0)
        {
            Instantiate(GarbChip, starsAbove[4].transform.position, Quaternion.Euler(0f, 90f, 0f));
   
			for (int row = 0; row < boardHeight; row++)
			{
            if (theBoard[4, row] == 0) //Finds empty spot
				{
					theBoard[4, row] = 3;
						break;
				}
				
			}
        }
        else
        {
            Debug.Log("Owie!");
        }
        //Add check for col 5 full
        if (theBoard[5, boardHeight - 1] == 0)
        {
          Instantiate(GarbChip, starsAbove[5].transform.position, Quaternion.Euler(0f, 90f, 0f));
   
			for (int row = 0; row < boardHeight; row++)
			{
            if (theBoard[5, row] == 0) //Finds empty spot
				{
					theBoard[5, row] = 3;
						break;
				}
				
			}
        }
        else
        {
            Debug.Log("WOW!");
        }
        //Add check for col 6 full
        if (theBoard[6, boardHeight - 1] == 0)
        {
            Instantiate(GarbChip, starsAbove[6].transform.position, Quaternion.Euler(0f, 90f, 0f));
   
			for (int row = 0; row < boardHeight; row++)
			{
            if (theBoard[6, row] == 0) //Finds empty spot
				{
					theBoard[6, row] = 3;
						break;
				}
				
			}
        }
        else
        {
            Debug.Log("What the?!");
        }


    }
    
    */
}
