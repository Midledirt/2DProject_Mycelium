using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrActivatePlatform : MonoBehaviour
{
    [Tooltip("Adjust this to change how long the platform stays active")]
    [Range(1f, 60f)]
    public float platformTimer = 1f;
    [HideInInspector]
    public bool isTouched;
    [Tooltip("This can be any cube with a 2D collider, as the mesh should be turned invisible.")]
    public GameObject platform;
    private GameObject instantiatedPlatform;
    [Tooltip("This is the point where the invisible platform will appear when the trigger is touched.")]
    public GameObject platformPlacemeent;

    //Animation
    public Animator MushroomAnimator;

    //Prevent the player from being able to que upp several mushroom activations
    private bool canBeActivated;

    private void OnDrawGizmos()
    {
        //Draw a gismos version of the platform for better illustration
        Gizmos.color = Color.red;
        Gizmos.DrawCube(platformPlacemeent.transform.position, platform.transform.localScale);
    }
    void Start()
    {
        canBeActivated = true;
        //platform.SetActive(true);
        isTouched = false;
        //platform.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouched == true && instantiatedPlatform == null && canBeActivated == true)
        {
            if (canBeActivated == false)
            {
                //Stop the coroutine from running twice
                return;
            }
            else 
            {
                //Prevent multiple activations before the start of the coroutine... Does not work properly. I think it is because (if you tap quickly), you can queue upp two
                //Platforms before the next frame, which is when the canBeActivated bool is set to false... 
                canBeActivated = false;
                //reset touch to false
                isTouched = false;

                //Make the touched animation start.
                if (MushroomAnimator != null)
                {
                    MushroomAnimator.SetBool("MushroomTouched", true);
                }
                StartCoroutine(tempPlatform());
                //print(transform.name + "is activated");
            }

        }
    }

   
    //Funker, men dersom knappen (for å spawne platformer) trykkes på flere ganger på en gang ser det ut til å que opp nye platformer etter hverandre. Kanskje dette er fordi IEnnumerators funker slik??
    IEnumerator tempPlatform()
    {
        if (instantiatedPlatform == null)
            {
            //Instantiate a temporary platform at selected location
        
            instantiatedPlatform = Instantiate(platform, platformPlacemeent.transform.position, Quaternion.identity);
        
            //platform.SetActive(true);

            yield return new WaitForSeconds(platformTimer);
            if (MushroomAnimator != null)
            {
                MushroomAnimator.SetBool("MushroomTouched", false);
            }
            if (platform != null)
            {
                Destroy(instantiatedPlatform);
                //platform.SetActive(false);
            }
        }
        else if (isTouched)
        {
            //StopCoroutine(tempPlatform());
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        canBeActivated = true;
    }
}
