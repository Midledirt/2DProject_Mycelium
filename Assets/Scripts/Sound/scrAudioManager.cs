using UnityEngine.Audio;
using System;
using UnityEngine;

public class scrAudioManager : MonoBehaviour
{

    public scrSound[] sounds;
    // Start is called before the first frame update

    public static scrAudioManager instance;
    private void Awake()
    {
        //Make this object persistent
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (scrSound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        //Play main theme
        PlaySound("MenuTheme");
    }
    
    public void PlaySound(string name)
    {
        scrSound s = Array.Find(sounds, sound => sound.name == name);
        //For preventing the game from playing a sound that is spelled wrong
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }
}
