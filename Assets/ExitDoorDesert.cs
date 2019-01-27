using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorDesert : MonoBehaviour
{
    public bool usable = true;

    public int numberTimesUsed = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (usable && collision.CompareTag("Player"))
        {
            InteractableObjectSprite ios = GetComponent<InteractableObjectSprite>();
            InteractionsManager.Instance.ResolveInteraction(ios.id, gameObject);
        }
    }
}
