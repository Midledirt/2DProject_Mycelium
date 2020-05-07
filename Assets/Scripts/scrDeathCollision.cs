using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrDeathCollision : MonoBehaviour
{
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
        //Do something more hre? Like play animation and wait for an IEnumerator or something...

        //print("YouShouldBeDead");
        //Load the death scene
        SceneManager.LoadScene("DeathScene");
    }
}
