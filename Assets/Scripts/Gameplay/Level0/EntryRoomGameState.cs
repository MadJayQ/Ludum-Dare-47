using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class EntryRoomGameState : BaseGameState
{
    private bool doorOpened = false;
    public void TriggerStateChanged(bool activated)
    {
        if(activated)
        {
            Administrator.Instance.Player.OnPlayerInteract.AddListener(OnPlayerInteract);
        }
        else
        {
            Administrator.Instance.Player.OnPlayerInteract.RemoveListener(OnPlayerInteract);
        }
    }

    private void OnPlayerInteract()
    {
        doorOpened = true; 
    }

    public override bool GameStateSatisfied()
    {
        return doorOpened;
    }
}
