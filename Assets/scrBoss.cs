using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scrBoss : MonoBehaviour
{
    public bool isTouched;
    [SerializeField]
    [Range(5, 50)]
    private int bossHp;
    //How much damage the boss takes when touched
    private int damageTakenWhenTouched = 1;

    private bool invulnerable;

    private bool dead;

    [Tooltip("Slot the spore dispenser attached to the boss into this slot")]
    public GameObject bossDispenser;

    public Animator BossAnimator;

    public Renderer BossRenderer;

    private void Start()
    {
        invulnerable = true;
        dead = false;
    }
    void Update()
    {
        invulnerable = bossDispenser.GetComponent<scrSporeDispencer>().dispencingSpore;
        if (isTouched == true && invulnerable == false && dead == false)
        {
            StartCoroutine(damageEffect());
            //print("Ouch, that hurt!");
            takeDamage(damageTakenWhenTouched);
            //Play animation
            BossAnimator.SetTrigger("BossDamage");
            //Reset touch to false
            isTouched = false;
        }
        else if (isTouched == true && invulnerable == true && dead == false)
        {
            StartCoroutine(noDamageEffect());
            //print("That did not hurt one bit!");
            //Play animation
            BossAnimator.SetTrigger("BossNoDamage");
            //Reset touch to false
            isTouched = false;
        }

        if (bossHp <= 0)
        {
            dead = true;
            StartCoroutine(victoryEvent());
        }
    }
    private void takeDamage(int damageTaken)
    {
        //Make the boss take damage
        bossHp -= damageTaken;
    }
    IEnumerator victoryEvent()
    {
        //Play some death animation, sound or anything
        //print("I am dead!");
        BossAnimator.SetBool("BossDead", true);

        //Wait
        yield return new WaitForSeconds(1.1f);
        //Fade to white
        yield return new WaitForSeconds(2);
        //Load victory screen
        SceneManager.LoadScene("VictoryScreen");
    }
    IEnumerator damageEffect()
    {
        BossRenderer.material.SetColor("_Color", Color.red);
        yield return new WaitForSeconds(0.1f);
        BossRenderer.material.SetColor("_Color", Color.white);
    }
    IEnumerator noDamageEffect()
    {
        BossRenderer.material.SetColor("_Color", Color.green);
        yield return new WaitForSeconds(0.1f);
        BossRenderer.material.SetColor("_Color", Color.white);
    }
}
