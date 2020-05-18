using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scrSpores : MonoBehaviour
{
    //Min ide: Ha en usynlig sprite på UIen, som representerer sporer. Når sporene "spawner" øker alphaen, helt frem til skjermen er dekket. Da får spilleren game over.
    //++: Endre musikk og/eller lydeffekter når sporene spawner
    //++: Gjør at spillerens maks hastighet synker etterhvert som sporene blir tykkere


    public Image spores;
    public Color myColor;
    public bool canSpawnSpores = false;

    private float deadlySporeConsentration = 0.9f;

    [Range(0.05f, 0.1f)]
    public float sporeOcclusionSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //spores = GetComponent<Image>();
        myColor.a = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {
        //make the spawnSpores bool true if it is true in the scrLevelTimer script. 
        canSpawnSpores = GetComponent<scrLevelTimer>().spawnSpores;
        

        spores.color = myColor;
        if (canSpawnSpores == true && myColor.a <= deadlySporeConsentration)
        {
            //myColor.a += Time.deltaTime * 100f;
            //print(myColor.a);
            myColor.a += Time.deltaTime * sporeOcclusionSpeed;
        }
        if (myColor.a >= deadlySporeConsentration)
        {
            SceneManager.LoadScene("DeathScene");
        }
        
    }
}
