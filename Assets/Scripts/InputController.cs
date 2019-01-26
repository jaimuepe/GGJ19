using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    [SerializeField]
    private CharacterController2D characterController2D;

    private float moveDirection;

    private void Update()
    {
        GetInput();
        ProcessInput();
    }

    private void GetInput()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
    }

    private void ProcessInput()
    {
        //Debug.Log(moveDirection != 0.0f);

        if (moveDirection != 0.0f)
        {
            characterController2D.RequestMove(moveDirection);
        }
    }
}
