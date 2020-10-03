using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LevelOneState : BaseGameState
{
    public List<bool> Triggers = new List<bool>();
    public override bool GameStateSatisfied()
    {
        bool allTriggersEnabled = true;
        for(int i = 0; i < Triggers.Count; i++)
        {
            if(Triggers[i] != true)
            {
                allTriggersEnabled = false;
                break;
            }
        }
        return allTriggersEnabled;
    }
}
