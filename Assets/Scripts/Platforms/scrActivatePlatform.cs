using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrActivatePlatform : MonoBehaviour
{
    [Range(1f, 10f)]
    public float platformTimer = 1f;
    public bool isTouched;
    [Tooltip("This can be any cube with a 2D collider, as the mesh should be turned invisible.")]
    public GameObject platform;
    private GameObject instantiatedPlatform;
    [Tooltip("This is the point where the invisible platform will appear when the trigger is touched.")]
    public GameObject platformPlacemeent;

    //Animation
    public Animator MushroomAnimator;
    // Start is called before the first frame update
    void Start()
    {
        //platform.SetActive(true);
        isTouched = false;
        //platform.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouched && instantiatedPlatform == null)
        {
            //Make the touched animation start.
            if (MushroomAnimator != null)
            {
                MushroomAnimator.SetBool("MushroomTouched", true);
            }
            StartCoroutine(tempPlatform());
            //print(transform.name + "is activated");
            //reset touch to false
            isTouched = false;
        }
        else if (isTouched && instantiatedPlatform != null)
        {
            return;
            //Destroy(instantiatedPlatform);
            //StopCoroutine(tempPlatform());
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
    }
}
