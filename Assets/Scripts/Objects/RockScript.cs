using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockScript : MonoBehaviour
{
     
    //When the mouse hovers over the GameObject, it turns to this color (red)
    Color m_MouseOverColor = Color.red;

    //This stores the GameObject’s original color
    Color m_OriginalColor;

    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    MeshRenderer m_Renderer;

    public GameObject player; // player game object goes here
    public GameObject rock;
    bool overRock;
    bool playerOnRock;
    Transform rockPoint;
    float rockX;
    float rockZ;
    bool checkX;
    bool checkZ;
    bool hasRock;


    void Start()
    {
        //Fetch the mesh renderer component from the GameObject
        m_Renderer = GetComponent<MeshRenderer>();
        //Fetch the original color of the GameObject
        m_OriginalColor = m_Renderer.material.color;
    }

    private void Update()
    {
        RockMath();
        rockPoint = this.transform;
        rockX = rockPoint.transform.position.x;
        rockZ = rockPoint.transform.position.z;


        if(overRock == true && checkX == true && checkZ == true)
        {
            if (Input.GetKeyDown(KeyCode.E) == true)
            {
                hasRock = true;
                Debug.Log("Getting the Rock man!");
            }
        }

        if(hasRock == true)
        {
            rock.transform.position = player.transform.position;
            
            if (Input.GetKeyDown(KeyCode.Mouse0) == true)
            {
                hasRock = false;
            }
                
        }
        
    }


    void OnMouseOver()
    {
        // Change the color of the GameObject to red when the mouse is over GameObject
        m_Renderer.material.color = m_MouseOverColor;
        Debug.Log("Mouse is over GameObject." + gameObject.name);
        if(gameObject.name == "Rock1" || gameObject.name == "Rock2") 
        { 
            overRock = true;
            if(overRock==true)
                Debug.Log("Over Rock should be true");
        }
    }

    void OnMouseExit()
    {
        // Reset the color of the GameObject back to normal
        m_Renderer.material.color = m_OriginalColor;
        Debug.Log("Mouse is not over a GameObject.");
        overRock = false;
    }

    void RockMath()
    {
        float rockMathX = rockX - player.transform.position.x;
        float rockMathZ = rockZ - player.transform.position.z;

        if(rockMathX >= -2 && rockMathX <= 2)
        {
            checkX = true;
            Debug.Log("CheckX is true");
        }
        else
        {
            checkX = false;
        }

        if(rockMathZ >= -2 && rockMathZ <= 2)
        {
            checkZ = true;
            Debug.Log("CheckZ is true");
        }
        else
        {
            checkX = false;
        }
    }
    
    

     
}


