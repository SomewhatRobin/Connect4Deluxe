using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitSelect : MonoBehaviour
{
    //Can select between 3 rows
    //KnifeSelect will be repurposed for this


    public GameObject SliceKnife;
    public GameObject KaliKnife;
    public Transform[] handBy;
    public Transform[] knifeNear;
    public int sliRow;

    static public bool allDone = false;
    public InvisHand invisHad;

    public KeyCode PlaceKey;
    public KeyCode AbiliKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;

    // Start is called before the first frame update
    void OnEnable()
    {

        invisHad = GetComponentInParent<InvisHand>();
        if (invisHad.goldRoad != 1 && invisHad.goldRoad != 2) //If this is being activated for the first time
        {
            sliRow = 1;
            transform.position = handBy[sliRow].position;
            transform.rotation = handBy[sliRow].rotation;
            Invoke("KnifeAim", 0.02f);
        }

        else
        {
           RagingStorm(invisHad.doomRow);
            Invoke("pastStorm", 2.1f);
            Invoke("waitOver", 2.4f);
        }
        
    }

    void Update()
    {
        if (!invisHad.usedUp[4]) //So this doesn't change if Horizon Splitter was used during the round
        {
            if (!invisHad.isPause) //None of this works while paused
            {
                if (!invisHad.chipHeld) //None of this becomes relevant until Chip Mode is off.
                {
                    if (Input.GetKeyDown(LeftKey) && !allDone)
                    {
                        if (sliRow > 0)
                        {
                            sliRow--;
                            KnifeAim();
                        }
                        else
                        {
                            KnifeAim();
                        }


                    }

                    if (Input.GetKeyDown(RightKey) && !allDone)
                    {
                        if (sliRow < 2)
                        {
                            sliRow++;
                            KnifeAim();
                        }

                        else
                        {
                            KnifeAim();
                        }

                        //Else play nuh-uh.wav

                    }


                    if (Input.GetKeyDown(PlaceKey) && !allDone)
                    {
                        invisHad.doomRow = sliRow;
                        invisHad.usedUp[4] = true; //Marks the column that got into Knife Mode as used
                        allDone = true; //Done using the ability
                        Invoke("ExorciseKnife", 0.11f); // Call Hexed() from invisHad to deactivate the knife
                                                        
                        if (!invisHad.whoTurn()) //If it's Player 1's Turn
                        {
                            invisHad.goldRoad = 1;
                            Debug.LogWarning("Player 1 steps up to bat");
                        }

                        else if (invisHad.whoTurn()) //If it's Player 2's Turn
                        {
                            invisHad.goldRoad = 2;
                            Debug.LogWarning("Player 2 begins to strike back");
                        }

                        else //If it's ERROR's Turn
                        {
                            invisHad.goldRoad = 3;
                            Debug.LogWarning("THE GARBAGE IS SPLITTING THE HORIZON?");
                        }

                        Invoke("SwapToChip", 0.03f); //Swap to Chip Mode
                        invisHad.SwapTurn(); //Swap To other player's Turn
                    }

                   

                }

                //Unchanging
                if (Input.GetKeyDown(AbiliKey) && !allDone)
                {
                    SwapToChip();
                    allDone = true;
                    Invoke("ExorciseKnife", 0.1f);
                }
            }
        }


    }

    private void RagingStorm(int aimAt)
    {

        GreatSplit((aimAt * 2) + 1); //*Guitar Riff Plays* - Math in the argument is so the selected row internally lines up with the visually selected row 
        Instantiate(SliceKnife, knifeNear[aimAt].position, Quaternion.identity); //Throw the knife, so row is visibly split
        invisHad.smolWait = true;                                                             //small pause



    }

    private void pastStorm()
    {
       
        invisHad.goldRoad = 0; //Reset goldRoad, so SwapTurn works again
        invisHad.SwapTurn();
        
    }

    private void waitOver()
    {
        invisHad.smolWait = false;
    }

    private void KnifeAim()
    {

        if (!invisHad.chipHeld) //If there's no chip being held, there's no chip being held
        {
            invisHad.P1Ghost.SetActive(false);
            invisHad.P2Ghost.SetActive(false);
        }
        //Set the knife to the right spot/rotation
        KaliKnife.transform.position = handBy[sliRow].position;
        KaliKnife.transform.rotation = handBy[sliRow].rotation;
    }
    

    private void GreatSplit(int row)
    {
        //HE SAID IT HE SAID THE THING

        //Places the VOID
        for (int col = 0; col < invisHad.boardWidth; col++)
        {
            if (invisHad.onBoard[col, row] != 0) //Finds filled spot
            {
                //Adds THE VOID to that spot
                invisHad.onBoard[col, row] = 0;
                //So the entire row does count as being filled with NOTHING
            }

            else
            {
                Debug.Log("Combat Preparation was used last turn.");
            }


        }

        //Conditions check 4 rows up for sliRow = 0, 2 rows up for sliRow = 1, skips if sliRow = 2. 
        if (row < 5)
        {
            for (int r = row; r < (invisHad.boardHeight - row); r++)
            {
                for (int column = 0; column < invisHad.boardWidth; column++)
                {
                    if (invisHad.onBoard[column, r + 1] != 0) //Finds filled spot on row above
                    {
                        //Adds that spot to spot below
                        invisHad.onBoard[column, r] = invisHad.onBoard[column, r + 1];
                        //So the entire row does count as being filled with the row above
                        invisHad.onBoard[column, r + 1] = 0;
                    }

                    else
                    {
                        Debug.Log("All that remained at [" + column + ", " + r + "] was a Red Mist.");
                    }
                }
            }
        }



        invisHad.notHere = false;

    }


    //OG Code for CheckAll
    /*
     
    private void TestAllSpots()
    {
        for (int q = 0; q < invisHad.boardHeight; q++)
        {
            for (int col = 0; col < invisHad.boardWidth; col++)
            {
                if (invisHad.onBoard[col, q] == 1) //Finds filled spot
                {
                    //Adds THE VOID to that spot
                    Debug.Log("P1Chip at [" + col + ", " + q + "].");
                    //So the entire row does count as being filled with NOTHING
                }

                else if (invisHad.onBoard[col, q] == 2) //Finds filled spot
                {
                    //Adds THE VOID to that spot
                    Debug.Log("P2Chip at [" + col + ", " + q + "].");
                    //So the entire row does count as being filled with NOTHING
                }
                else if (invisHad.onBoard[col, q] == 3) //Finds filled spot
                {
                    //Adds THE VOID to that spot
                    Debug.Log("GarbageChip at [" + col + ", " + q + "].");
                    //So the entire row does count as being filled with NOTHING
                }
                else
                {
                    Debug.Log("Nothing There at [" + col + ", " + q + "].");
                }


            }
        }
    }

    */

    private void SwapToChip() //Swap to Chip Mode
    {
        invisHad.chipHeld = true;
        invisHad.notHere = false;
        Invoke("readyUp", 0.05f);
    }

    private void readyUp()
    {
        allDone = false;
    }

    private void ExorciseKnife()
    {
        invisHad.Hexed(); //This sets chipHeld to true, deactivates the knife, activates the current player's chip ghost, and then deactivates the mage 
    }


}
