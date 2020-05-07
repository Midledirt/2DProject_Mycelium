using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrActivatePlatform : MonoBehaviour
{
    [Range(1f, 10f)]
    public float platformTimer = 1f;
    public bool isTouched;
    public GameObject platform;
    private GameObject instantiatedPlatform;
    public GameObject platformPlacemeent;
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
