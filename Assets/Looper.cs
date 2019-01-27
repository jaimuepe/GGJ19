using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Camera mainCamera = Camera.main;

            float screenAspect = (float)Screen.width / (float)Screen.height;
            float camHalfHeight = mainCamera.orthographicSize;
            float camHalfWidth = screenAspect * camHalfHeight;
            float camWidth = 2.0f * camHalfWidth;

            Transform playerTransform = collision.transform;
            playerTransform.position = new Vector3(
                mainCamera.transform.position.x - camHalfWidth,
                playerTransform.position.y,
                playerTransform.position.z);
        }
    }
}
