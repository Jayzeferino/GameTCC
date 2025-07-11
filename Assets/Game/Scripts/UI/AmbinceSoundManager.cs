using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbinceSoundManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip[] ambienceSounds;
    public AudioClip lastAmbienceSoundPlayed;
    public List<AudioClip> potentialsAmbienceSounds;
    private AudioSource audioSource;


    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void LateUpdate()
    {
        if (!audioSource.isPlaying)
        {
            PlayAmbienceSound();
        }
    }

    public void stopAllSounds()
    {
        audioSource.Stop();
    }

    public virtual void PlayAmbienceSound()
    {
        potentialsAmbienceSounds = new List<AudioClip>();

        foreach (var ambienceSound in ambienceSounds)
        {
            if (lastAmbienceSoundPlayed != ambienceSound)
            {
                potentialsAmbienceSounds.Add(ambienceSound);
            }
        }

        int randomIndex = Random.Range(0, potentialsAmbienceSounds.Count);
        lastAmbienceSoundPlayed = potentialsAmbienceSounds[randomIndex];

        PlaySound(lastAmbienceSoundPlayed);
    }

    public virtual void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, 0.9f);
    }
}
