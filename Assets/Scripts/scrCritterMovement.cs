using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrCritterMovement : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Transform critter;

    [Range(1, 5)]
    public float speed;
    //This bool should be true whenever the critter hits the ground. It is inteded for a movement check
    private bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
    }

    private void FixedUpdate()
    {
        if (isGrounded == true)
        {
            runCritterMovement();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            //print("Hit the ground");
            rigid.gravityScale = 0;
            rigid.velocity = Vector3.zero;
            isGrounded = true;
        }
    }

    private void runCritterMovement()
    {
        //Decide on a system for collision detection or whatever you want to stop this thing. F.eks, an inumerator routine to make it randomly either stop or switch direction.
        critter.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
