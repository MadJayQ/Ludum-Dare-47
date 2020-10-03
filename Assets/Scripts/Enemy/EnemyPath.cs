using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{

    public Transform[] pathingList; // custom list to create the transforms that the enemy would use
    public GameObject Enemy;
    public int controlPath = 1; // beginning of the list
    public int currentPath = 0; // the current path that the enemy is on
    Transform targetPath; // the target that the enemy is walking torwards

    public float EnemySpeed = 3f; // speed of the enemy

    

    private void Update()
    {
        if(controlPath % 2 == 1 )
        {
            if(currentPath < this.pathingList.Length)
            {
                if(targetPath == null)
                {
                    targetPath = pathingList[currentPath];
                }
                MoveUpList();
            }
            if(currentPath == pathingList.Length)
            {
                controlPath++;
            }
        }

        if (controlPath % 2 == 0)
        {
            if (currentPath == this.pathingList.Length)
            {               

                if (targetPath == null)
                {
                    targetPath = pathingList[currentPath - 1];
                }
                
            }
            MoveDownList();
            if(currentPath == 0)
            {
                controlPath++;
            }
        }
    }




    void MoveUpList()
    {
        


        if (transform.position == targetPath.position)
        {
            currentPath++;
            
            targetPath = pathingList[currentPath];
        }
        

        // rotate torwards target
        transform.forward = Vector3.RotateTowards(transform.forward, targetPath.position - transform.position, EnemySpeed * Time.deltaTime, 0.0f);

        // move torwards the target
        transform.position = Vector3.MoveTowards(transform.position, targetPath.position, EnemySpeed * Time.deltaTime);

    }

    void MoveDownList()
    {

        
        
        if (transform.position == targetPath.position)
        {
            currentPath -= 1;
            targetPath = pathingList[currentPath];
        }

        // rotate torwards target
        transform.forward = Vector3.RotateTowards(transform.forward, targetPath.position - transform.position, EnemySpeed * Time.deltaTime, 0.0f);

        // move torwards the target
        transform.position = Vector3.MoveTowards(transform.position, targetPath.position, EnemySpeed * Time.deltaTime);
    }

}
