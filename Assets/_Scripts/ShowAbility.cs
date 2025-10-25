using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAbility : MonoBehaviour
{
    public InvisHand ivisHand;
    
    public GameObject[] usedSpells;
    // Start is called before the first frame update
    void Start()
    {
        usedSpells[0].SetActive(false);
        usedSpells[1].SetActive(false);
        usedSpells[2].SetActive(false);
        usedSpells[3].SetActive(false);
        usedSpells[4].SetActive(false);
        usedSpells[5].SetActive(false);
        usedSpells[6].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(ivisHand.AbiliKey) || Input.GetKeyUp(ivisHand.PlaceKey) ||
            Input.GetKeyDown(ivisHand.AbiliKey) || Input.GetKeyDown(ivisHand.PlaceKey) ||
            Input.GetKeyDown(ivisHand.LeftKey) || Input.GetKeyDown(ivisHand.RightKey))
        {
            for (int col = 0; col < (ivisHand.boardWidth); col++) 
            {
                if (ivisHand.usedUp[col] == true)
                {
                    usedSpells[col].SetActive(true);
                   
                }
            }
        }
    }
}
