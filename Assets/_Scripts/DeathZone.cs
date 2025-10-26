using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
   
    void Update()
    {
        if (transform.position.y < -10 || transform.position.x > 40)
        {
            Destroy(this.gameObject);
        }
    }

}

    


