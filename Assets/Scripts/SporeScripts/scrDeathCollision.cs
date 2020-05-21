using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrDeathCollision : MonoBehaviour
{
    private GameObject thePlayer;

    public GameObject respawnPos;


    private void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");

        respawnPos = GameObject.FindGameObjectWithTag("RespawnPos");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Run the death function
            playerDeath();
        }
    }


    private void playerDeath()
    {
        //Reset the timer.
        FindObjectOfType<scrLevelTimer>().currentTime = 0f;
        //Stop new spores
        FindObjectOfType<scrLevelTimer>().spawnSpores = false;

        //Move the player back to respawn position
        thePlayer.transform.position = respawnPos.transform.position;

        //Reset the spore alpha (Just in case)
        FindObjectOfType<scrSpores>().myColor.a = 0.0f;
    }
}
