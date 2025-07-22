using UnityEngine;
using UnityEngine.SceneManagement;

public class DificultyLvManager : MonoBehaviour
{
    public int pointsToGain = 2;
    public int challengeLV = 1;

    public int GetPointsToGain()
    {
        return pointsToGain + challengeLV;
    }

    public void SetChallengeLevel(int level)
    {
        challengeLV = level;
    }
    public int GetChallengeLevel()
    {
        return challengeLV;
    }

    public void IncreaseChallengeLevel()
    {
        challengeLV = MapLevelCalculator.GetMapLevel(challengeLV, 1, 0.253f, 10f);
        PlayerPrefs.SetInt($"Dificuldade_{SceneManager.GetActiveScene().name}", challengeLV);
        PlayerPrefs.Save();
    }

}
