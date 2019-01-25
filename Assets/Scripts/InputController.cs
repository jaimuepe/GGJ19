using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    [SerializeField]
    private CharacterController2D characterController2D;

    private bool jumpPressed;
    private bool jumpReleased;

    private float moveDirection;

    private void Update()
    {
        GetInput();
        ProcessInput();
    }

    private void GetInput()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
        jumpPressed = Input.GetButtonDown("Jump");
        bool jumpHold = !jumpPressed && Input.GetButton("Jump");
        jumpReleased = !jumpPressed && !jumpHold;
    }

    private void ProcessInput()
    {
        Debug.Log(moveDirection != 0.0f);

        if (moveDirection != 0.0f)
        {
            characterController2D.RequestMove(moveDirection);
        }
        if (jumpPressed)
        {
            characterController2D.RequestJump();
        }
        else if (jumpReleased)
        {
            characterController2D.RequestStopJumping();
        }
    }
}
