using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KinematicCharacterController;
using UnityEngine.Events;

/// <summary>
/// This is the input layout that the character controller expects to be able to move
/// </summary>
public struct CharacterControllerInputs
{
    Vector3 MovementVector;
}

public class OnTriggerEnterEvent : UnityEvent<BaseTrigger> { }
public class OnTriggerExitEvent : UnityEvent<BaseTrigger> { }

public class CharacterController : BaseCharacterController
{
    public Vector3 Origin { get; private set; }
    public Quaternion Rotation { get; private set; }
    public Vector3 Velocity { get; private set; }
    public Vector3 MoveInputVector { get; set; }

    public OnTriggerEnterEvent TriggerEnterEvent = new OnTriggerEnterEvent();
    public OnTriggerExitEvent TriggerExitEvent = new OnTriggerExitEvent();

    [Header("Game Movement")]
    public float MaxMoveSpeedStable;
    public float MoveSpeedSharpnessStable;
    public Vector3 Gravity = new Vector3(0, 0, -9.8f);

    public override void AfterCharacterUpdate(float deltaTime)
    {
        Origin = Motor.TransientPosition;
    }

    public override void BeforeCharacterUpdate(float deltaTime)
    {

    }

    public override bool IsColliderValidForCollisions(Collider coll)
    {
        return true;
    }

    public override void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {

    }

    public override void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        BaseTrigger trigger = other.gameObject.GetComponent<BaseTrigger>();
        if(trigger != null)
        {
            TriggerEnterEvent.Invoke(trigger);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BaseTrigger trigger = other.gameObject.GetComponent<BaseTrigger>();
        if(trigger != null)
        {
            TriggerExitEvent.Invoke(trigger);
        }
    }

    public override void PostGroundingUpdate(float deltaTime)
    {

    }

    public override void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
    {

    }

    public void SetRotation(Quaternion rotation)
    {
        Rotation = rotation;
    }

    /// <summary>
    /// Teleports the MovementController to the new origin/rotation invalidating the interpolation record
    /// </summary>
    /// <param name="newOrigin"> Destination Absolute Rotation</param>
    /// <param name="newRotation">Destination Absolute Rotation</param>
    public void Teleport(Vector3? newOrigin = null, Quaternion? newRotation = null, bool ignoreInterpolation = true)
    {
        if(newOrigin != null && newOrigin.HasValue)
        {
            Origin = newOrigin.Value;
        }
        if(newRotation != null && newRotation.HasValue)
        {
            Rotation = newRotation.Value;
        }

        Motor.SetPositionAndRotation(Origin, Rotation, ignoreInterpolation);
    }

    public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        currentRotation = Rotation;
    }

    /// <summary>
    /// Execute a GroundMove tick
    /// </summary>
    /// <returns></returns>
    private void DoGroundMove(float dt)
    {
        Vector3 targetMovementVelocity = Vector3.zero;
        Vector3 effectiveGroundNormal = Motor.GroundingStatus.GroundNormal;
        if(Velocity.sqrMagnitude > 0 && Motor.GroundingStatus.SnappingPrevented)
        {
            Vector3 groundPointToCharacter = Motor.TransientPosition - Motor.GroundingStatus.GroundPoint;
            if(Vector3.Dot(Velocity, groundPointToCharacter) >= 0)
            {
                effectiveGroundNormal = Motor.GroundingStatus.OuterGroundNormal;
            }
            else
            {
                effectiveGroundNormal = Motor.GroundingStatus.InnerGroundNormal;
            }
        }

        Velocity = Motor.GetDirectionTangentToSurface(Velocity, effectiveGroundNormal) * Velocity.magnitude;
        Vector3 planarDirection = Vector3.ProjectOnPlane(MoveInputVector, Motor.CharacterUp).normalized;
        if(planarDirection.sqrMagnitude <= 0f)
        {
            planarDirection = Vector3.ProjectOnPlane(Rotation * Vector3.up, Motor.CharacterUp).normalized;
        }
        Vector3 inputRight = Vector3.Cross(planarDirection, Motor.CharacterUp);
        Vector3 reorientedInput = Vector3.Cross(effectiveGroundNormal, inputRight).normalized * MoveInputVector.magnitude;
        targetMovementVelocity = reorientedInput * MaxMoveSpeedStable;

        Velocity = Vector3.Lerp(Velocity, targetMovementVelocity, 1 - Mathf.Exp(-MoveSpeedSharpnessStable * dt));
    }
    /// <summary>
    /// Execute a AirMove tick
    /// </summary>
    /// <returns></returns>
    private void DoAirMove(float dt)
    {
        Velocity += Gravity * dt;
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        Velocity = Vector3.zero;
        if(Motor.GroundingStatus.IsStableOnGround)
        {
            DoGroundMove(deltaTime);
        }
        else
        {
            DoAirMove(deltaTime);
        }
        currentVelocity = Velocity;
    }

    public void LookAt(Vector3 position)
    {
        Vector3 direction = (position - Origin).normalized;
        Rotation = Quaternion.LookRotation(direction);
    }
}
