using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrTouchToDestroy : MonoBehaviour
{
    [HideInInspector]
    public bool isTouched;

    //Becomes false if this object is destroyed
    [HideInInspector]
    public bool isActive;
    private bool checkIfRespawn;
    private GameObject oGame;
    
    [SerializeField]
    [Header("Set health")]
    [Range(1, 50)]
    private int health = 5;
    private int currentHealth;

    [SerializeField]
    [Range(1, 5)]
    [Tooltip("How much damage this object takes when touched")]
    private int damageOnTouch = 1;

    [Tooltip("Assign the animator component on this object")]
    public Animator animator;
    [Tooltip("Assign the renderer component on this object")]
    public Renderer getRenderer;

    void Start()
    {
        currentHealth = health;
        isTouched = false;
        isActive = true;
        checkIfRespawn = false;
        oGame = GameObject.FindGameObjectWithTag("oGame");
    }

    void Update()
    {
        checkIfRespawn = oGame.GetComponent<scrSpores>().resetItems;

        if (checkIfRespawn == true)
        {
            isActive = true;
            //Reset the health
            currentHealth = health;
        }
        //Check if we are touching the object, and it is visible in the game
        if (isTouched == true && isActive == true)
        {
            TakeDamage(damageOnTouch);
            isTouched = false;
        }
        if (isActive)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void TakeDamage(int damageTaken)
    {
        //Reduce health
        currentHealth -= damageTaken;
        //Animate
        StartCoroutine(onTakeDamge());
        //Kill object if health is 0
        if (currentHealth <= 0)
        {
            DestroyObject();
        }

    }
    private void DestroyObject()
    {
        //Animate


        //print("I am dead");
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        isActive = false;
    }
    IEnumerator onTakeDamge()
    {
        getRenderer.material.SetColor("_Color", Color.red);
        //animator.SetBool("isTouched", true);
        yield return new WaitForSeconds(0.1f);
        //animator.SetBool("isTouched", false);
        getRenderer.material.SetColor("_Color", Color.white);
    }
}
