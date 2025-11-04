using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipSounds : MonoBehaviour
{
    public AudioSource[] audioSrc;
    private Rigidbody rb;
    private bool madeNoise = false;
    public GameObject hBox;

    // Start is called before the first frame update
    void OnEnable()
    {
        audioSrc = new AudioSource[2];
        audioSrc[0] = GetComponent<AudioSource>();
        audioSrc[1] = hBox.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        madeNoise = false;
    }



    // Update is called once per frame
    void Update()
    {
           

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!madeNoise)
        {
            if (collision.transform.CompareTag("P1Chip") || collision.transform.CompareTag("P2Chip") || collision.transform.CompareTag("GarbChip"))
            {
                if (audioSrc.Length >= 1)
                {
                    audioSrc[0].Play();
                }
                madeNoise = true;
            }
            else if (collision.transform.CompareTag("Boarddom"))
            {
                if (audioSrc.Length >= 2)
                {
                    audioSrc[1].Play();
                }
                madeNoise = true;
                
            }
        }

        

    }


}
