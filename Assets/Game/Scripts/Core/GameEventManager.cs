using System;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance;

    private void Awake()
    {
        instance = this;

    }

    public event Action<string, int> OnTermoButtonPressedHandler;
    public void TermoButtonPressed(string letter, int id)
    {
        OnTermoButtonPressedHandler?.Invoke(letter, id);
    }
    public event Action<int> OnNextFloorStepHandler;
    public void CurrentFloorId(int id)
    {
        OnNextFloorStepHandler?.Invoke(id);
    }
    public event Action<bool> OnFallOfBridgeHandler;
    public void SetFailInCalc(bool fail)
    {
        OnFallOfBridgeHandler?.Invoke(fail);
    }

    public event Action<bool, int> OnButtonChangeColorHandler;
    public void ChangedButtonColor(bool success, int id)
    {
        OnButtonChangeColorHandler?.Invoke(success, id);
    }

    public event Action<bool> OnEnterResetMathExpressionHandler;
    public void ResetMathExpressions(bool success)
    {
        OnEnterResetMathExpressionHandler?.Invoke(success);
    }

    public event Action<string, string> OnKickToGoalHandler;
    public void BallInGoal(string valor, string tag)
    {
        OnKickToGoalHandler?.Invoke(valor, tag);
    }



}