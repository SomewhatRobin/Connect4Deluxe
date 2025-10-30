using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceSelect : MonoBehaviour
{
    public GameObject DropKnife;
    public GameObject GhostKnife;
    public Transform[] handOver;
    public Transform[] knifeOver;
    public int sliColumn;

    static public bool allDone = false;
    public InvisHand nvisHand;

    public KeyCode PlaceKey;
    public KeyCode AbiliKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;

    // Start is called before the first frame update
    void OnEnable()
    {
        nvisHand = GetComponentInParent<InvisHand>();
        sliColumn = nvisHand.usedColumn;
        transform.position = handOver[sliColumn].position;
        transform.rotation = handOver[sliColumn].rotation;

        Invoke("KnifeAim", 0.02f);
    }

    void Update()
    {
        if (!nvisHand.isPause) //None of this works while paused
        {
            if (!nvisHand.chipHeld) //None of this becomes relevant until Chip Mode is off.
            {
                if (Input.GetKeyDown(LeftKey) && !allDone)
                {
                    if (sliColumn > 0)
                    {
                        sliColumn--;
                        KnifeAim();
                    }

                    //Else play nuh-uh.wav

                }

                if (Input.GetKeyDown(RightKey) && !allDone)
                {
                    if (sliColumn < 6)
                    {
                        sliColumn++;
                        KnifeAim();
                    }

                    //Else play nuh-uh.wav

                }


                if (Input.GetKeyDown(PlaceKey) && !allDone)
                {
                    nvisHand.usedUp[nvisHand.usedColumn] = true; //Marks the column that got into Knife Mode as used
                    allDone = true; //Done using the ability
                    Invoke("ExorciseKnife", 0.11f); // Call Hexed() from nvisHand to deactivate the knife
                    ColumnSlash(sliColumn); //COLUMN ZLASH
                    Instantiate(DropKnife, knifeOver[sliColumn].position, Quaternion.identity); //Drop the knife, so column is visibly slashed
                    Invoke("SwapToChip", 0.03f); //Swap to Chip Mode
                    nvisHand.SwapTurn(); //Swap To other player's Turn
                }

                if (Input.GetKeyDown(AbiliKey) && !allDone)
                {
                    SwapToChip();
                    allDone = true;
                    Invoke("ExorciseKnife", 0.1f);
                }

            }
        }

       


    }

    private void KnifeAim()
    {

        if(!nvisHand.chipHeld) //If there's no chip being held, there's no chip being held
        {
            nvisHand.P1Ghost.SetActive(false);
            nvisHand.P2Ghost.SetActive(false);
        }
        //Set the knife to the right spot/rotation
        GhostKnife.transform.position = handOver[sliColumn].position;
        GhostKnife.transform.rotation = handOver[sliColumn].rotation;
    }

    private void ColumnSlash(int column)
    {
        //HE SAID IT HE SAID THE THING
        
            //Places the VOID
            for (int row = 0; row < nvisHand.boardHeight; row++)
            {
                if (nvisHand.onBoard[column, row] != 0) //Finds filled spot
                {
                    //Adds THE VOID to that spot
                    nvisHand.onBoard[column, row] = 0;
                     //So the entire column does count as being filled with NOTHING
                }

                else
                {
                    Debug.Log("https://wiki.teamfortress.com/w/images/2/24/Scout_invincible02.wav");
                }
            }
        
        

    }

    private void SwapToChip() //Swap to Chip Mode
    {
        nvisHand.chipHeld = true;
        Invoke("readyUp",0.05f);
    }

    private void readyUp()
    {
        allDone = false;
    }

    private void ExorciseKnife()
    {
        nvisHand.Hexed(); //This sets chipHeld to true, deactivates the knife, activates the current player's chip ghost, and then deactivates the mage 
    }


}