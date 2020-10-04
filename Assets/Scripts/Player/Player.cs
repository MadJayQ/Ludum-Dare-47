using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public struct PlayerInput
{
    public Vector3 MousePosition;
    //Buttons pressed
}

public class PlayerInteractEvent : UnityEvent { }

public enum PlayerCloneWindupState
{
    WindingUp = 0,
    Casted = 1,
    WindingDown = 2
}


public class Player : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float windupDecay = 1f;

    private PlayerCloneWindupState windupState = PlayerCloneWindupState.WindingUp;
    private Coroutine windingUpRoutine;

    //Readonly access for the character controller for our player
    public CharacterController Controller => characterController;

    private float createCloneWindupProgress = 0f;

    public MeshFilter MeshFilter
    {
        get
        {
            return Controller.GetComponent<MeshFilter>();
        }
    }

    private PlayerInput input = new PlayerInput();

    public PlayerInteractEvent OnPlayerInteract = new PlayerInteractEvent();

#if UNITY_EDITOR
    [SerializeField] private bool drawDebugInformation;
    Vector3 debugMouseWorldPosition;
#endif

    public void Possess(CharacterController controller)
    {
        characterController = controller;
        controller.TriggerEnterEvent.AddListener(OnPlayerTriggerEntered);
        controller.TriggerExitEvent.AddListener(OnPlayerTriggerExited);
    }

    public void Update()
    {
        PollInput();

        SetPlayerRotation();
        SetPlayerVelocity();

        TickPlayerCreateClone();
    }

    private void PollInput()
    {
        input.MousePosition = Input.mousePosition;
        if (Input.GetKey(KeyCode.Space))
        {
            createCloneWindupProgress += Time.deltaTime;
        }
        else
        {
            createCloneWindupProgress -= windupDecay * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerInteract();
        }
    }

    private void TickPlayerCreateClone()
    {
        if (createCloneWindupProgress <= 0f)
        {
            createCloneWindupProgress = 0f;
        }
        if (createCloneWindupProgress >= 1f)
        { 
            createCloneWindupProgress = 0f;
            windupState = PlayerCloneWindupState.Casted;
            AnimationSystem.Instance.PlayerCastCloneCreate();
            //Set state to casted
        }
        float progress = Mathf.Clamp01(Mathf.Pow(createCloneWindupProgress, 2f));
        switch (windupState)
        {
            case PlayerCloneWindupState.WindingUp:
                AnimationSystem.Instance.PlayerCloneCreateWindup(progress);
                break;
            case PlayerCloneWindupState.WindingDown:
                windupState = PlayerCloneWindupState.WindingUp;
                break;
            case PlayerCloneWindupState.Casted:
                if (AnimationSystem.Instance.CastAnimationFinished)
                {
                    windupState = PlayerCloneWindupState.WindingDown;
                    AnimationSystem.Instance.WindDown();
                    CreatePlayerClone();
                }
                break;
        }
    }

    private void PlayerInteract()
    {
        OnPlayerInteract.Invoke();
    }

    private void SetPlayerVelocity()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        characterController.MoveInputVector = new Vector3(inputX, 0, inputY);
    }

    private void SetPlayerRotation()
    {
        //First we calculate what the current mouses world position would be
        Vector3 mouseWorldPosition = CalculateMouseWorldPosition(mainCamera.nearClipPlane, characterController.Origin);
#if UNITY_EDITOR
        debugMouseWorldPosition = mouseWorldPosition;
#endif
        characterController.LookAt(mouseWorldPosition);

    }

    private void CreatePlayerClone()
    {
        PlayerCloneState cloneState = new PlayerCloneState(this);
        CloneSystem.Instance.CreateClone(cloneState);
        Administrator.Instance.RespawnPlayer();
    }

    private void OnPlayerTriggerEntered(BaseTrigger trigger)
    {
        trigger.OnTriggered();
    }

    private void OnPlayerTriggerExited(BaseTrigger trigger)
    {
        trigger.TriggerDeactivated();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (drawDebugInformation)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(debugMouseWorldPosition, 1f);
        }
    }
#endif
    private Vector3 CalculateMouseWorldPosition(float z, Vector3 focusPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(input.MousePosition);
        Plane xy = new Plane(Vector3.up, new Vector3(focusPosition.x, focusPosition.y, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
