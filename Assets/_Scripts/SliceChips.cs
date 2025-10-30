using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceChips : MonoBehaviour
{
    public GameObject heldKnife;
    public AudioSource audioSrc;


    void OnEnable()
    {
        audioSrc = GetComponent<AudioSource>();

        if (heldKnife == null)
        {
            heldKnife = GameObject.FindWithTag("GhostKnife");
        }
        //Had to add variables so this rotates where it should per column
        RotateToGhost();

    }


   //Rotates the Spawned knife to match the Ghost Knife
    private void RotateToGhost()
    {
        transform.rotation = heldKnife.transform.rotation;
        //Borrowed from the FPS controller, rotates on y so the knifes look like they're pointing the same way that the ghost is
        if (InvisHand.selColumn < 3)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                                            transform.localEulerAngles.y * 0.8f, 
                                            transform.localEulerAngles.z);
        }
        else if (InvisHand.selColumn > 3)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,
                                            transform.localEulerAngles.y * 1.2f,
                                            transform.localEulerAngles.z);
        }
        else
        {
            Debug.Log("This could be a tiebreaker!");
        }

    }



    //This outright removes Garbage Chips, Makes P1/P2 Chips fall through the board, might be able to do that one thing, and have puyo clear play for garbage
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.CompareTag("P1Chip") || other.transform.CompareTag("P2Chip") || other.transform.CompareTag("GarbChip") )
        {
            audioSrc.Play();
            Destroy(other.gameObject);
        }

    }


}