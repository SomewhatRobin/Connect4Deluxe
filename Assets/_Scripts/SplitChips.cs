using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitChips : MonoBehaviour
{
    public GameObject heldKnife;



    void OnEnable()
    {
        if (heldKnife == null)
        {
            heldKnife = GameObject.FindWithTag("KaliKnife");
        }
        //Had to add variables so this rotates where it should per column
        RotateToMist();
    }


    private void RotateToMist()
    {
        transform.rotation = Quaternion.Euler(0f,180f,90f);
    }


    //This outright removes Garbage Chips, Makes P1/P2 Chips fall through the board, might be able to do that one thing, and have puyo clear play for garbage
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag("P1Chip") || other.transform.CompareTag("P2Chip") || other.transform.CompareTag("GarbChip"))
        {
            //.75s delay so that falling chips aren't split & for style.
            Destroy(other.gameObject, 0.75f);
        }

    }


}