using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrOcclusionRespawn : MonoBehaviour
{
    [Tooltip("This should contain the original startpoint object position")]
    public Transform originalSpawnPoint;

    private GameObject oGame;
    private bool checkIfRespawn;


    private void Start()
    {
        //Find the current instance of oGame
        oGame = GameObject.FindGameObjectWithTag("oGame");

        //Set respawn to false
        checkIfRespawn = false;
    }
    void Update()
    {
        //The reset items function is called when the game restarts, thus we know the game is restarting when we get this component
        checkIfRespawn = oGame.GetComponent<scrSpores>().resetItems;

        if (checkIfRespawn == true)
        {
            //Set this object back to its start position
            transform.position = originalSpawnPoint.position;
        }
    }
}
