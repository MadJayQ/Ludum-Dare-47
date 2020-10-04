using System;
using UnityEngine;

public class LevelOneButtonTrigger : BaseTrigger
{
    [SerializeField] private LevelOneState gameState;
    [SerializeField] private int buttonIdx;
    public override void OnTriggered()
    {
        if(gameState.gameObject.activeInHierarchy)
        {
            gameState.Triggers[buttonIdx] = true;
        }
    }
    public override void TriggerDeactivated()
    {
        if(gameState.gameObject.activeInHierarchy)
        {
            gameState.Triggers[buttonIdx] = false;
        }
    }
}