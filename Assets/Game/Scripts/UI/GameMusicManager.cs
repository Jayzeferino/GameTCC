using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicManager : MonoBehaviour, ITimeTracker
{
    [Header("Audio Clips")]
    public AudioClip[] backGroundMusics;
    public AudioClip[] miniGameBackGroundMusics;
    public AudioClip lastbackGroundMusicPlayed;
    public int atualHourMusic = 0;
    public List<AudioClip> potentialsbackGroundMusics;
    public string atualMapName = "MainMap";
    private AudioSource audioSource;
    public GameTimestamp InGameTimestamp;


    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void Start()
    {
        TimeManager.Instance.RegisterTracker(this);
    }

    private void LateUpdate()
    {
        if (atualMapName == "MainMap")
        {

            if (!audioSource.isPlaying)
            {
                PlaybackGroundMusic(atualHourMusic);
            }

        }
        else if (atualMapName == "PonteDoCalculo")
        {
            PlaybackMiniGameMusic(0); // Assuming 0 is the index for PonteDoCalculo music
        }
        else if (atualMapName == "Futematica")
        {
            PlaybackMiniGameMusic(1); // Assuming 1 is the index for Futematica music
        }
        else if (atualMapName == "PT1")
        {
            PlaybackMiniGameMusic(2); // Assuming 2 is the index for PonteDaFisica music
        }
        else if (atualMapName == "PT2")
        {
            PlaybackMiniGameMusic(3); // Assuming 3 is the index for PonteDaQuimica music
        }

    }

    public void stopAllSounds()
    {
        audioSource.Stop();
    }

    public virtual void PlaybackGroundMusic(int musicIndex)
    {

        potentialsbackGroundMusics = new List<AudioClip>();

        foreach (var backGroundMusic in backGroundMusics)
        {
            if (lastbackGroundMusicPlayed != backGroundMusic)
            {
                potentialsbackGroundMusics.Add(backGroundMusic);
            }
        }

        lastbackGroundMusicPlayed = potentialsbackGroundMusics[musicIndex];
        audioSource.clip = lastbackGroundMusicPlayed;
        audioSource.loop = true;
        audioSource.Play();
    }

    public virtual void PlaybackMiniGameMusic(int musicIndex)
    {
        lastbackGroundMusicPlayed = miniGameBackGroundMusics[musicIndex];
        audioSource.clip = lastbackGroundMusicPlayed;
        audioSource.loop = true;
        audioSource.Play();
    }

    public virtual void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, 0.9f);
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        float atualHour = TimeManager.Instance.gameTimestamp.hour;
        Debug.Log($"atualHour: {atualHour} | atualHourMusic: {atualHourMusic}");
        if (atualHour > atualHourMusic)
        {
            atualHourMusic = (int)atualHour;
            PlaybackGroundMusic(atualHourMusic - 1);
        }

    }
}
