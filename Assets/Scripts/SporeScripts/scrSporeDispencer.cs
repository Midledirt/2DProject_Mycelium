using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrSporeDispencer : MonoBehaviour
{
    //Call this from the timer script. If this is false, stop the timer.
    public bool dispencingSpore = false;

    //Make multiple spore dispensers per level work, and have it so that for each occluded spore dispencer, the spore dispencerscript slows down:
    //Make a list of total amount of spore dispencers.
    //When occluded, subtract 1 from that list
    //Have the proggression of spore be "the current number"/sporeDispencers * (active)sporeDispencers. IF all are active (lets say there are 4) the math will be
    //(current number / 4) * 4. If two are occluded, it will be (current number /4) * 2.

    [SerializeField]
    [Tooltip("This object should be the sporeDispencer itself")]
    private Transform cloggObject;

    [Range(0.1f, 2f)]
    [Tooltip("How large the collision detection circle is")]
    public float circleRadius = 0.1f;

    [Tooltip("What layer to check for collisions on")]
    public LayerMask cloggObjectLayer;

    [Tooltip("Should containt the part of this prefab that has the animator on it")]
    public Animator sporeanimation;

    public void Start()
    {
        //This should be set to true at the start of the level
        dispencingSpore = true;
    }

    #region Bad old collision detection
    //Old collision detection, glitchy and bad.
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        //This should make sure that the spore dispencer will dispence spore whenever not clogged up.

        //CURRENT BUG, IF THE PLAYER COLLIDES WITH THIS OBJECT AFTER THE CLOG UP HAS HAPPENED, THE CODE WILL START DITECTING THE PLAYER IN STEAD, CAUSING THIS 2 STOP WORKING
        //BECAUSE THE PLAYER DOES NOT HAVE THE CLOG UP TAG. POTENTIAL SOLUTIONS:
        //Change the detection logic
        //Utilize physics2D.overlapcircle, to specificly check for stuff a specified layer. Seems to be way better than the overuse of tags...
        if (collision.tag == "DispenserClogUp")
        {
            dispencingSpore = false;
        }
        else
        {
            dispencingSpore = true;
        }
    }*/
    #endregion

    //New awesome collision detection:
    private void FixedUpdate()
    {
        //Fixed update for more consistent detection
        if (Physics2D.OverlapCircle(cloggObject.position, circleRadius, cloggObjectLayer, -Mathf.Infinity, Mathf.Infinity))
        {
            //This is where we might deactivate animations or particle effects
            dispencingSpore = false;
            //Animate
            if (sporeanimation != null)
            {
                sporeanimation.SetBool("DispencingSpore", false);
            }
        }
        else
        {
            //This is where we might activate animations or particle effects
            dispencingSpore = true;
            //Animate
            if (sporeanimation != null)
            {
                sporeanimation.SetBool("DispencingSpore", true);
            }
        }
    }



}
