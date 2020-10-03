using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public void TeleportPlayerToRespawn(Player player)
    {
        player.Controller.Teleport(transform.position, transform.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
        Vector3 forward = (transform.rotation * Vector3.forward).normalized;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, forward * 3);
    }
}
