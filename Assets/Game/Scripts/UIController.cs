using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private InputActions inputActions;



    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Enable();
    }
}
