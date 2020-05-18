using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrLevelTimer : MonoBehaviour
{
    //Ide: Ha en timer representert som en liten loading bar. Når den er tum, spawner swarmen.
    [Range(3f, 300f)]
    public float levelTimer;
    public float currentTime;
    public bool spawnSpores = false;

    public GameObject sporeDispencer;
    public bool dispencingSpore;

    //This one requires the namespace UnityEngine.UI!
    [Tooltip("This slot requires the (spawn timer empty) found under canvas, UIbar")]
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    { 
        //Set the current time to zero at the start of the level
        currentTime = 0f;
        setLevelTimer(levelTimer);
    }

    private void Update()
    {
        //Now that we have pickups that subtrackts from the current time, lets make sure it cannot be set 2 anything less tan 0
        if (currentTime < 0)
        {
            currentTime = 0;
        }

        //IMPORTANT: LOOK AT THE FOLLOWING LINES, THE SECOND ONE IS IDENTICAL TO ONE THAT WORKS IN TWO OTHER SCRIPTS, HOWEVER IT DOES NOT WORK FOR THIS SCRIPT. I THINK THE REASON IS THAT WHENEVER YOU REFERENCE A SCRIPT THAT SITS ON A PHYSICAL OBJECT,
        //YOU NEED TO FIRST GET THE ID OF THE SPECIFIC OBJECT. BUT WHY? THE CODE IN THE SECOND LINE EXISTS ON UI ELEMENTS AND EMPTYS. SOOOOO WTF

        //GOT A THEORY: I THINK THAT IT NEEDS A SPECIFIED GAMEOBJECT BECAUSE THE BOOL IS USED TO CHECK FOR A COLLISION, THEREFORE IT NEEDS THE SPECIFIC COLLIDER OF AN OBJECT, CANNOT JUST BE ANY OBJECT IN THE SCENE WITH THAT SCRIPT
        dispencingSpore = sporeDispencer.GetComponent<scrSporeDispencer>().dispencingSpore;
        //dispencingSpore = GetComponent<scrSporeDispencer>().dispencingSpore;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (dispencingSpore == true)
        {
            if (currentTime <= levelTimer)
            {
                currentTime += Time.deltaTime;
            }
            else if (currentTime >= levelTimer)
            {
                currentTime = levelTimer;
                spawnSpores = true;
            }
        }
        setCurrentTimer(currentTime);

    }

    public void setLevelTimer(float levelTimer)
    {
        slider.maxValue = levelTimer;
        slider.value = 0f;
    }

    public void setCurrentTimer(float currentTime)
    {
        slider.value = currentTime;
    }

    IEnumerator increaseBar()
    {




        yield return null;
    }

}
