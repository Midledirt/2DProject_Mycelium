using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPickups : MonoBehaviour
{
    [Tooltip("Find and drag the player in the scene into this slot, if missing")]
    public GameObject thePlayer;
    public GameObject pickup;
    [Tooltip("The object with the timer script is oGame, drag it into this slot")]
    public GameObject timer;
    [Tooltip("How much time you get back when you pick up the pickup")]
    [Range(1f, 20f)]
    public float timeGained;


    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 travelDistance = new Vector3(0, 1, 0);
    private float speed = 1.0f;

    private void Start()
    {
        pos1 = transform.position;
        pos2 = transform.position + travelDistance;
    }

    private void Update()
    {
        //This code is from this site https://answers.unity.com/questions/690884/how-to-move-an-object-along-x-axis-between-two-poi.html
        transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            destroyPickup();
        }
    }

    public void destroyPickup()
    {
        //print("hit the player");
        timer.GetComponent<scrLevelTimer>().currentTime -= timeGained;

        //Play sound

        //PLay awesome animation

        Object.Destroy(pickup);
    }
}
