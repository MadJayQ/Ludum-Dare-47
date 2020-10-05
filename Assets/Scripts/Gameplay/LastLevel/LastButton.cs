using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastButton : MonoBehaviour
{
    bool buttonPressed;
    bool touched;
    
    // Start is called before the first frame update
    void Start()
    {
        buttonPressed = false;
        touched = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    private void OnTriggerStay(Collider other)
    {
        buttonPressed = true;
        
        if(other.gameObject.name == "PlayerClone")
        { 
            LastLevel level = GameObject.Find("LastLevelTrigger").GetComponent<LastLevel>();
            Debug.Log(this.gameObject.name + " is down");
            Debug.Log("Clone is in Collider");
            if(touched == false) 
            { 
                level.ButtonDown();
                touched = true;
            }
        }
       
        
    }
}
