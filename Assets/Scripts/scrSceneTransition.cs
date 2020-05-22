using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrSceneTransition : MonoBehaviour
{
    [Tooltip("Adjust the size of the collision area")]
    [SerializeField]
    [Range(0.5f, 2f)]
    private float circleRadius = 0.5f;

    [Header("Choose what scene to move to.")]
    [Tooltip("This gets the build index ID of the scene we wish to load. Currently, scene 2 is intro, and scene 6 is boss")]
    [Range(2, 6)]
    public int theNextScene;

    [Tooltip("Set this to the layer named Player")]
    public LayerMask PlayerLayer;
    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, circleRadius, PlayerLayer, -Mathf.Infinity, Mathf.Infinity))
        {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        //print("Lets go to the next level");
        SceneManager.LoadScene(theNextScene);
    }
}
