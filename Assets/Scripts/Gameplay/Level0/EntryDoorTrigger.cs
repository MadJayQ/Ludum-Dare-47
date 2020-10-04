using System;
using System.Collections.Generic;

using UnityEngine;

public class EntryDoorTrigger : BaseTrigger
{
    [SerializeField] private EntryRoomGameState entryRoomState;

    public override void OnTriggered()
    {
        if (entryRoomState.gameObject.activeInHierarchy)
        {
            entryRoomState.TriggerStateChanged(true);
        }
    }

    public override void TriggerDeactivated()
    {
        if (entryRoomState.gameObject.activeInHierarchy)
        {
            entryRoomState.TriggerStateChanged(false);
        }
    }
}
