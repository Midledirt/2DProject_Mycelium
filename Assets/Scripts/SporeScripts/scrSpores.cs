using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scrSpores : MonoBehaviour
{
    [Tooltip("The respawnPos`s Z(axis) MUST be set to 0")]
    public Transform respawnPos;
    private GameObject thePlayer;

    //List for resetting items during respawn
    //GameObject[] PickupsInTheScene;
    [HideInInspector]
    public bool resetItems = false;

    public Image spores;
    [Tooltip("Add the UI image with the game over text into this slot")]
    public Image gameOverText;
    public Color gameOverTextColor;
    public Color myColor;
    public bool canSpawnSpores = false;
    //Decides how long the game over screen lasts
    private float gameOverScreenTimer = 2f;

    //How occluded the sceen will be by the spore sprite before a reset
    private float deadlySporeConsentration = 0.95f;

    [Tooltip("Decides how fast the screen is occluded")]
    [Range(0.1f, 0.8f)]
    public float sporeOcclusionSpeed = 0.5f;

    // Start is called before the first frame update
    private void Awake()
    {
        resetItems = false;
    }
    void Start()
    {
        //spores = GetComponent<Image>();
        myColor.a = 0.0f;
        gameOverTextColor.a = 0.0f;
        //Find the player
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        
        //Find all the pickups in the scene and store it in this list
        //PickupsInTheScene = GameObject.FindGameObjectsWithTag("Pickup");
    }

    // Update is called once per frame
    void Update()
    {
        //make the spawnSpores bool true if it is true in the scrLevelTimer script. 
        canSpawnSpores = GetComponent<scrLevelTimer>().spawnSpores;

        //Update the alpha of the images
        gameOverText.color = gameOverTextColor;
        spores.color = myColor;

        if (canSpawnSpores == true && myColor.a <= deadlySporeConsentration)
        {
            //myColor.a += Time.deltaTime * 100f;
            //print(myColor.a);
            myColor.a += Time.deltaTime * sporeOcclusionSpeed;
        }
        if (myColor.a >= deadlySporeConsentration)
        {
            //SceneManager.LoadScene("DeathScene");

            Respawn();
            return;
        }
        /*if (timeToResetItems == true)
        {
            //Reset items
            StartCoroutine(ResetTheItems());
            timeToResetItems = false;
        }*/
        
    }

    private void Respawn()
    {
        //Reset the timer.
        FindObjectOfType<scrLevelTimer>().currentTime = 0f;
        //Stop new spores
        FindObjectOfType<scrLevelTimer>().spawnSpores = false;

        //Respawn items
        /*foreach(GameObject @object in PickupsInTheScene)
        {
            @object.GetComponent<scrPickups>().isactive = true;
        }*/

        //Move the player back to respawn position
        thePlayer.transform.position = respawnPos.position;

        //Game over screen
        StartCoroutine(FadeFromBlack());
    }
    /*IEnumerator onDeath()
    {


        //yield return new WaitForSeconds(10);



        yield break;
    }*/
    /*IEnumerator ResetTheItems()
    {

    }*/
    IEnumerator FadeFromBlack()
    {
        //Add a game over text
        if (gameOverTextColor.a < 1)
        {
            gameOverTextColor.a += 0.1f;
        }

        yield return new WaitForSeconds(gameOverScreenTimer);

        resetItems = true;
        yield return new WaitForSeconds(0.1f);
        resetItems = false;

        //Remove the game over text
        gameOverTextColor.a = 0.0f;

        //Reset the spore alpha
        myColor.a = 0.0f;
    }
}
