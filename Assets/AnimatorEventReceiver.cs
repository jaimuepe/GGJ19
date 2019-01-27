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

    public FMODUnity.StudioEventEmitter wooshEmitter;
    public FMODUnity.StudioEventEmitter walkEmitter;
    public FMODUnity.StudioEventEmitter talkEmitter;

    public void Woosh()
    {
        Debug.Log("Woosh");
        wooshEmitter.Play();
    }

    public void Walk()
    {
        walkEmitter.Play();
    }

    public void Talk()
    {
        talkEmitter.Play();
    }
}
