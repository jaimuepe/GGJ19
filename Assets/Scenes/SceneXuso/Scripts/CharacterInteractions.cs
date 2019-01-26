using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractions : MonoBehaviour
{
    private bool CanDoAction, carryingObject, canActionDoor;
    public Transform ObjectTakenPosition, ObjectThrowRight, ObjectThrowLeft;
    private GameObject objectToCarry, doorToOpen;
    private enum FaceDirection { Right, Left };
    private FaceDirection m_FaceDirection = FaceDirection.Right;

    void Start()
    {

    }

    void Update()
    {
        if (carryingObject)
        {
            objectToCarry.transform.position = ObjectTakenPosition.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (carryingObject)
            {
                ThrowObject(FaceDirection.Right);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "InteractableObject":
                CanDoAction = true;
                //Debug.Log("Character trigger activated: Enter");
                break;
            case "Door":
                //doorToOpen = other.gameObject;
                canActionDoor = true;
                break;
            default:
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "InteractableObject":
                //Debug.Log("Character trigger activated: Stay");
                if (Input.GetKeyDown(KeyCode.E) && CanDoAction)
                {
                    ProcessAction(other.gameObject);
                }
                break;
            case "Door":
                if (Input.GetKeyDown(KeyCode.E) && canActionDoor)
                {
                    OpenDoor(other.gameObject);
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "InteractableObject":
                CanDoAction = false;
                //Debug.Log("Character trigger activated: Enter");
                break;
            case "Door":
                canActionDoor = false;
                break;
            default:
                break;
        }
    }

    private void ProcessAction(GameObject interactableObject)
    {
        interactableObject.transform.position = ObjectTakenPosition.position;
        objectToCarry = interactableObject;
        carryingObject = true;
    }

    private void ThrowObject(FaceDirection direction)
    {
        if (m_FaceDirection == FaceDirection.Right)
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
        StartCoroutine(CloseDoorAfterTime(initialDoorPosition,door));
    }

    private IEnumerator CloseDoorAfterTime(Vector3 doorInitialPosition,GameObject door)
    {
        yield return new WaitForSeconds(2f);
        door.transform.position = doorInitialPosition;     
    }

    private void GetShorterDistanceObject()
    {

    }
}
