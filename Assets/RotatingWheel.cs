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

    public FMODUnity.StudioEventEmitter switchEmitter;

    public void Rotate()
    {
        if (!usable)
        {
            return;
        }

        switchEmitter.Play();

        transform.Rotate(new Vector3(0.0f, 0.0f, -rotationDegrees));
        rotatingFish.Rotate(rotationDegrees);
        CheckSolution();
    }

    private void CheckSolution()
    {
        bool rightSolution = true;
        for (int i = 0; i < rotatingWheels.Length; i++)
        {
            float angle = rotatingWheels[i].transform.eulerAngles.z;
            float targetAngle = rotatingWheels[i].targetAngle;

            if (Mathf.Abs(angle - targetAngle) > 5.0f)
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

            InteractionsManager.Instance.ResolveInteraction("wheel_puzzle_ok", gameObject);
        }
    }
}
