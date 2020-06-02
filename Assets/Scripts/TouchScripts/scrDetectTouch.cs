using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrDetectTouch : MonoBehaviour
{
    Vector3 touchPositionWorld;

    //For now, lets save this gameobject as a public one, perhaps I can make other scripts on spcific gameobjects reference this.
    public GameObject touchedObject;

    //Edit this touchphase to alter which touchphase is used for the check
    TouchPhase editableTouchphase = TouchPhase.Ended;

    void Update()
    {
        #region Check the first touch for collision
        //Check for finger touches, take the first touch (GetTouch(0))and check its status.
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == editableTouchphase)
        {
            //We get the position of the first touch in "world position"...
            touchPositionWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            //And store it as a vector2!
            Vector2 touchPositionWOrld2D = new Vector2(touchPositionWorld.x, touchPositionWorld.y);

            //Cast a raycast from the camera to the position AND stores it in the datatype RaycastHit2D.
            //RaycastHit2D hitInformation = Physics2D.Raycast(touchPositionWOrld2D, Camera.main.transform.forward);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(touchPositionWOrld2D, .1f);

            foreach (var item in colliders)
            {
                //Grunnen til at dette ikke funker er at mitt touch script trenger å treffe et object med en COLLIDER (ex box collider) for at dette skal funke.
                //Dersom jeg fester en collider til UIen, vil collideren kun være der UIen er i scenen. Den vil ikke flytte seg med kamera...

                //YES, NÅ FUNKER DET. ADDET ET OBJECT 
                if (item.tag == "UIBar")
                {
                    //print("Touched the canvas");
                    return;
                }
            }

            foreach (var col in colliders)
            {

                //If we hit something, store information of the gameobject into the variable "touchedObject".
                touchedObject = col.gameObject;
                //print(touchedObject.name + " touched");

                //Prints the name of the object touched
                //print("Touched" + touchedObject.transform.name);

                //Make the touched object respond
                //Need 2 find a way too get the specific ID! (Perhaps not necessary?)
                if (touchedObject.tag == "Touchable")
                {
                    touchedObject.GetComponent<scrWhenTouched>().isTouched = true;
                }
                else if (touchedObject.tag == "Platform")
                {
                    touchedObject.GetComponent<scrActivatePlatform>().isTouched = true;
                }
                else if (touchedObject.tag == "Pickup")
                {
                    touchedObject.GetComponent<scrText>().isTouched = true;
                }
                else
                {
                    return;
                }

            }

        }
        #endregion
        #region Check the second touch for collision
        //Check for finger touches, take the first touch (GetTouch(0))and check its status.
        if (Input.touchCount > 1 && Input.GetTouch(1).phase == editableTouchphase)
        {
            //We get the position of the first touch in "world position"...
            touchPositionWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(1).position);

            //And store it as a vector2!
            Vector2 touchPositionWOrld2D = new Vector2(touchPositionWorld.x, touchPositionWorld.y);

            //Cast a raycast from the camera to the position AND stores it in the datatype RaycastHit2D.
            //RaycastHit2D hitInformation = Physics2D.Raycast(touchPositionWOrld2D, Camera.main.transform.forward);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(touchPositionWOrld2D, .1f);

            foreach (var item in colliders)
            {
                //Grunnen til at dette ikke funker er at mitt touch script trenger å treffe et object med en COLLIDER (ex box collider) for at dette skal funke.
                //Dersom jeg fester en collider til UIen, vil collideren kun være der UIen er i scenen. Den vil ikke flytte seg med kamera...

                //YES, NÅ FUNKER DET. ADDET ET OBJECT 
                if (item.tag == "UIBar")
                {
                    //print("Touched the canvas");
                    return;
                }
            }

            foreach (var col in colliders)
            {

                //If we hit something, store information of the gameobject into the variable "touchedObject".
                touchedObject = col.gameObject;
                //print(touchedObject.name + " touched");

                //Prints the name of the object touched
                //print("Touched" + touchedObject.transform.name);

                //Make the touched object respond
                //Need 2 find a way too get the specific ID! (Perhaps not necessary?)
                if (touchedObject.tag == "Touchable")
                {
                    touchedObject.GetComponent<scrWhenTouched>().isTouched = true;
                }
                else if (touchedObject.tag == "Platform")
                {
                    touchedObject.GetComponent<scrActivatePlatform>().isTouched = true;
                }
                else if (touchedObject.tag == "Pickup")
                {
                    touchedObject.GetComponent<scrText>().isTouched = true;
                }
                else
                {
                    return;
                }

            }

        }
        #endregion

    }

} 
