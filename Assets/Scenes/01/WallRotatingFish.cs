using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRotatingFish : MonoBehaviour
{
    
    public void Rotate(float rotationDegrees)
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, -rotationDegrees));
    }
}
