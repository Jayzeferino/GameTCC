using System;
using UnityEngine;

public class PlayerStaminaManager : MonoBehaviour, ITimeTracker
{
    public float maxStamina;
    public float currentStamina;
    private PlayerStatsManager playerStatsManager;
    public float staminaDurationInSeconds;
    private float regenRatePerSecond;
    public GameTimestamp InGameTimestamp;

    private void Awake()
    {
        playerStatsManager = GetComponent<PlayerStatsManager>();
    }

    void Start()
    {
        TimeManager.Instance.RegisterTracker(this);

        maxStamina = playerStatsManager.maxStamina;

        regenRatePerSecond = maxStamina / staminaDurationInSeconds;

        currentStamina = playerStatsManager.currentStamina;

        // Verifica se há data de saída salva
        if (PlayerPrefs.HasKey("last_Logout") && PlayerPrefs.HasKey("current_stamina"))
        {
            currentStamina = PlayerPrefs.GetFloat("current_stamina");
            string lastLogoutStr = PlayerPrefs.GetString("last_Logout");
            DateTime lastLogout = DateTime.Parse(lastLogoutStr);
            TimeSpan timeAway = DateTime.Now - lastLogout;

            float secondsAway = (float)timeAway.TotalSeconds;
            float staminaToRegen = regenRatePerSecond * secondsAway;

            currentStamina += staminaToRegen;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
            playerStatsManager.currentStamina = (int)currentStamina;
            Debug.Log($"lastLogout: {lastLogoutStr}| currentStamina: {currentStamina}, staminaToRegen: {staminaToRegen}");
        }

        InGameTimestamp.StartClock();
    }

    private void OnApplicationQuit()
    {

        // Debug.Log("O jogo está sendo fechado. Salvando dados...");
        SaveStaminaData();
    }

    private void OnApplicationPause(bool pause)
    {
        // Debug.Log("O jogo foi pausado. Salvando dados...");
        if (pause)
        {
            SaveStaminaData();
        }
    }

    void SaveStaminaData()
    {
        PlayerPrefs.SetString("last_Logout", DateTime.Now.ToString());
        PlayerPrefs.SetFloat("current_stamina", currentStamina);
        PlayerPrefs.Save();
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        float timeElapsed = GameTimestamp.CompareTimestampInSeconds(InGameTimestamp, timestamp);

        // Debug.Log($"TimeElapsed: {timeElapsed}| currentStamina: {currentStamina}");
        if (timeElapsed > 1.0f)
        {
            currentStamina -= timeElapsed * regenRatePerSecond;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
            playerStatsManager.currentStamina = (int)currentStamina;
            InGameTimestamp.realElapsedTime = timestamp.realElapsedTime;
        }

        if (currentStamina == 0)
        {
            //Mandar pro menu
        }
    }


}