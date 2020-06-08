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

    //Animate death for objects
    public bool hasDeathAnimation = false;
    [SerializeField]
    [Tooltip("Set this to the actual length of the deathAnimation")]
    [Range(0.5f, 1f)]
    public float animationTimer = 0.90f;

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
        //print("I am dead");
        //Check if this object has a death animation
        if (hasDeathAnimation == false)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            isActive = false;
        }
        else if (hasDeathAnimation == true)
        {
            StartCoroutine(DeathAnimation());
        }

    }
    IEnumerator onTakeDamge()
    {
        getRenderer.material.SetColor("_Color", Color.red);
        if (animator != null && animator.isActiveAndEnabled)
        {
            animator.SetBool("isTouched", true);
        }
        yield return new WaitForSeconds(0.1f);
        if (animator != null && animator.isActiveAndEnabled)
        {
            animator.SetBool("isTouched", false);
        }
        getRenderer.material.SetColor("_Color", Color.white);
    }

    IEnumerator DeathAnimation()
    {
        if (hasDeathAnimation == false)
        {
            yield return null;
        }
        else if (animator != null)
        {
            //Animate
            //print("isDestroyedBegins");
            if (animator != null && animator.isActiveAndEnabled)
            {
                animator.SetBool("isDestroyed", true);
            }
            yield return new WaitForSeconds(animationTimer);
            //Destroy
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            isActive = false;
            if (animator != null && animator.isActiveAndEnabled)
            {
                animator.SetBool("isDestroyed", false);
            }
            //print("isDestroyedEnds");
        }

    }
}
