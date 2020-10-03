using System;
using System.Collections.Generic;

using UnityEngine;

public class EntryDoorTrigger : BaseTrigger
{
    public override void OnTriggered()
    {
        Administrator.Instance.Player.OnPlayerInteract.AddListener(OnPlayerInteract);
    }

    public override void TriggerDeactivated()
    {
        Administrator.Instance.Player.OnPlayerInteract.RemoveListener(OnPlayerInteract);
    }

    private void OnPlayerInteract()
    {

    }
}
