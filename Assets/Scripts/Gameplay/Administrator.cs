using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[SingletonTag(rootObject: "Gameplay Objects")]
public class Administrator : MonoSingleton<Administrator>
{
    [SerializeField] private GameObject playerPrefab; //This is the prefab that we will use to spawn in the player when the game starts
    [SerializeField] private Player player;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera mainCamera;
    [SerializeField] private BaseGameState currentGameState;

    public Player Player => player;

    private int currentDoorIndex = 0;

    public RespawnPoint CurrentRespawn;


    public class DoorOpenEvent : UnityEvent<int> { }

    public DoorOpenEvent OnDoorOpen = new DoorOpenEvent();

    public void RespawnPlayer()
    {
        CurrentRespawn.TeleportPlayerToRespawn(player);
    }

    IEnumerator Start()
    {
        yield return PlayerCloneState.LoadAssets(); //Load our player clone state assets in
        //Spawn our player
        CharacterController playerController = Instantiate(playerPrefab).GetComponent<CharacterController>();
        //Setup player
        Debug.Assert(playerController != null, "Our player prefab does not have a character controller!");
        player.Possess(playerController);
        player.enabled = true;
        RespawnPlayer();
        //Setup camera
        mainCamera.Follow = playerController.transform;
        //Set gamestate
        currentGameState = GameStateProvider.GetCurrentGameState();
    }

    void Update()
    {
        if (currentGameState != null)
        {
            if (currentGameState.GameStateSatisfied())
            {
                RoomCompleted();
            }
        }
    }

    private void RoomCompleted()
    {
        OnDoorOpen.Invoke(currentDoorIndex++);
        currentGameState = GameStateProvider.GetCurrentGameState(); //Store the new current gamestate
    }
}
