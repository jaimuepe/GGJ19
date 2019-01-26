using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingWheel : MonoBehaviour
{

    private static int TARGET_ANGLE = 0;

    RotatingWheel[] rotatingWheels;

    public int id;
    public float rotationDegrees = 45;

    public WallRotatingFish rotatingFish;

    private void Start()
    {
        rotatingWheels = FindObjectsOfType<RotatingWheel>();
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, -rotationDegrees));
        rotatingFish.Rotate(rotationDegrees);
        CheckSolution();
    }

    private void CheckSolution()
    {
        bool rightSolution = true;
        for (int i = 0; i < rotatingWheels.Length; i++)
        {
            int angle = (int)rotatingWheels[i].transform.eulerAngles.z;
            if (angle != TARGET_ANGLE)
            {
                rightSolution = false;
                break;
            }
        }

        Debug.Log(rightSolution ? "OK" : "NO OK");
    }
}
