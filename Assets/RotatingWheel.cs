using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingWheel : MonoBehaviour
{

    RotatingWheel[] rotatingWheels;

    public int id;
    public float rotationDegrees = 45;

    public WallRotatingFish rotatingFish;
    public float targetAngle;

    public bool usable = true;

    private void Start()
    {
        rotatingWheels = FindObjectsOfType<RotatingWheel>();
    }

    public void Rotate()
    {
        if (!usable)
        {
            return;
        }

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
            int targetAngle = (int)rotatingWheels[i].targetAngle;

            if (angle != targetAngle)
            {
                rightSolution = false;
                break;
            }
        }

        if (rightSolution)
        {
            for (int i = 0; i < rotatingWheels.Length; i++)
            {
                rotatingWheels[i].usable = false;
            }
        }
    }
}
