using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAbility : MonoBehaviour
{
    public InvisHand ivisHand;
    
    public GameObject[] usedSpells; //Array of greyed out ability icons
    public GameObject[] stopScreens; //Array of pause & win screen overlays
    public GameObject[] malPages; //Array of manual pages

    public bool lookBook; //Is either player reading?
    public int bookMark;

    // Start is called before the first frame update
    void Start()
    {
        bookMark = 0;
        lookBook = false;

        usedSpells[0].SetActive(false);
        usedSpells[1].SetActive(false);
        usedSpells[2].SetActive(false);
        usedSpells[3].SetActive(false);
        usedSpells[4].SetActive(false);
        usedSpells[5].SetActive(false);
        usedSpells[6].SetActive(false);

        stopScreens[0].SetActive(false);
        stopScreens[1].SetActive(false);
        stopScreens[2].SetActive(false);

        malPages[0].SetActive(false);
        malPages[1].SetActive(false);
        malPages[2].SetActive(false);
        malPages[3].SetActive(false);
        malPages[4].SetActive(false);
        malPages[5].SetActive(false);
        malPages[6].SetActive(false);
      //  malPages[7].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       // ReadIn();

       // NextPage();
       
       // PrevPage();

        if (Input.GetKeyUp(ivisHand.AbiliKey) || Input.GetKeyUp(ivisHand.PlaceKey) ||
            Input.GetKeyDown(ivisHand.AbiliKey) || Input.GetKeyDown(ivisHand.PlaceKey) ||
            Input.GetKeyDown(ivisHand.LeftKey) || Input.GetKeyDown(ivisHand.RightKey)) //Only updates GUI each frame a button is pressed, instead of every frame.
        {
            for (int col = 0; col < (ivisHand.boardWidth); col++)
            {
                if (ivisHand.usedUp[col] == true)
                {
                    usedSpells[col].SetActive(true);

                }
            }
        }

        if (lookBook)
        {
            malPages[bookMark].SetActive(true);
        }
        else
        {
            malPages[bookMark].SetActive(false);
        }


        if (ivisHand.isPause && !ivisHand.DidWin(1) && !ivisHand.DidWin(2))
        {
            stopScreens[0].SetActive(true);
            //The pause overlay is NOT a raycast target so the quit buttons still work.
        }
        else if (ivisHand.isPause && ivisHand.DidWin(1) && !ivisHand.DidWin(2))
        {
            stopScreens[1].SetActive(true);
            //The pause overlay is NOT a raycast target so the quit buttons still work.
        }
        else if (ivisHand.isPause && !ivisHand.DidWin(1) && ivisHand.DidWin(2))
        {
            stopScreens[2].SetActive(true);
            //The pause overlay is NOT a raycast target so the quit buttons still work.
        }
        else if (!ivisHand.isPause)
        {
            stopScreens[0].SetActive(false);
            stopScreens[1].SetActive(false);
            stopScreens[2].SetActive(false);
        }
        else
        {
            Debug.LogWarning("Neither paused nor unpaused???");
        }

    }

    public void ReadIn()
    {
        lookBook = true;
        ivisHand.isPause = true;
    }

    public void ReadOut()
    {
        lookBook = false;

        if (!ivisHand.DidWin(1) && !ivisHand.DidWin(2) && !ivisHand.DidDraw())
        {
            ivisHand.isPause = false;
        }
        
    }

    public void PrevPage()
    {
        bookMark -= 1; //Goes to the previous page
        malPages[bookMark + 1].SetActive(false); //Closes the page you were on
    }

    public void NextPage()
    {
        bookMark += 1; //Goes to the next page
        malPages[bookMark - 1].SetActive(false); //Closes the page you were on
    }


}

