
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance { get; set; }
    public string characterName;

    public double wallet;

    [Header("Stamina")]
    public float maxStamina;
    public float currentStamina;

    [Header("SkillLevels")]
    public float mathLv;
    public float portLv;
    public float mathPoints;
    public float portPoints;

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
        mathLv = LevelCalculator.GetMathLevel(mathPoints, math_initialLevel, math_coefficient, math_offset);

    }

    public void SetPortLv()
    {
        portLv = LevelCalculator.GetPTLevel(portPoints, pt_initialLevel, pt_coefficient, pt_offset);

    }

    [Header("Parâmetros de Nível de Math")]
    public int math_initialLevel = 1;
    public float math_coefficient = 0.5f;
    public float math_offset = 10f;

    [Header("Parâmetros de Nível de PT")]
    public int pt_initialLevel = 1;
    public float pt_coefficient = 0.5f;
    public float pt_offset = 10f;

    // Seu método para adicionar pontos de Math
    public void AddMathPoints(float pontos)
    {
        mathPoints += pontos;
        SetMathLv(); // Atualiza o nível de Math sempre que pontos são adicionados
    }

    // Seu método para adicionar pontos de PT
    public void AddPTPoints(float pontos)
    {
        portPoints += pontos;
        SetPortLv(); // Atualiza o nível de PT sempre que pontos são adicionados
    }

    // // Exemplo de como você chamaria isso de outro script ou de um botão na UI
    // void Update()
    // {
    //     // Apenas para teste, remova no jogo final
    //     if (Input.GetKeyDown(KeyCode.M))
    //     {
    //         AddMathPoints(5f); // Simula seu incremento original
    //     }
    //     if (Input.GetKeyDown(KeyCode.P))
    //     {
    //         AddPTPoints(5f); // Simula seu incremento original
    //     }
    // }
}

