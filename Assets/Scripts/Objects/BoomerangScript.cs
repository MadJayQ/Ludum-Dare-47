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
    GameObject enemyHit;
    CapsuleCollider enemyCollider;
    MeshCollider[] enemyLight;
  
    
    
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
            UnityEngine.Debug.Log("We hit an Enemy Guard and their tag is " + other.gameObject.tag);
            enemyHit = other.gameObject;
            enemyLight = enemyHit.GetComponentsInChildren<MeshCollider>();
            enemyCollider = enemyHit.GetComponent<CapsuleCollider>();
            UnityEngine.Debug.Log("enemyHit name is " + enemyHit.gameObject.name);
            StartCoroutine(EnemyStun());   
        }
        
    }

    IEnumerator EnemyStun()
    {
        enemyHit.SetActive(false);
        enemyCollider.enabled = false;
        enemyLight[0].enabled = false;
        yield return new WaitForSeconds(0.5f);
        enemyHit.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        enemyHit.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        enemyHit.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        enemyHit.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        enemyHit.SetActive(true);
        enemyLight[0].enabled = true;
        enemyCollider.enabled = true;

    }

}
