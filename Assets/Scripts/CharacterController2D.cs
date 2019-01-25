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

    [SerializeField]
    private Vector2 mTotalVelocity;

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

    public void RequestMove(float moveDirection)
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

        mTotalVelocity = new Vector2(movementSystem.AccumulatedVelocity + jumpSystem.AccumulatedVelocity.x,
            jumpSystem.AccumulatedVelocity.y);

        Vector2 deltaMovement = mTotalVelocity * Time.deltaTime;

        deltaMovement = collisionSystem.Calculate(deltaMovement);

        movementSystem.UpdateCollisionData();
        jumpSystem.UpdateCollisionData();

        mTotalVelocity = new Vector2(movementSystem.AccumulatedVelocity + jumpSystem.AccumulatedVelocity.x,
            jumpSystem.AccumulatedVelocity.y);

        ResolvePosition(deltaMovement);

        movementSystem.ClearVariablesEndFrame();
    }

    private void ResolvePosition(Vector2 deltaMovement)
    {
        mTransform.position = new Vector3(
                Mathf.Lerp(mTransform.position.x, mTransform.position.x + deltaMovement.x, smoothSpeed * Time.deltaTime),
                mTransform.position.y + deltaMovement.y,
                mTransform.position.z);
    }
}
