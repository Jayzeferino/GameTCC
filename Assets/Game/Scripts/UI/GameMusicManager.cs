using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicManager : MonoBehaviour, ITimeTracker
{
    [Header("Audio Clips")]
    public AudioClip[] backGroundMusics;
    public AudioClip[] miniGameBackGroundMusics;
    public AudioClip lastbackGroundMusicPlayed;
    public int atualHourMusic = 0;
    public List<AudioClip> potentialsbackGroundMusics;
    public string atualMapName = "";
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

    private void Update()
    {
        atualMapName = SceneManager.GetActiveScene().name;
        if (atualMapName == "MainMap")
        {
            if (!audioSource.isPlaying)
            {
                PlaybackGroundMusic(atualHourMusic);
            }

        }
    }

    public void PlayMainMapMusics()
    {
        PlaybackGroundMusic(atualHourMusic);
    }
    public void StopAllSounds()
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
        // Debug.Log($"atualHour: {atualHour} | atualHourMusic: {atualHourMusic}");
        if (atualHour > atualHourMusic)
        {
            atualHourMusic = (int)atualHour;
            if (atualMapName == "MainMap")
            {
                PlaybackGroundMusic(atualHourMusic - 1);
            }
        }

    }
}
