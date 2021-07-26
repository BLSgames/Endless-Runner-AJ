using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;
    public static AudioManager instance;
    void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start() {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sounds => sounds.name == name);   
        if(s == null)
            return;
        
        s.source.Play(); 
    }
    public void BgSoundPause()
    {
        Sounds s = Array.Find(sounds, sounds => sounds.name == "Theme");   
        if(s == null)
            return;
        
        s.source.Pause(); 
    }
}
