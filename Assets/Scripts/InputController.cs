using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    [SerializeField]
    private CharacterController2D characterController2D;

    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    private void Update()
    {
        GetInput();
        ProcessInput();
    }

    private void GetInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }

    private void ProcessInput()
    {
<<<<<<< HEAD
        if (HorizontalInput != 0.0f)
=======
        //Debug.Log(moveDirection != 0.0f);

        if (moveDirection != 0.0f)
>>>>>>> 8017aa60ec2344707201384d454386c23f9b6308
        {
            characterController2D.RequestHorizontal(HorizontalInput);
        }
    }
}
