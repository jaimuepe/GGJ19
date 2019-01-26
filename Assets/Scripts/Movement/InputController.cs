using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    private CharacterController2D characterController2D;

    private bool _gamepadConnected;
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    private void Start()
    {
        characterController2D = FindObjectOfType<CharacterController2D>();
        string[] joystickNames = Input.GetJoystickNames();
        _gamepadConnected = joystickNames.Length > 0 ? true : false;
        Debug.Log("Gamedpads connected:" + _gamepadConnected);
    }

    private void Update()
    {
        GetInput();
        ProcessInput();
    }

    private void GetInput()
    {
        if (true)
        {
            HorizontalInput = Input.GetAxisRaw("Horizontal");
            VerticalInput = Input.GetAxisRaw("Vertical");
        }
        else
        {
            HorizontalInput = Input.GetAxisRaw("HorizontalXbox360");
            VerticalInput = Input.GetAxisRaw("Vertical");
        }   
    }

    private void ProcessInput()
    {
        if (!_gamepadConnected)
        {
            if (HorizontalInput != 0.0f)
            {
                characterController2D.RequestHorizontal(HorizontalInput);
            }
        }
        else
        {
            if (Mathf.Abs(HorizontalInput)>0.9f)
            {
                characterController2D.RequestHorizontal(HorizontalInput);
            }
        }
    }
}
