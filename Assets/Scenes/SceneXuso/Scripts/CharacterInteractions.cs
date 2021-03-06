﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractions : MonoBehaviour
{
    private bool CanDoAction, carryingObject, canActionDoor;
    public Transform ObjectTakenPosition, ObjectThrowRight, ObjectThrowLeft;
    private GameObject objectToCarry, doorToOpen;

    private CharacterController2D characterController;
    private Transform mTransform;

    public BoxCollider2D interactionsCollider;

    private GameObject interactableObject;
    private LayerMask interactableLayer;

    Animator mAnimator;

    public bool InteractionsEnabled { get; set; } = true;

    public Collider2D lastCollider;

    private void Start()
    {
        characterController = GetComponent<CharacterController2D>();
        mTransform = transform;

        if (interactionsCollider == null)
        {
            interactionsCollider = GetComponent<BoxCollider2D>();
        }

        interactableLayer = LayerMask.GetMask("Interactable");

        mAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        interactableObject = null;

        mAnimator.SetBool("headbutt", false);

        if (!InteractionsEnabled)
        {
            return;
        }

        if (carryingObject)
        {
            objectToCarry.transform.position = ObjectTakenPosition.transform.position;

            if (Input.GetKeyDown(KeyCode.R))
            {
                ThrowObject();
            }
        }
        else
        {
            bool positive = interactionsCollider.transform.eulerAngles.y < 90.0f;
            
            Collider2D collider = Physics2D.OverlapBox(
                interactionsCollider.transform.position + new Vector3(
                    interactionsCollider.offset.x * (positive ? 1 : -1),
                    interactionsCollider.offset.y,
                    0.0f),
                interactionsCollider.size,
                0.0f,
                interactableLayer);

            if (collider)
            {
                if (lastCollider != collider && lastCollider!=null)
                {
                    lastCollider.gameObject.GetComponent<InteractableObjectSprite>().ShowInteractionHelper(false);
                }
                lastCollider = collider;
                interactableObject = collider.gameObject;
                if (lastCollider != null)
                {
                    interactableObject.GetComponent<InteractableObjectSprite>().ShowInteractionHelper(true);
                }
            }
            else
            {
                if (lastCollider != null)
                {
                    lastCollider.gameObject.GetComponent<InteractableObjectSprite>().ShowInteractionHelper(false);
                    lastCollider = null;
                }
            }
        }

        if (interactableObject && Input.GetKeyDown(KeyCode.E))
        {
            InteractableObjectSprite interactable = interactableObject.GetComponent<InteractableObjectSprite>();
            InteractionsManager.Instance.ResolveInteraction(interactable.id, interactableObject);
        }
    }

    private void ProcessAction(GameObject interactableObject)
    {
        interactableObject.transform.position = ObjectTakenPosition.position;
        objectToCarry = interactableObject;
        carryingObject = true;
    }

    private void ThrowObject()
    {
        if (characterController.MovementDirection > 0)
        {
            objectToCarry.transform.position = ObjectThrowRight.position;
        }
        else
        {
            objectToCarry.transform.position = ObjectThrowLeft.position;
        }

        carryingObject = false;
        objectToCarry = null;
    }

    private void OpenDoor(GameObject door)
    {
        Vector3 initialDoorPosition = door.transform.position;
        door.transform.position += new Vector3(0, 3, 0);
        StartCoroutine(CloseDoorAfterTime(initialDoorPosition, door));
    }

    private IEnumerator CloseDoorAfterTime(Vector3 doorInitialPosition, GameObject door)
    {
        yield return new WaitForSeconds(2f);
        door.transform.position = doorInitialPosition;
    }

    private void GetShorterDistanceObject()
    {

    }
}
