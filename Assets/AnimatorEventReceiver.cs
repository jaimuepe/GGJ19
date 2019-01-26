using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEventReceiver : MonoBehaviour
{

    CharacterController2D characterController;
    CharacterInteractions characterInteractions;

    private void OnEnable()
    {
        characterController = FindObjectOfType<CharacterController2D>();
        characterInteractions = characterController.GetComponent<CharacterInteractions>();
    }

    public void HeadbuttHit()
    {
        BreakableDoor bDoor = FindObjectOfType<BreakableDoor>();
        bDoor.HeadButt();
    }

    public void DisablePlayerActions()
    {
        characterController.MovementEnabled = false;
        characterInteractions.InteractionsEnabled = false;
    }

    public void EnablePlayerActions()
    {
        characterController.MovementEnabled = true;
        characterInteractions.InteractionsEnabled = true;
    }
}
