﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrPlayerMovement : MonoBehaviour
{
    //What I am working on
    //Making functions/methods that move the character in a specific direction whilst called upon
    //Then call these methods with the buttons


    #region Editable Vars
    //Movement
    public float jumpForce;
    private float currentWalkSpeed = 0.2f;
    public float maxWalkSpeed = 10;
    public float minspeed = 0;

    [Range(0.2f , 0.5f)]
    public float acceleration;
    [Range(15, 30f)]
    public float deAcceleration;

    //This should be the point from where the collision check is cast?
    public Transform feetPos;
    //This should be the radius of the ground collision check
    [Range(0.1f, 0.5f)]
    public float circleRadius = 0.1f;
    //This should be the specific layers it checks for collision with
    public LayerMask Ground;
    #endregion

    #region Non editable Vars
    //Rigidbody
    private Rigidbody2D rigid;
    //Movement
    private bool moveLeft;
    private bool dontMove;
    public bool canJump;

    private float highJumpCountdownReset = 0.3f;
    private float highJumpCountdown;


    #endregion

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        dontMove = true;
    }
    //Manual collision used to fix the bug where the player gets stuck in the air.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            currentWalkSpeed = 0;
            dontMove = true;
            //print("Hitting the wall");
        }
    }
    void FixedUpdate()
    {
        HandleMoving();

        canJump = Physics2D.OverlapCircle(feetPos.position, circleRadius, Ground, -Mathf.Infinity, Mathf.Infinity);
    }

    #region Movement Logic
    void HandleMoving()
    {
        if (dontMove)
        {
            StopMoving();
        }
        else
        {
            //Increase the currentwalkspeed until it reaches the same speed as walkspeed.
            if (moveLeft)
            {
                MoveLeft();
            }
            else if (!moveLeft)
            {
                MoveRight();
            }
        }
    }
    public void AllowMovement(bool movement)
    {
        dontMove = false;
        moveLeft = movement;
    }

    public void DontAllowMovement()
    {
        dontMove = true;
    }
    public void Jump()
    {
        if (canJump) //Can jump is the bool checking for collision
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        }
    }
    public void ExitJump()
    {
        highJumpCountdown = highJumpCountdownReset;
    }
    #endregion

    #region Acceleration, DeAcelleration and Walkspeed
    public void MoveLeft()
    {
        if (currentWalkSpeed < maxWalkSpeed)
        {
            currentWalkSpeed += acceleration;
        }
        else
        {
            currentWalkSpeed = maxWalkSpeed;
        }
        rigid.velocity = new Vector2(-currentWalkSpeed, rigid.velocity.y);
    }
    public void MoveRight()
    {
        if (currentWalkSpeed < maxWalkSpeed)
        {
            currentWalkSpeed += acceleration;
        }
        else
        {
            currentWalkSpeed = maxWalkSpeed;
        }
        rigid.velocity = new Vector2(currentWalkSpeed, rigid.velocity.y);
    }
    public void StopMoving()
    {
        //Resets the walkspeed
        currentWalkSpeed -= deAcceleration * Time.deltaTime;

        //Stops the character emediately, if the currentwalkspeed is low, to prevent a "ice skating effect", and allow for more precise movement.
        if (currentWalkSpeed < (maxWalkSpeed / 3))
        {
            currentWalkSpeed = 0f;
            rigid.velocity = new Vector2(0f, rigid.velocity.y);
        }

        //Checks that the speed has returned to minspeed before stopping
        if (currentWalkSpeed <= minspeed)
        {
            currentWalkSpeed = 0f;
            rigid.velocity = new Vector2(0f, rigid.velocity.y);
        }
    }
    #endregion
    void DetectInput()
    {
        float X = Input.GetAxisRaw("Horizontal");

        if (X > 0)
        {
            MoveRight();
        }
        else if (X < 0)
        {
            MoveLeft();
        }
        else
        {
            StopMoving();
        }
    }

    #region Collision Check
    //Ground check
    // Some problems: Because of the objects in the game that the player can jump on, the ground check has a tendency to set canjump to false afther colliding with these objects, even whilst colliding with the ground...

    //Old buggy collision check
    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "PhysObj")
        {
            canJump = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = false;
        }
    }*/

    #endregion


}