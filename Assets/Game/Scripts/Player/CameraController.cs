using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CharacterMovement characterMovement;
    public Transform CameraTarget;
    public float TargetHeight = 1f;
    public float TargetDistance = 0.71f;

    public Vector2 XRotationRange = new Vector2(-70, 70);
    private Vector2 targetLook;

    public Quaternion LookRotation => CameraTarget.rotation;

    private void LateUpdate()
    {
        CameraTarget.transform.SetPositionAndRotation(characterMovement.transform.position + new Vector3(0, 1 * TargetHeight, 1), Quaternion.Euler(targetLook.x, targetLook.y, 0));
    }

    public void IncrementLookRotation(Vector2 lookdelta)
    {
        targetLook += lookdelta;
        targetLook.x = Mathf.Clamp(targetLook.x, XRotationRange.x, XRotationRange.y);
    }


}
