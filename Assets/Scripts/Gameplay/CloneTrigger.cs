using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CloneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BaseTrigger trigger = other.GetComponent<BaseTrigger>();
        if(trigger != null)
        {
            trigger.OnTriggered();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BaseTrigger trigger = other.GetComponent<BaseTrigger>();
        if(trigger != null)
        {
            trigger.TriggerDeactivated();
        }
    }
}
