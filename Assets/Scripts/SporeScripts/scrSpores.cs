using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scrSpores : MonoBehaviour
{
    //Min ide: Ha en usynlig sprite på UIen, som representerer sporer. Når sporene "spawner" øker alphaen, helt frem til skjermen er dekket. Da får spilleren game over.
    //++: Endre musikk og/eller lydeffekter når sporene spawner
    //++: Gjør at spillerens maks hastighet synker etterhvert som sporene blir tykkere
    [Tooltip("The respawnPos`s Z MUST be set to 0")]
    public Transform respawnPos;
    private GameObject thePlayer;

    public Image spores;
    public Color myColor;
    public bool canSpawnSpores = false;

    //How occluded the sceen will be by the spore sprite before a reset
    private float deadlySporeConsentration = 0.95f;

    [Range(0.05f, 0.1f)]
    public float sporeOcclusionSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //spores = GetComponent<Image>();
        myColor.a = 0.0f;
        //Find the player
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        //Get the current timer (for spawning spores)
    }

    // Update is called once per frame
    void Update()
    {
        //make the spawnSpores bool true if it is true in the scrLevelTimer script. 
        canSpawnSpores = GetComponent<scrLevelTimer>().spawnSpores;
        

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
        }
        
    }

    private void Respawn()
    {
        //Reset the timer.
        FindObjectOfType<scrLevelTimer>().currentTime = 0f;
        //Stop new spores
        FindObjectOfType<scrLevelTimer>().spawnSpores = false;

        //Move the player back to respawn position
        thePlayer.transform.position = respawnPos.position;

        //Reset the spore alpha
        myColor.a = 0.0f;
    }
}
