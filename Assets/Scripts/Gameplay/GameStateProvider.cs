using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateProvider : MonoSingleton<GameStateProvider>
{
    public List<BaseGameState> GameStates;
    private int currentGameState = 0;

    private void Start()
    {
        Administrator.Instance.OnDoorOpen.AddListener(OnDoorOpen);
    }

    public static BaseGameState GetCurrentGameState()
    {
        return Instance.GameStates[Instance.currentGameState];
    }

    private void OnDoorOpen(int doorIndex)
    {
        Debug.Assert(currentGameState < GameStates.Count);
        GameStates[currentGameState++].enabled = false; //Deactivate our current gamestate
        GameStates[currentGameState].enabled = true; //Enable our next gamestate
    }
}