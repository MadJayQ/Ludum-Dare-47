using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public Transform target;
    public float VisionCone; // size that the cone should be for enemy

    // prints "close" if the z-axis of this transform looks
    // almost towards the target

    void Update()
    {
        Vector3 targetDir = target.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);
        

        if (angle < VisionCone) // if this inspector values aren't working might need to hard code the values
            print("Player seen"); // prints this to console when target is in "sight"
    }
}
