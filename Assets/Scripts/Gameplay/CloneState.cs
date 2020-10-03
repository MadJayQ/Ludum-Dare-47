using System;
using UnityEngine;

public abstract class BaseCloneState : MonoBehaviour
{
    public Vector3 Origin { get; protected set; }
    public Quaternion Rotation { get; protected set; }

    //CreateGameObject is responsible for instantiating the gameobject that will be used to represent this clone state
    //Factory method for creating the gameobject
    public abstract GameObject CreateGameObject();

}

