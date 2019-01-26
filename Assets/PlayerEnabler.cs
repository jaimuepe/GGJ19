using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnabler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterController2D controller = collision.gameObject.GetComponent<CharacterController2D>();
            CharacterInteractions ch = collision.gameObject.GetComponent<CharacterInteractions>();

            ch.InteractionsEnabled = true;
            controller.MovementEnabled = true;
            controller.WalkRightEndlessly = false;
        }
    }
}
