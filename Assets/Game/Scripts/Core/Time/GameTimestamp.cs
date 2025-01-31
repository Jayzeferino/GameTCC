using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameTimestamp
{
    public DateTime gameStartTime;
    public DateTime realStartTime;
    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter

    }
    public enum Month
    {
        January = 1,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
    public int day;
    public int hour;
    public int minute;
    public string diaDaSemana;
    public Month month;
    public int year;
    public Season season;
    public DateTime currentGameTime;
    public string currentGameTimeString;
    public TimeSpan realElapsedTime;


    public void StartClock()
    {
        // Initialize both game and real-time clocks
        gameStartTime = DateTime.Now;
        realStartTime = DateTime.Now;
    }

    public void UpdateClock()
    {
        // Calculate the time elapsed in real-time
        realElapsedTime = DateTime.Now - realStartTime;
        // Update the game time accordingly
        currentGameTime = gameStartTime.Add(realElapsedTime);
        currentGameTimeString = currentGameTime.ToString("HH:mm:ss");
        ExtractInfoFromDate(currentGameTime);
    }

    public GameTimestamp GetGameTimestamp()
    {
        return this;
    }
    public void RestoreGameTime(string stringData)
    {
        gameStartTime = DateTime.Parse(stringData);
        realStartTime = DateTime.Now;
        UpdateClock();
    }

    public void ExtractInfoFromDate(DateTime agora)
    {
        // Extrai informações específicas
        minute = agora.Minute;
        year = agora.Year;
        month = (Month)agora.Month;
        day = agora.Day;
        hour = agora.Hour;
        diaDaSemana = agora.DayOfWeek.ToString(); // Nome do dia da semana (em inglês)
        ObterEstacao();
    }

    public void ObterEstacao()
    {
        if ((month == Month.December && day >= 21) || (month >= Month.January && month <= Month.February) || (month == Month.March && day < 21))
            season = Season.Summer;
        if ((month == Month.March && day >= 21) || (month >= Month.April && month <= Month.May) || (month == Month.June && day < 21))
            season = Season.Fall;
        if ((month == Month.June && day >= 21) || (month >= Month.July && month <= Month.August) || (month == Month.September && day < 23))
            season = Season.Winter;
        if ((month == Month.September && day >= 23) || (month >= Month.October && month <= Month.November) || (month == Month.December && day < 21))
            season = Season.Spring;
    }

    public static int HourToMinutes(int hour)
    {
        return hour * 60;

    }
    public static int DaysToHours(int days)
    {
        return days * 24;

    }
    public static int YearsToDays(int years)
    {
        return years * 4 * 30;

    }
    public static int CompareTimestampInHours(GameTimestamp time1, GameTimestamp time2)
    {

        TimeSpan timeDif = time1.realElapsedTime - time2.realElapsedTime;
        int difference = Math.Abs(timeDif.Hours);
        return difference;

    }
    public static int CompareTimestampInMinutes(GameTimestamp time1, GameTimestamp time2)
    {

        TimeSpan timeDif = time1.realElapsedTime - time2.realElapsedTime;
        int difference = Math.Abs(timeDif.Minutes);
        return difference;

    }
}
