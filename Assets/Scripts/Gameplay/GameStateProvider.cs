using System;
using System.Collections.Generic;
using UnityEngine;

[SingletonTag(rootObject: "Gameplay Objects")]
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
        GameStates[currentGameState].gameObject.SetActive(false);
        if(currentGameState < GameStates.Count)
        {
            GameStates[++currentGameState].gameObject.SetActive(true);
        }
    }
}