using System.Collections;
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
    [Tooltip("This should be set to a layer with the same name")]
    public LayerMask Ground;
    [Tooltip("This should be set to a layer with the same name")]
    public LayerMask StickyLayer;

    //For making the player a child of the platform
    [Tooltip("This should be set to the entire oPlayer object")]
    public GameObject player;
    [Tooltip("The object in this slot should be the Platform sprite itself, else it wont work properly")]
    public GameObject platform;
    public bool conectToPlatform;
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

    #region Animation vars
    //Turns true if we are in the air
    private bool isInAir;
    //Turns true if we are moving left or right
    private bool isMoving;
    [Tooltip("Drag the animator found on the player onto this")]
    public Animator playerAnimator;

    private bool jumpAnimReset = false;

    //Flipping the character
    private SpriteRenderer spriteRenderer;

    #endregion
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        //Set moving to false
        isInAir = false;
        isMoving = false;

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
        #region Animation
        //Update the animation bools
        if (isInAir == true)
        {
            playerAnimator.SetBool("isInAir", true);
        }
        else if (!isInAir)
        {
            playerAnimator.SetBool("isInAir", false);
        }

        if (isMoving == true)
        {
            playerAnimator.SetBool("isMoving", true);
        }
        else if (!isMoving)
        {
            playerAnimator.SetBool("isMoving", false);
        }

        #endregion
        HandleMoving();
        //This bool turns true whenever the player feet pos hits the ground
        canJump = Physics2D.OverlapCircle(feetPos.position, circleRadius, Ground, -Mathf.Infinity, Mathf.Infinity);
        //This bool turns true whenever the player feet pos hits objects that the player is supposed to follow.
        conectToPlatform = Physics2D.OverlapCircle(feetPos.position, circleRadius, StickyLayer, -Mathf.Infinity, Mathf.Infinity);

        if (conectToPlatform)
        {
            //Should set the transform parent of the player to the platform if hit. Hit should move the player with the platform
            player.transform.parent = platform.transform;
        }
        else if (conectToPlatform == false)
        {
            player.transform.parent = null;
        }
        //End the jump animation
        if (canJump == true && jumpAnimReset == true)
        {
            isInAir = false;
        }
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
                //Animation
                isMoving = true;
                //Flip character
                if (spriteRenderer != null)
                {
                    spriteRenderer.flipX = true;
                }
            }
            else if (!moveLeft)
            {
                MoveRight();
                //Animation
                isMoving = true;
                //Flip character
                if (spriteRenderer != null)
                {
                    spriteRenderer.flipX = false;
                }
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
        //Animation
        isMoving = false;
    }
    public void Jump()
    {
        if (canJump) //Can jump is the bool checking for collision
        {
            jumpAnimReset = false;
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
            //Animate
            isInAir = true;
            StartCoroutine(JumpCountdown());
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
    IEnumerator JumpCountdown()
    {
        yield return new WaitForSeconds(0.1f);
        jumpAnimReset = true;
    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "PlatformGlue")
        {
            print("Hit the platform");
        }
    }*/
}
