using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SingletonTag(rootObject:"Debug Objects")]
class DebugView : MonoSingleton<DebugView>
{
    public Dictionary<string, Vector3> Spheres;
}