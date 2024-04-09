using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : BYSingletonMono<SoundManager>
{
    public AudioSource audioFx;
    public AudioClip button_clip;
    private void OnValidate()
    {
        if(audioFx == null)
        {
            audioFx = gameObject.AddComponent<AudioSource>();
            audioFx.playOnAwake = false;
        }
          
        
    }
    public void OnPlayButtonSound()
    {
        audioFx.PlayOneShot(button_clip);
    }
}
