using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform PosA;
    [SerializeField]
    private Transform PosB;
    [SerializeField]
    public GameObject Platform;

    [Range(0.1f, 10f)]
    public float speed = 4f;

    [Tooltip("How long the platform waits at a location")]
    [Range(0.1f, 5f)]
    public float PlatformWaitDuration;

    private Vector3 nextPos;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(PosA.position, PosB.position);
    }

    private void Start()
    {
        nextPos = PosB.position;
    }
    private void Update()
    {
        movePlatform();

        //Change nextpos whenever the platform reaches the position
        if (Platform.transform.position == PosA.position)
        {
            StartCoroutine(returnTooB());
        }
        else if (Platform.transform.position == PosB.position)
        {
            StartCoroutine(returnTooA());
        }
    }

    private void movePlatform()
    {
        Platform.transform.position = Vector3.MoveTowards(Platform.transform.position, nextPos, speed * Time.deltaTime);
    }

    IEnumerator returnTooA()
    {
        yield return new WaitForSeconds(PlatformWaitDuration);
        nextPos = PosA.position;

        yield return null;
    }
    IEnumerator returnTooB()
    {
        yield return new WaitForSeconds(PlatformWaitDuration);
        nextPos = PosB.position;

        yield return null;
    }
}
