using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField]
    private AudioClip gunshot;
    AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();

        if(audio == null)
        {
            Debug.LogError("Audio is null on " + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object as entered collider" + other.gameObject.name);
        Administrator.Instance.RespawnPlayer(); // respawn player when he is seen by guard
        audio.PlayOneShot(gunshot);
        
    }
}
