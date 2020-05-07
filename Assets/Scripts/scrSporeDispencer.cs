using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrSporeDispencer : MonoBehaviour
{
    //Call this from the timer script. If this is false, stop the timer.
    public bool dispencingSpore = false;

    public void Start()
    {
        //This should be set to true at the start of the level
        dispencingSpore = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
    }



}
