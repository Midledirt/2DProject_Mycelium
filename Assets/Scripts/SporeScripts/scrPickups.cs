using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPickups : MonoBehaviour
{
    [Tooltip("Find and drag the player in the scene into this slot, if missing.")]
    public GameObject thePlayer;
    public GameObject pickup;
    [Tooltip("The object with the timer script is oGame, drag it into this slot")]
    public GameObject timer;
    [Tooltip("How much time you get back when you pick up the pickup")]
    [Range(1f, 20f)]
    public float secondsGained;

    //Bools for turning the pickup on or off
    [HideInInspector]
    public bool isactive = true;
    private bool checkIfRespawn;
    private GameObject oGame;
   

    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 travelDistance = new Vector3(0, 1, 0);
    private float speed = 1.0f;

    private void Start()
    {
        pos1 = transform.position;
        pos2 = transform.position + travelDistance;
        isactive = true;
        checkIfRespawn = false;
        //Find the specific instance of oGame in order to get its scrSpores.
        oGame = GameObject.FindGameObjectWithTag("oGame");
    }

    private void Update()
    {
        //Move items
        transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);

        //check for the resetItems bool
        checkIfRespawn = oGame.GetComponent<scrSpores>().resetItems;

        if (checkIfRespawn == true)
        {
            //Set isactive to true if we are respawning
            isactive = true;
        }

        if (isactive == true)
        {
            //Have the sprite renderer turned on
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MyPlayer" && isactive == true)
        {
            destroyPickup();
        }
    }

    public void destroyPickup()
    {
        //Turn off the sprite renderer
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //print("hit the player");
        timer.GetComponent<scrLevelTimer>().currentTime -= secondsGained;

        //Play sound

        //PLay awesome animation

        //Turn pickup off
        isactive = false;
        //Object.Destroy(pickup);
    }
}
