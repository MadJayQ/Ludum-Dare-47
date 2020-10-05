using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAudio : MonoBehaviour
{

    AudioSource audio;
    public AudioClip OpenDoor;
    bool playAudio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playAudio == true)
        {
            audio.PlayOneShot(OpenDoor);
            playAudio = false;
        }
    }

    public void SetAudio()
    {
        playAudio = true;
    }
}
