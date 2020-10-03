using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KinematicCharacterController;

public class CharacterController : BaseCharacterController
{
    public Vector3 Origin { get; private set; }
    public Quaternion Rotation { get; private set; }
    public Vector3 Velocity { get; private set; }

    public override void AfterCharacterUpdate(float deltaTime)
    {

    }

    public override void BeforeCharacterUpdate(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    public override bool IsColliderValidForCollisions(Collider coll)
    {
        throw new System.NotImplementedException();
    }

    public override void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        throw new System.NotImplementedException();
    }

    public override void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        throw new System.NotImplementedException();
    }

    public override void PostGroundingUpdate(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    public override void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
    {
        throw new System.NotImplementedException();
    }
    public void SetVelocity(Vector2 velocity)
    {
        Velocity = velocity;
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
    public void Teleport(Vector3? newOrigin = null, Quaternion? newRotation = null)
    {
        if(newOrigin != null && newOrigin.HasValue)
        {
            Origin = newOrigin.Value;
        }
        if(newRotation != null && newRotation.HasValue)
        {
            Rotation = newRotation.Value;
        }
    }

    public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        currentRotation = Rotation;
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        currentVelocity = Velocity;
    }
}
