using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrLoadLevelAfterScroller : MonoBehaviour
{
    [Tooltip("This number should be the same as the length of the scroller animation. It is 35 Seconds at the time of writing this")]
    [Range(30, 50)]
    public float scrollerTimer = 35f;

    public void Start()
    {
        //Start the scroller timer
        StartCoroutine(waitForScroller());
    }
    IEnumerator waitForScroller()
    {
        yield return new WaitForSeconds(scrollerTimer);
        //Load the start scene
        SceneManager.LoadScene("StartScene");
    }
}
