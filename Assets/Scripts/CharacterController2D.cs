using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public Movementparameters movementParameters;
    public CollisionParameters collisionParameters;

    public float smoothSpeed;

    private Transform mTransform;

    [Header("Debug")]

    private Vector2 deltaMovement;

    [NonSerialized]
    public MovementSystem movementSystem;
    [NonSerialized]
    public JumpGravitySystem jumpSystem;
    [NonSerialized]
    public CollisionSystem collisionSystem;
    [NonSerialized]
    public StairsSystem stairsSystem;

    private void Awake()
    {
        mTransform = transform;

        collisionSystem = GetComponent<CollisionSystem>();
        if (!collisionSystem)
        {
            collisionSystem = gameObject.AddComponent<CollisionSystem>();
        }

        movementSystem = GetComponent<MovementSystem>();
        if (!movementSystem)
        {
            movementSystem = gameObject.AddComponent<MovementSystem>();
        }

        jumpSystem = GetComponent<JumpGravitySystem>();
        if (!jumpSystem)
        {
            jumpSystem = gameObject.AddComponent<JumpGravitySystem>();
        }

        stairsSystem = GetComponent<StairsSystem>();
        if (!stairsSystem)
        {
            stairsSystem = gameObject.AddComponent<StairsSystem>();
        }
    }

    public void RequestHorizontal(float moveDirection)
    {
        // either -1, 0 or 1
        if (moveDirection != 0.0f)
        {
            moveDirection = Mathf.Sign(moveDirection);
        }

        movementSystem.Direction = moveDirection;
    }

    private void Update()
    {
        movementSystem.Calculate();
        jumpSystem.Calculate();

        Vector2 mTotalVelocity = new Vector2(
            movementSystem.AccumulatedVelocity,
            jumpSystem.AccumulatedVelocity.y);

        deltaMovement = mTotalVelocity * Time.deltaTime;

        stairsSystem.Calculate(ref deltaMovement);
        if (collisionSystem.enabled)
        {
            deltaMovement = collisionSystem.Calculate(deltaMovement);
        }

        movementSystem.UpdateCollisionData();
        jumpSystem.UpdateCollisionData();
    }

    private void LateUpdate()
    {
        ResolvePosition();
        movementSystem.ClearVariablesEndFrame();
    }

    private void ResolvePosition()
    {
        float y = mTransform.position.y + deltaMovement.y;

        mTransform.position = new Vector3(
                Mathf.Lerp(
                    mTransform.position.x,
                    mTransform.position.x + deltaMovement.x,
                    smoothSpeed * Time.deltaTime),
                y,
                /*
                Mathf.Lerp(
                     mTransform.position.y,
                     y,
                    smoothSpeed * Time.deltaTime),
                 */
                mTransform.position.z);
    }
}
