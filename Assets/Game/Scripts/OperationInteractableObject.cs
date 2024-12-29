using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationInteractableObject : MonoBehaviour
{
    [SerializeField] private char operation;

    public char GetOperation()
    {
        return operation;
    }
}