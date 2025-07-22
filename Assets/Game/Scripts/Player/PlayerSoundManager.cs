using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip jumpSound;
    public AudioClip interactSound;
    public AudioClip actionSound;
    public AudioClip endWateringSound;
    public AudioClip[] stepsSounds;
    bool isPlayingActionSound = false;
    public AudioClip lastStepSoundPlayed;
    public List<AudioClip> potentialsStepsSounds;
    private AudioSource audioSource;
    private Animator animator;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    public void PlayJumpSound()
    {
        PlaySound(jumpSound);
    }
    public void PlayInteractSound()
    {
        PlaySound(interactSound);
    }

    public void StopActionSound()
    {
        if (audioSource.isPlaying && actionSound != null)
        {
            audioSource.Stop();
        }
    }

    public void PlayActionSound()
    {
        isPlayingActionSound = true;
        PlaySound(actionSound);
    }

    public void PlayEndWateringSound()
    {
        PlaySound(endWateringSound);
    }

    public void SetActionSound(AudioClip actionSound)
    {
        this.actionSound = actionSound;
    }

    public void stopAllSounds()
    {
        audioSource.Stop();
    }

    public virtual void PlayStepSound()
    {

        potentialsStepsSounds = new List<AudioClip>();

        foreach (var stepsSound in stepsSounds)
        {
            if (lastStepSoundPlayed != stepsSound)
            {
                potentialsStepsSounds.Add(stepsSound);
            }
        }

        int randomIndex = Random.Range(0, potentialsStepsSounds.Count);
        lastStepSoundPlayed = potentialsStepsSounds[randomIndex];

        if (!animator.GetBool("isInteracting") || !animator.GetBool("isJumping"))
        {
            PlaySound(lastStepSoundPlayed);
        }

    }

    public virtual void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, 0.9f);
    }
}
