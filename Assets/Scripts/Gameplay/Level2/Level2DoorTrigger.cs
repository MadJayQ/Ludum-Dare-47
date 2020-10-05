using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2DoorTrigger : MonoBehaviour
{
    bool playerIn;
    public GameObject DoorLeft;
    public GameObject DoorRight;
    Animator DoorLeftAnim;
    Animator DoorRightAnim;

    private void Start()
    {
        DoorLeftAnim = DoorLeft.GetComponent<Animator>();
        DoorRightAnim = DoorRight.GetComponent<Animator>();
    }

    private void Update()
    {
        if(playerIn == true && Input.GetKeyDown(KeyCode.E) == true)
        {
            DoorLeftAnim.Play("openDoorLeft");
            DoorRightAnim.Play("openDoor");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerIn = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        playerIn = false;
    }
}
