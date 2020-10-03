using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KinematicCharacterController;

public class MovementController : BaseMoverController
{
    public Vector3 Origin { get; private set; }
    public Quaternion Rotation { get; private set; }
    public Vector3 Velocity { get; private set; }

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
    /// <summary>
    /// Set MovementController's new velocity
    /// </summary>
    /// <param name="newVelocity"></param>
    public void SetVelocity(Vector3 newVelocity)
    {
        Velocity = newVelocity;
    }

    public override void UpdateMovement(out Vector3 goalPosition, out Quaternion goalRotation, float deltaTime)
    {
        throw new System.NotImplementedException();
    }
}
