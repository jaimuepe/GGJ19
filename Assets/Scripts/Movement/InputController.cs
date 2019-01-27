using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    private CharacterController2D characterController2D;

    public bool _gamepadConnected;
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    public bool _onTutorial, _blockMovement;
    public bool _movementTutorialPassed, _actionTutorialPassed;
    public TutorialManager m_TutorialManager;

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
        if (!_blockMovement)
        {
            ProcessInput();
        }
    }

    private void GetInput()
    {
        if (!_onTutorial)
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
        else
        {
            TutorialChecker();
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
            if (Mathf.Abs(HorizontalInput) > 0.9f)
            {
                characterController2D.RequestHorizontal(HorizontalInput);
            }
        }
    }

    private void TutorialChecker()
    {
        if (!_blockMovement)
        {
            if (!_movementTutorialPassed)
            {
                if (Input.GetAxisRaw("Horizontal") > 0f)
                {
                    _movementTutorialPassed = true;
                    _onTutorial = false;
                    m_TutorialManager.ShowMovementTutorial(false);
                }
                return;
            }
        }
    }

    public void ActionTutorialPass()
    {
        if (!_actionTutorialPassed && _movementTutorialPassed)
        {
            _blockMovement = false;
            _movementTutorialPassed = true;
            _onTutorial = false;
            m_TutorialManager.ShowActionTutorial(false);
        }
    }
}
