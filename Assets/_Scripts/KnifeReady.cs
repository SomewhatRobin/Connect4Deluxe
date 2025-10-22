using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeReady : MonoBehaviour
{
    public Transform[] handOver;
    public int sliColumn = 2;


    // Start is called before the first frame update
    void OnEnable()
    {
        sliColumn = InvisHand.selColumn;
        SliceSelect.allDone = false;
        transform.rotation = handOver[sliColumn].rotation;
    }



}
