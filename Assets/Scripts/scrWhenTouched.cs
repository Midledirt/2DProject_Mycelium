using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrWhenTouched : MonoBehaviour
{
    public bool isTouched;
    private float colorSwapDuration;
    public float defaultDuration = 1f;
    Renderer getTheRenderer;
    public Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        colorSwapDuration = defaultDuration;
        isTouched = false;
        //Get the renderer of the specific object.
        getTheRenderer = transform.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouched)
        {
            if (myAnimator != null)
            {
                myAnimator.SetBool("isTouched", true);
            }
            
            colorSwapDuration = defaultDuration;

            
            //print(transform.name + "Says: That hurt!");

            //Reset isTouchedb back to false
            isTouched = false;
        }
        if (colorSwapDuration <= 0)
        {
            getTheRenderer.material.SetColor("_Color", Color.white);

            if (myAnimator != null)
            {
                myAnimator.SetBool("isTouched", false);
            }
        }
        else if (colorSwapDuration > 0)
        {
            getTheRenderer.material.SetColor("_Color", Color.red);
            colorSwapDuration -= Time.deltaTime;
        }
    }

}
