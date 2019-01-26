using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRotatingFish : MonoBehaviour
{

    private static int TARGET_ANGLE = 0;

    public int id;
    public float rotationDegrees = 45;

    WallRotatingFish[] wallRotatingFishes;

    private void Start()
    {
        wallRotatingFishes = FindObjectsOfType<WallRotatingFish>();
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (id == 0)
            {
                Rotate();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            if (id == 1)
            {
                Rotate();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            if (id == 2)
            {
                Rotate();
            }
        }
    }
#endif

    public void Rotate()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, -rotationDegrees));
        CheckSolution();
    }

    private void CheckSolution()
    {
        bool rightSolution = true;
        for (int i = 0; i < wallRotatingFishes.Length; i++)
        {
            int angle = (int)wallRotatingFishes[i].transform.eulerAngles.z;
            if (angle != TARGET_ANGLE)
            {
                rightSolution = false;
                break;
            }
        }

        Debug.Log(rightSolution ? "OK" : "NO OK");
    }
}
