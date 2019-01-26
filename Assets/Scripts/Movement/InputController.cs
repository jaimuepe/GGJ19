﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    private CharacterController2D characterController2D;

    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    private void Start()
    {
        characterController2D = FindObjectOfType<CharacterController2D>();
    }

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
        if (HorizontalInput != 0.0f)
        {
            characterController2D.RequestHorizontal(HorizontalInput);
        }
    }
}