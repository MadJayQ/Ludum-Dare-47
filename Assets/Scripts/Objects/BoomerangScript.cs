using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class BoomerangScript : MonoBehaviour
{
    
    public GameObject boomerangWaypoint; // specific spot the boomerang will be sent
    
    public float boomerangSpeed; // speed of the boomerang
    public bool hasBoomerang; // checked to see if the player and boomerang have the same position so that the player can use it
    
    Animator animator;



    void Start()
    {
        hasBoomerang = true;
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        

        if (Input.GetKey(KeyCode.Mouse1) == true && hasBoomerang == true) // if the right mouse button is hit should throw the boomerang at the way point
        {
            hasBoomerang = false;
            animator.SetBool("RightMouse", true); 
            StartCoroutine(AttackTime());
            
        }
        
        
        
        if (this.transform.position.z == boomerangWaypoint.transform.position.z)
        {
            ReturnBoomerang();
        }
        
    }

    void ReturnBoomerang()
    {
        hasBoomerang = true; // makes sure the player has the boomerang  
       

    }
    
    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(2.0f);
        hasBoomerang = true;
        animator.SetBool("RightMouse", false);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        UnityEngine.Debug.Log("The Boomerang hit " + other.gameObject.name);
        if(other.gameObject.tag == "Enemy")
        {
            // put the function that stuns the enemy here
        }
    }

}
