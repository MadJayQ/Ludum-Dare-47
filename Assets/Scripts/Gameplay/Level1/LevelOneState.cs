using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LevelOneState : BaseGameState
{
    public bool TriggerOneActive = false;
    public bool TriggerTwoActive = false;

    public override bool GameStateSatisfied()
    {
        return TriggerOneActive && TriggerTwoActive;
    }
}
