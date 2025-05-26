
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{

    public static PlayerStatsManager Instance { get; set; }
    public string characterName;

    [Header("Stamina")]
    public float maxStamina;
    public float currentStamina;

    [Header("SkillLevels")]
    public float mathLv;
    public float portLv;

    [Header("WorldCoordinates")]
    public float xPosition;
    public float yPosition;
    public float zPosition;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMathLv()
    {
        mathLv += 1 * 0.5f;

    }

    public void SetPortLv()
    {
        portLv += 1 * 0.5f;

    }
}
