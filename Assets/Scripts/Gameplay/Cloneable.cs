using System;
using UnityEngine;
using UnityEngine.Rendering;

public interface ICloneable<T> where T : MonoBehaviour
{
    BaseCloneState CreateCloneState(T source);
}

