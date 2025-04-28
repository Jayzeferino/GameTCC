using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    public GameTimestamp gameTimestamp;

    public Material dia;
    public Material tarde;
    public Material noite;
    public Transform sunTransform;
    public float timeScale = 1.0f;

    List<ITimeTracker> listeners = new();

    private void Awake()
    {
        if (Instance != null && Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }
    void Start()
    {
        gameTimestamp = new GameTimestamp();
        gameTimestamp.StartClock();
        StartCoroutine(TimeUpdate());
    }


    IEnumerator TimeUpdate()
    {
        while (true)
        {
            Tick();
            yield return new WaitForSeconds(1 / timeScale);
        }

    }
    void Tick()
    {
        gameTimestamp.UpdateClock();
        foreach (ITimeTracker listener in listeners)
        {
            listener.ClockUpdate(gameTimestamp);
        }

        UpdateSunMovement();
    }

    private void UpdateSunMovement()
    {
        int timeInMinutes = GameTimestamp.HourToMinutes(gameTimestamp.hour) + gameTimestamp.minute;
        float sunAngle = .25f * timeInMinutes - 90;
        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
        MudarSkybox();
    }

    public void MudarSkybox()
    {

        if (gameTimestamp.hour > 7 && gameTimestamp.hour <= 17)
        {
            RenderSettings.skybox = dia;
        }

        if (gameTimestamp.hour > 17 && gameTimestamp.hour <= 19 || gameTimestamp.hour > 6 && gameTimestamp.hour <= 7)
        {
            RenderSettings.skybox = tarde;
        }

        if (gameTimestamp.hour > 19 && gameTimestamp.hour <= 6)
        {
            RenderSettings.skybox = noite;
        }

        DynamicGI.UpdateEnvironment();
    }

    public GameTimestamp GetGameTimestamp()
    {
        return gameTimestamp.GetGameTimestamp();
    }

    public void RegisterTracker(ITimeTracker listener)
    {

        listeners.Add(listener);
    }

    public void UnregisterTacker(ITimeTracker listener)
    {
        listeners.Remove(listener);
    }


}
