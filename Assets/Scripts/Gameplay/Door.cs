using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int DoorIndex; //This is the level that the door belongs to, when the player completes a level an event is broadcasted containing which set of doors to open
    private void Awake()
    {
        Administrator.Instance.OnDoorOpen.AddListener(OnDoorOpenEvent);
    }

    private void OnDoorOpenEvent(int doorIndex)
    {
        if(doorIndex == DoorIndex)
        {
            OpenDoor();
            
        }
    }

    //TODO: After the player has reached the next room, close the doors (remove previous level geometry?)
    private void OpenDoor()
    {
        gameObject.SetActive(false);
        Administrator.Instance.OnDoorOpen.RemoveListener(OnDoorOpenEvent);
    }
}
