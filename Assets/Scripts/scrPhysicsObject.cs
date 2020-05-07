using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrPhysicsObject : MonoBehaviour
{
    public Rigidbody2D rigid;

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            //Set the velocity along the x axis to 0 whilst maintaining the y velocity
            rigid.velocity = Vector3.zero;
            //print("Hitting wall");
        }
    }
}
