using System;
using System.Collections.Generic;
using System.Collections;

using UnityEditor;
using UnityEngine;

public abstract class BaseTrigger : MonoBehaviour
{
    public abstract void OnTriggered();
    public abstract void TriggerDeactivated();
}
