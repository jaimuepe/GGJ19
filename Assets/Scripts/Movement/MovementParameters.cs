using System;
using UnityEngine;

[Serializable]
public class Movementparameters
{
    [Header("Movement")]
    public float maxMovementSpeed = 5.0f;

    public Vector2 gravityVector;
}

[Serializable]
public class CollisionParameters
{
    public float groundForgivingDistance = 0.1f;
    public int numberOfVerticalRays = 5;
    public int numberOfHorizontalRays = 5;
    public float skinWidth = 0.02f;
}