using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateProvider : MonoSingleton<GameStateProvider>
{
    public List<BaseGameState> GameStates;
    private int currentGameState = 0;

    private void OnDoorOpen(int doorIndex)
    {
        currentGameState++;
        Debug.Assert(currentGameState < GameStates.Count);
    }
}