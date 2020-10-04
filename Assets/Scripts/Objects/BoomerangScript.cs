using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class BoomerangScript : MonoBehaviour
{
    //When the mouse hovers over the GameObject, it turns to this color (red)
    Color m_MouseOverColor = Color.red;

    //This stores the GameObject’s original color
    Color m_OriginalColor;

    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    MeshRenderer m_Renderer;

    public GameObject boomerangWaypoint; // specific spot the boomerang will be sent
    public GameObject returnPoint; // the players Z position used to return boomerang
    public float boomerangSpeed; // speed of the boomerang
    bool hasBoomerang; // checked to see if the player and boomerang have the same position so that the player can use it
   




    void Update()
    {
        
        if (Input.GetKey(KeyCode.Mouse1) == true) // if the right mouse button is hit should throw the boomerang at the way point
        {
            
            ThrowBoomerang(); 
            UnityEngine.Debug.Log("Trying To Throw Boomerang, state of hasBoomerang " + hasBoomerang);
            hasBoomerang = false;
        }
        UnityEngine.Debug.Log("state of hasBoomerang " + hasBoomerang);
        
        if (this.transform.position.z == boomerangWaypoint.transform.position.z)
        {
            ReturnBoomerang();
            UnityEngine.Debug.Log("Trying to return the Boomerang");
        }

        if(this.transform.position.z == 0)
        {
            hasBoomerang = true; // makes sure the player has the boomerang
        }
        
    }


    void ThrowBoomerang()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, boomerangWaypoint.transform.position, boomerangSpeed * Time.deltaTime);
      
    }

    void ReturnBoomerang()
    {
        transform.position = Vector3.MoveTowards(transform.position, returnPoint.transform.position, boomerangSpeed * Time.deltaTime);
        
    }
}
