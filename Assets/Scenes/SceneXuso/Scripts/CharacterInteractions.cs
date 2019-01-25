using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractions : MonoBehaviour
{
    private bool CanDoAction;
    public Transform ObjectTakenPosition;

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        CanDoAction = true;
        Debug.Log("Character trigger activated: Enter");
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && CanDoAction)
        {
            ProcessAction(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CanDoAction = false;
        Debug.Log("Character trigger activated: Enter");
    }

    private void ProcessAction(GameObject interactableObject)
    {
        interactableObject.transform.position = ObjectTakenPosition.position;
    }
}
