using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceSelect : MonoBehaviour
{
    public GameObject DropKnife;
    public GameObject GhostKnife;
    public Transform[] handOver;
    public Transform[] knifeOver;
    public int sliColumn = 2;

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
        sliColumn = InvisHand.selColumn;
        transform.position = handOver[sliColumn].position;
        transform.rotation = handOver[sliColumn].rotation;
    }

    void Update()
    {
        if (!nvisHand.chipHeld)
        {
            if (Input.GetKeyDown(LeftKey) && !allDone)
            {
                if (sliColumn > 0)
                {
                    sliColumn--;
                    GhostKnife.transform.position = handOver[sliColumn].position;
                    GhostKnife.transform.rotation = handOver[sliColumn].rotation;
                }

                //Else play nuh-uh.wav

            }

            if (Input.GetKeyDown(RightKey) && !allDone)
            {
                if (sliColumn < 6)
                {
                    sliColumn++;
                    GhostKnife.transform.position = handOver[sliColumn].position;
                    GhostKnife.transform.rotation = handOver[sliColumn].rotation;
                }

                //Else play nuh-uh.wav

            }

            //TODO: Add in InvisHand.Hexed() from InvisHand
            if (Input.GetKeyDown(PlaceKey) && !allDone)
            {
               
                allDone = true;
                Invoke("ExorciseKnife", 0.11f);
                ColumnSlash(sliColumn);
                Instantiate(DropKnife, knifeOver[sliColumn].position, Quaternion.identity);
                Invoke("SwapToChip", 0.03f);
                nvisHand.SwapTurn();
            }

            if (Input.GetKeyDown(AbiliKey) && !allDone)
            {
                SwapToChip();
                allDone = true;
                Invoke("ExorciseKnife", 0.1f);
            }

        }


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

    private void SwapToChip()
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
        GhostKnife.SetActive(false);
    }


}