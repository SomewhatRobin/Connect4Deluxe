using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoThere : MonoBehaviour
{

    private Rigidbody rb;
    public float fallForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Stolen wholesale from the ball in box. This jumps the knife down, making it go faster without dropping it from a time-consuming height.
        rb.AddForce(Vector3.right * fallForce, ForceMode.Impulse);
    }

}
