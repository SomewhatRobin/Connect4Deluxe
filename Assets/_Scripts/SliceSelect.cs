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
    static public bool usedAbility = false;
    static public bool allDone = false;


    public KeyCode PlaceKey;
    public KeyCode AbiliKey;
    public KeyCode LeftKey;
    public KeyCode RightKey;

    // Start is called before the first frame update
    void OnEnable()
    {

        sliColumn = InvisHand.selColumn;
        transform.position = handOver[sliColumn].position;
        transform.rotation = handOver[sliColumn].rotation;
    }

    void Update()
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
        if (Input.GetKeyDown(PlaceKey) && !usedAbility && !allDone)
        {
            usedAbility = true;
            allDone = true;
            Invoke("ExorciseKnife", 0.1f);
            Instantiate(DropKnife, knifeOver[sliColumn].position, Quaternion.identity);
            Invoke("ResetAbility", 0.6f);
        }

        if (Input.GetKeyDown(AbiliKey) && !allDone)
        {
            InvisHand.chipHeld = true;
            allDone = true;
            ExorciseKnife();
        }

    }

    private void ExorciseKnife()
    {
        GhostKnife.SetActive(false);
    }

    private void ResetAbility()
    {
        usedAbility = false;
    }

}