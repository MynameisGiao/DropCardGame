using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MusicType
{
    bg_1 =0,
    bg_2 =1
}
public class MusicManager : BYSingletonMono<MusicManager>
{
    public AudioSource audioFx;
   
    private void OnValidate()
    {
        if (audioFx == null)
        {
            audioFx = gameObject.AddComponent<AudioSource>();
            audioFx.playOnAwake = false;
        }


    }
    public void OnPlayMusic(MusicType musicType)
    {
        SmoothSound(audioFx, 1,musicType);
       
    }
    public void SmoothSound(AudioSource audioSource, float fadeTime, MusicType musicType)
    {
        StartCoroutine(FadeSoundOn(audioSource, fadeTime, musicType));
      
    }
    IEnumerator FadeSoundOn(AudioSource audioSource, float fadeTime, MusicType musicType)
    {
        yield return FadeSoundOff(audioSource, fadeTime);
        var audio = Resources.Load<AudioClip>($"Sound/{musicType.ToString()}");
        //audioFx.PlayDelayed(2);
        audioFx.loop = true;
        audioFx.clip = audio;
        audioFx.Play();

        var t = 0f;
        while (t < 1)
        {
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
            if (audioSource != null)
            {
                audioSource.volume = t;
            }
        }

       
    }
    IEnumerator FadeSoundOff(AudioSource audioSource, float fadeTime)
    {
        var t = fadeTime;
        while(t>0)
        {
            yield return new WaitForEndOfFrame();
            t -= Time.deltaTime;
            if(audioSource != null )
            {
                audioSource.volume = t;
            }
        }
    }
}
