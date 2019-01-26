using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisabler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterController2D controller = collision.gameObject.GetComponent<CharacterController2D>();
            controller.MovementEnabled = false;
            controller.WalkRightEndlessly = false;
        }
    }
}
