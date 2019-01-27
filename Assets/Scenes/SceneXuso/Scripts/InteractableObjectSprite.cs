using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class InteractableObjectSprite : MonoBehaviour
{
    public string id;
    public GameObject InteractionHelper;

    public void ShowInteractionHelper(bool show = true)
    {
        if (InteractionHelper != null)
            InteractionHelper.SetActive(show);
    }
}
