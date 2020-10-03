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

public class Player : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CharacterController characterController;

    //Readonly access for the character controller for our player
    public CharacterController Controller => characterController;

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
    }

    public void Update()
    {
        PollInput();

        SetPlayerRotation();
        SetPlayerVelocity();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreatePlayerClone();
        }
    }

    private void PollInput()
    {
        input.MousePosition = Input.mousePosition;
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

    private void OnPlayerTriggerEntry(Collider otherTrigger)
    {

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(drawDebugInformation)
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
