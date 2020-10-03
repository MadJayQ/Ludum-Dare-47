using System;
using UnityEngine;
using UnityEditor;

public struct PlayerInput
{
    public Vector3 MousePosition;
    //Buttons pressed
}

public class Player : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CharacterController characterController;

    private PlayerInput input = new PlayerInput();

    public void Update()
    {
        if(DebugView.Instance) { }
        PollInput();
        SetPlayerRotation();
    }

    private void PollInput()
    {
        input.MousePosition = Input.mousePosition;
    }

    private void SetPlayerRotation()
    { 
        //First we calculate what the current mouses world position would be

    }

    private Vector3 CalculateMouseWorldPosition(float cameraZ)
    {
        Ray ray = mainCamera.ScreenPointToRay(input.MousePosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, cameraZ));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
