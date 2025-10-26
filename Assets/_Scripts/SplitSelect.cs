using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitSelect : MonoBehaviour
{
    //Can select between 3 sets of 2 rows
    //The row is randomly decided between the 2
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
        sliRow = 1;
        transform.position = handBy[sliRow].position;
        transform.rotation = handBy[sliRow].rotation;

        Invoke("KnifeAim", 0.02f);
    }

    void Update()
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

                //Else play nuh-uh.wav

            }

            if (Input.GetKeyDown(RightKey) && !allDone)
            {
                if (sliRow < 2)
                {
                    sliRow++;
                    KnifeAim();
                }

                //Else play nuh-uh.wav

            }

            //TODO: Make this into GS:H
            if (Input.GetKeyDown(PlaceKey) && !allDone)
            {
                invisHad.usedUp[4] = true; //Marks the column that got into Knife Mode as used
                allDone = true; //Done using the ability
                Invoke("ExorciseKnife", 0.11f); // Call Hexed() from invisHad to deactivate the knife
                GreatSplit(sliRow); //*Guitar Riff Plays*
                Instantiate(SliceKnife, knifeNear[sliRow].position, Quaternion.identity); //Drop the knife, so column is visibly slashed
                Invoke("SwapToChip", 0.03f); //Swap to Chip Mode
                invisHad.SwapTurn(); //Swap To other player's Turn
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
    //TODO: Make this into GS:H
    //SliceChips might still work for GS:H?
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
        invisHad.chipHeld = true;
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
