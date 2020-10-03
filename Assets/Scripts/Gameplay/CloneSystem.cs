using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// The Clone System is a singleton object that controls the creation and or deletion of clones for the core gameplay loop
/// </summary>

[SingletonTag(rootObject:"Gameplay Objects")]
public class CloneSystem : MonoSingleton<CloneSystem>
{
    public void CreateClone(BaseCloneState cloneState)
    {
        //Call our factory method for creating the clone gameobject
        GameObject cloneObject = cloneState.CreateGameObject();
        cloneObject.transform.parent = this.transform;
    }
}

