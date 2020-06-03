using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrDeathCollision : MonoBehaviour
{
    private GameObject thePlayer;

    //public GameObject respawnPos;

    [SerializeField]
    [Range(0.5f, 1f)]
    private float collisionCircle = 0.5f;

    public LayerMask playerLayer;

    [Header("Spore acelleration")]
    [Tooltip("This decides how quickly the timer will acellerate when the player stands in a spore cloud")]
    [Range(0.5f, 10f)]
    public float SporeCloudEffect;

    private bool thisIsActive;


    private void Start()
    {
        //thePlayer = GameObject.FindGameObjectWithTag("Player");

        //respawnPos = GameObject.FindGameObjectWithTag("RespawnPos");
    }

    private void Update()
    {
        //Check if this object "exists"
        thisIsActive = this.GetComponent<scrTouchToDestroy>().isActive;

        if (Physics2D.OverlapCircle(transform.position, collisionCircle, playerLayer, -Mathf.Infinity, Mathf.Infinity) && thisIsActive == true)
        {
            axcellerateSporeOcclusion();
            //print("Player is touching spore cloud");
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Run the death function
            //playerDeath();
            axcellerateSporeOcclusion();
            print("Player is touching spore cloud");
        }
    }*/


    private void axcellerateSporeOcclusion()
    {
        FindObjectOfType<scrLevelTimer>().currentTime += (SporeCloudEffect * Time.deltaTime);
    }


    /*private void playerDeath()
    {
        //Reset the timer.
        FindObjectOfType<scrLevelTimer>().currentTime = 0f;
        //Stop new spores
        FindObjectOfType<scrLevelTimer>().spawnSpores = false;

        //Move the player back to respawn position
        thePlayer.transform.position = respawnPos.transform.position;

        //Reset the spore alpha (Just in case)
        FindObjectOfType<scrSpores>().myColor.a = 0.0f;
    }*/
}
