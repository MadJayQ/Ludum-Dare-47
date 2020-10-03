using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockScript : MonoBehaviour
{
    bool HasRock = false;
    bool IsHolding = false;
    Player player = GameObject.Find("Player"); // get the player gameobject so we can drop the rock whereever the player is standing


    private void Update()
    {
        
        if (HasRock == false) //check if the player doesn't have the rock
        {

            if (Input.GetKeyDown(KeyCode.E)) // range check also goes here after the keycode lets player pick up the rock if they dont already have it
            {
                PickUp();
                HasRock = true;
            }
            else 
            {
                return;
            }

        }

        if (HasRock == true && IsHolding == true) 
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) 
            {
                DropRock();
            }
        }
    
    
    }  
    
    private void PickUp() 
    {

    }

    private void DropRock()
    {

    }
}


