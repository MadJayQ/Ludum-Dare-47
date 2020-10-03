using System;
using System.Collections.Generic;

using UnityEngine;

public class EntryDoorTrigger : BaseTrigger
{
    [SerializeField] private EntryRoomGameState entryRoomState;

    public override void OnTriggered()
    {
        if (entryRoomState.enabled)
        {
            entryRoomState.TriggerStateChanged(true);
        }
    }

    public override void TriggerDeactivated()
    {
        if (entryRoomState.enabled)
        {
            entryRoomState.TriggerStateChanged(false);
        }
    }
}
