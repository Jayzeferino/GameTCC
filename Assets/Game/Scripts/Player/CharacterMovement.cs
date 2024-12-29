using System.Collections;
using System.Collections.Generic;
using KinematicCharacterController;
using UnityEngine;
public struct CharacterMovementInput
{
    public Vector2 MoveInput;
    public Quaternion LookRotation;
    public bool WantsToJump;

}

[RequireComponent(typeof(KinematicCharacterMotor))]
public class CharacterMovement : MonoBehaviour, ICharacterController
{
    private Vector3 moveInput;
    public KinematicCharacterMotor Motor;
    [Header("Ground Movement")]
    public float MaxSpeed = 5;
    public float Acceleration = 30;
    public float RotationSpeed = 25;
    public float Gravity = 40;
    public float JumpHeight = 1.3f;
    public float JumpSpeed => Mathf.Sqrt(2 * Gravity * JumpHeight);
    [Range(0.01f, 0.3f)]
    public float JumpRequestDuration = 0.1f;
    [Header("Air Movement")]
    public float MaxAirSpeed = 2;
    public float AirAcceleration = 20;
    [Min(0)]
    public float Drag = 0.5f;
    public float JumpRequestExpireTime;

    private void Awake()
    {
        Motor.CharacterController = this;
    }

    public void SetInput(in CharacterMovementInput input)
    {
        moveInput = Vector3.zero;
        if (input.MoveInput != Vector2.zero)
        {
            moveInput = new Vector3(input.MoveInput.x, 0, input.MoveInput.y);
            moveInput = input.LookRotation * moveInput;
            moveInput.y = 0;
            moveInput.Normalize();
        }
        if (input.WantsToJump)
        {
            JumpRequestExpireTime = Time.time + JumpRequestDuration;
        }
    }

    public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {

        if (moveInput != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(moveInput);
            currentRotation = Quaternion.Slerp(currentRotation, targetRotation, RotationSpeed * deltaTime);
        }

    }

    public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {

        if (Motor.GroundingStatus.IsStableOnGround)
        {
            var targetVelocity = moveInput * MaxSpeed;
            currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, Acceleration * deltaTime);
            if (Time.time < JumpRequestExpireTime)
            {
                currentVelocity.y = JumpSpeed;
                JumpRequestExpireTime = 0;
                Motor.ForceUnground();
            }
        }
        else
        {
            var targetVelocityXZ = new Vector2(moveInput.x, moveInput.z) * MaxAirSpeed;
            var currentVelocityXZ = new Vector2(currentVelocity.x, currentVelocity.z);
            currentVelocityXZ = Vector2.MoveTowards(currentVelocityXZ, targetVelocityXZ, AirAcceleration * deltaTime);
            currentVelocity.x = ApplyDrag(currentVelocityXZ.x, Drag, deltaTime);
            currentVelocity.z = ApplyDrag(currentVelocityXZ.y, Drag, deltaTime);
            currentVelocity.y -= Gravity * deltaTime;
        }
    }
    public static float ApplyDrag(float v, float drag, float deltaTime)
    {
        return v * (1f / (1f + drag + deltaTime));

    }
    #region not implemented
    public bool IsColliderValidForCollisions(Collider coll)
    {
        return true;
    }
    public void AfterCharacterUpdate(float deltaTime)
    {
    }

    public void BeforeCharacterUpdate(float deltaTime)
    {
    }
    public void OnDiscreteCollisionDetected(Collider hitCollider)
    {
    }

    public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
    }

    public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
    }

    public void PostGroundingUpdate(float deltaTime)
    {
    }

    public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
    {
    }


    #endregion
}
